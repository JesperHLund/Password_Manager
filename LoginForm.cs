using BCrypt.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Password_Manager
{
    public partial class LoginForm : Form
    {
        private Dictionary<string, string> users;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Load user data from a JSON file
            LoadUserData();
        }

        private void LoadUserData()
        {
            if (File.Exists("users.json"))
            {
                string json = File.ReadAllText("users.json");
                users = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            else
            {
                users = new Dictionary<string, string>();
            }
        }

        private void SaveUserData()
        {
            string json = JsonConvert.SerializeObject(users);
            File.WriteAllText("users.json", json);
        }

        private (string, string) encryptPassword(string password)
        {
            string salt;
            //generates a salt with a work factor of 16
            salt = BCrypt.Net.BCrypt.GenerateSalt(16);
        
            var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return (newHashedPassword, salt);
        }

        //Handles authentication of the user by comparing input password with the stored password.
        private bool Authenticate(string username, string password)
        {
            if (users.TryGetValue(username, out var userData))
            {
                //Gets the hashedpassword and salt from the userData Json
                var hashedPassword = userData;

                //Check if the hashedPassword matches the input password
                return BCrypt.Net.BCrypt.Verify(password,hashedPassword);
            }
            return false;
        }

        //Creates user using username and password
        private void CreateNewUser(string username, string password)
        {
            if (!users.ContainsKey(username))
            {
                var (hashedPassword, salt) = encryptPassword(password);
                users[username] = hashedPassword;
                SaveUserData();
                MessageBox.Show("User created successfully.");
            }
            else
            {
                MessageBox.Show("User already exists.");
            }
        }

        //Handels loginButton click event
        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (Authenticate(username, password))
            {

                //Sets a combinedSecret of password and username
                string combinedSecret = password + username;

                // Derive a consistent user-specific key
                using (var pbkdf2 = new Rfc2898DeriveBytes(combinedSecret, new byte[16], 600000, HashAlgorithmName.SHA256))
                {
                    byte[] keyBytes = pbkdf2.GetBytes(32); // 256 bits key
                    string key = Convert.ToBase64String(keyBytes);
                    
                    // Close the login form
                    this.Hide();

                    // Open the CredentialManagerForm with the derived key
                    CredentialManagerForm credentialManagerForm = new CredentialManagerForm(username, key);
                    credentialManagerForm.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Login failed. Please try again.");
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            CreateNewUser(username, password);
        }
    }
}

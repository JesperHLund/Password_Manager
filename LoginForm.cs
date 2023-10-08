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
        private Dictionary<string, (string hashedPassword, string salt)> users;

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
                users = JsonConvert.DeserializeObject<Dictionary<string, (string, string)>>(json);
            }
            else
            {
                users = new Dictionary<string, (string, string)>();
            }
        }

        private void SaveUserData()
        {
            string json = JsonConvert.SerializeObject(users);
            File.WriteAllText("users.json", json);
        }

        private (string, string) HashPassword(string password, string salt)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                if (salt == null)
                {
                    var saltBytes = new byte[32];
                    rng.GetBytes(saltBytes);
                    salt = Convert.ToBase64String(saltBytes);
                }

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPasswordBytes = new byte[salt.Length + passwordBytes.Length];

                Encoding.UTF8.GetBytes(salt).CopyTo(saltedPasswordBytes, 0);
                passwordBytes.CopyTo(saltedPasswordBytes, salt.Length);

                using (var sha256 = new SHA256Managed())
                {
                    byte[] hashedPasswordBytes = sha256.ComputeHash(saltedPasswordBytes);
                    return (Convert.ToBase64String(hashedPasswordBytes), salt);
                }
            }
        }

        private bool Authenticate(string username, string password)
        {
            if (users.TryGetValue(username, out var userData))
            {
                var (hashedPassword, salt) = userData;
                var (inputHash, _) = HashPassword(password, salt);
                return hashedPassword == inputHash;
            }
            return false;
        }

        private void CreateNewUser(string username, string password)
        {
            if (!users.ContainsKey(username))
            {
                var (hashedPassword, salt) = HashPassword(password, null);
                users[username] = (hashedPassword, salt);
                SaveUserData();
                MessageBox.Show("User created successfully.");
            }
            else
            {
                MessageBox.Show("User already exists.");
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (Authenticate(username, password))
            {
                // Close the login form
                this.Hide();

                // Open the CredentialManagerForm
                CredentialManagerForm credentialManagerForm = new CredentialManagerForm(username);
                credentialManagerForm.ShowDialog(); // ShowDialog() ensures the new form is modal
                this.Close();
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

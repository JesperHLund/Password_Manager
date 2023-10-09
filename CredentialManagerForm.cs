using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace Password_Manager
{
    public partial class CredentialManagerForm : Form
    {
        private List<Credential> credentials;
        private string currentUser;
        private string encryptionKey;

        private DataGridView dataGridViewCredentials;

        public class Credential
        {
            public string Platform { get; set; }
            public string Username { get; set; }
            public string EncryptedPassword { get; set; }
            public string IV { get; set; }
            [JsonIgnore]
            public string DecryptedPassword { get; set; } // Store decrypted password for DataGridView

        }

        public CredentialManagerForm(string username, string key)
        {
            InitializeComponent();
            currentUser = username;
            credentials = new List<Credential>();
            encryptionKey = key;
            LoadCredentials(key);
        }

        private void CredentialManagerForm_Load(object sender, EventArgs e)
        {
            // Initialize the DataGridView and other controls
            InitializeDataGridView();
            RefreshCredentialList();
        }

        private void InitializeDataGridView()
        {
            dataGridViewCredentials = new DataGridView();
            dataGridViewCredentials.Dock = DockStyle.Fill;
            dataGridViewCredentials.AutoGenerateColumns = false;
            dataGridViewCredentials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCredentials.Visible = true;

            DataGridViewTextBoxColumn platformColumn = new DataGridViewTextBoxColumn();
            platformColumn.DataPropertyName = "Platform";
            platformColumn.HeaderText = "Platform";
            platformColumn.Width = 50;
            platformColumn.ReadOnly = true;

            DataGridViewTextBoxColumn usernameColumn = new DataGridViewTextBoxColumn();
            usernameColumn.DataPropertyName = "Username";
            usernameColumn.HeaderText = "Username";
            usernameColumn.Width = 75;
            usernameColumn.ReadOnly = true;

            DataGridViewTextBoxColumn passwordColumn = new DataGridViewTextBoxColumn();
            passwordColumn.DataPropertyName = "DecryptedPassword";
            passwordColumn.HeaderText = "Password";
            passwordColumn.Width = 90;
            passwordColumn.ReadOnly = true;

            dataGridViewCredentials.Columns.Add(platformColumn);
            dataGridViewCredentials.Columns.Add(usernameColumn);
            dataGridViewCredentials.Columns.Add(passwordColumn);

            Controls.Add(dataGridViewCredentials);
        }

        // Refresh the DataGridView with stored credentials
        private void RefreshCredentialList()
        {
            var decryptedCredentials = credentials.Select(cred =>
            {
                var decryptedPassword = DecryptPassword(cred.EncryptedPassword, encryptionKey, cred.IV);
                return new Credential
                {
                    Platform = cred.Platform,
                    Username = cred.Username,
                    EncryptedPassword = cred.EncryptedPassword,
                    IV = cred.IV, 
                    DecryptedPassword = decryptedPassword
                };
            }).ToList();

            dataGridViewCredentials.DataSource = null;
            dataGridViewCredentials.DataSource = decryptedCredentials;
        }

        // Save credentials to a file
        private void SaveCredentials()
        {
            string json = JsonConvert.SerializeObject(credentials);
            File.WriteAllText($"{currentUser}_credentials.json", json);
        }

        // Load credentials from a file
        private void LoadCredentials(string key)
        {
            string filePath = $"{currentUser}_credentials.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                credentials = JsonConvert.DeserializeObject<List<Credential>>(json);
            }
        }

        // Generate an AES IV (Initialization Vector)
        private string GenerateAesIV()
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.GenerateIV();
                return Convert.ToBase64String(aesAlg.IV);
            }
        }

        // Encrypt a password with AES and a provided key and IV
        private string EncryptPassword(string password, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = Convert.FromBase64String(iv);
                aesAlg.Mode = CipherMode.CFB;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(password);
                            }
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        // Decrypt password using AES and the provided key and IV
        private string DecryptPassword(string encryptedPassword, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = Convert.FromBase64String(iv);
                aesAlg.Mode = CipherMode.CFB;

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
                {
                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }

        // Add a new credential
        private void addCredentialButton_Click(object sender, EventArgs e)
        {
            string platform = textBoxPlatform.Text;
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (!string.IsNullOrEmpty(platform) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                int selectedIndex = credentials.Count();
                string key = encryptionKey;
                string iv = GenerateAesIV();   // Generate an AES IV

                string encryptedPassword = EncryptPassword(password, key, iv);

                Credential newCredential = new Credential
                {
                    Platform = platform,
                    Username = username,
                    EncryptedPassword = encryptedPassword,
                    IV = iv, 
                };

                credentials.Add(newCredential);
                SaveCredentials();
                RefreshCredentialList();
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }
        }

        private void generatePasswordButton_Click(object sender, EventArgs e)
        {
            string generatedPassword = GeneratePassword();
            textBoxPassword.Text = generatedPassword;
        }

        private string GeneratePassword(int length = 32)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~`!@#$%^&*()_-+={[}]|\\:;\"'<,>.?/";
            var random = new Random();
            var password = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                password.Append(characters[random.Next(characters.Length)]);
            }

            return password.ToString();
        }
    }
}

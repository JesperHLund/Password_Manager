using System.Windows.Forms;
namespace Password_Manager
{
    partial class LoginForm
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Button createButton;
        private Label titleLabel;


        private void InitializeComponent()
        {
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            loginButton = new Button();
            createButton = new Button();
            titleLabel = new Label();

            // usernameTextBox
            usernameTextBox.Location = new System.Drawing.Point(100, 50);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new System.Drawing.Size(200, 20);

            // passwordTextBox
            passwordTextBox.Location = new System.Drawing.Point(100, 80);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new System.Drawing.Size(200, 20);

            // loginButton
            loginButton.Location = new System.Drawing.Point(100, 110);
            loginButton.Name = "loginButton";
            loginButton.Size = new System.Drawing.Size(90, 30);
            loginButton.Text = "Login";
            loginButton.Click += new System.EventHandler(loginButton_Click);

            // createButton
            createButton.Location = new System.Drawing.Point(210, 110);
            createButton.Name = "createButton";
            createButton.Size = new System.Drawing.Size(90, 30);
            createButton.Text = "Create User";
            createButton.Click += new System.EventHandler(createButton_Click);

            // titleLabel
            titleLabel.AutoSize = true;
            titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            titleLabel.Location = new System.Drawing.Point(100, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(200, 20);
            titleLabel.Text = "Password Manager";

            // PasswordManagerForm
            ClientSize = new System.Drawing.Size(400, 200);
            Controls.Add(usernameTextBox);
            Controls.Add(passwordTextBox);
            Controls.Add(loginButton);
            Controls.Add(createButton);
            Controls.Add(titleLabel);
            Name = "PasswordManagerForm";
            Text = "Password Manager";
            Load += new System.EventHandler(LoginForm_Load);
        }
    }
}

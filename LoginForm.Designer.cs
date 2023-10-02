using System;
using System.Drawing;
using System.Windows.Forms;

namespace Password_Manager
{
    public partial class LoginForm : Form
    {
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Label usernameLabel;
        private Label passwordLabel;

        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}

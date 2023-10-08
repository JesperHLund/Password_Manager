using System;
using System.Windows.Forms;

namespace Password_Manager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create an instance of your main form (PasswordManagerForm in this example)
            LoginForm mainForm = new LoginForm();

            // Start the application with the main form
            Application.Run(mainForm);
        }
    }
}

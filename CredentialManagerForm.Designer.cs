using System.Windows.Forms;
using System.Drawing;

namespace Password_Manager
{
    partial class CredentialManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private TextBox textBoxPlatform;
        private TextBox textBoxUsername;
        private TextBox textBoxPassword;
        private Button addCredentialButton;
        private Button generatePasswordButton;
        private TableLayoutPanel tableLayoutPanel;

        private void InitializeComponent()
        {
            textBoxPlatform = new TextBox();
            textBoxUsername = new TextBox();
            textBoxPassword = new TextBox();
            addCredentialButton = new Button();
            generatePasswordButton = new Button();
            tableLayoutPanel = new TableLayoutPanel();
            SuspendLayout();

            // textBoxPlatform
            textBoxPlatform.Location = new System.Drawing.Point(12, 12);
            textBoxPlatform.Name = "textBoxPlatform";
            textBoxPlatform.Size = new System.Drawing.Size(200, 50);
            textBoxPlatform.TabIndex = 1;

            // textBoxUsername
            textBoxUsername.Location = new System.Drawing.Point(118, 12);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new System.Drawing.Size(200, 75);
            textBoxUsername.TabIndex = 2;

            // textBoxPassword
            textBoxPassword.Location = new System.Drawing.Point(224, 12);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new System.Drawing.Size(250, 90);
            textBoxPassword.TabIndex = 3;

            // addCredentialButton
            addCredentialButton.Location = new System.Drawing.Point(530, 12);
            addCredentialButton.Name = "addCredentialButton";
            addCredentialButton.Size = new System.Drawing.Size(75, 20);
            addCredentialButton.TabIndex = 4;
            addCredentialButton.Text = "Add";
            addCredentialButton.UseVisualStyleBackColor = true;
            addCredentialButton.Click += new System.EventHandler(addCredentialButton_Click);

            // generatePasswordButton
            generatePasswordButton.Location = new System.Drawing.Point(224, 24);
            generatePasswordButton.Name = "generatePasswordButton";
            generatePasswordButton.TabIndex = 5;
            generatePasswordButton.Text = "Generate Password";
            generatePasswordButton.UseVisualStyleBackColor = true;
            generatePasswordButton.Click += new System.EventHandler(generatePasswordButton_Click);

            // tableLayoutPanel
            tableLayoutPanel.ColumnCount = 4;

            //sets the size per colomn in percentage
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            //Adds lables to top row containing text for what each field below them should contain and aligns them with middle center
            tableLayoutPanel.Controls.Add(new Label { Text = "Platform", TextAlign = ContentAlignment.MiddleCenter }, 0, 0);
            tableLayoutPanel.Controls.Add(new Label { Text = "Username", TextAlign = ContentAlignment.MiddleCenter }, 1, 0);
            tableLayoutPanel.Controls.Add(new Label { Text = "Password", TextAlign = ContentAlignment.MiddleCenter }, 2, 0);
            
            //Adds textfields and addCredentialButton to second row (middle row)
            tableLayoutPanel.Controls.Add(textBoxPlatform, 0, 1);
            tableLayoutPanel.Controls.Add(textBoxUsername, 1, 1);
            tableLayoutPanel.Controls.Add(textBoxPassword, 2, 1);
            tableLayoutPanel.Controls.Add(addCredentialButton, 3, 1);

            //Adds generatePowerButton to the third row
            tableLayoutPanel.Controls.Add(generatePasswordButton, 2, 2);
            
            //Sets dockstyle to bottom, so the Gridview always is on top
            tableLayoutPanel.Dock = DockStyle.Bottom;
            
            //Create 3 rows in the tablelayout, one for lables, one for textboxes and add button and finally one for the password generation button
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.Size = new System.Drawing.Size(800, 100);

            // CredentialManagerForm
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(dataGridViewCredentials);
            Controls.Add(tableLayoutPanel);
            Name = "CredentialManagerForm";
            Text = "Credential Manager";
            Load += new System.EventHandler(CredentialManagerForm_Load);
            ResumeLayout(false);
        }
    }
}

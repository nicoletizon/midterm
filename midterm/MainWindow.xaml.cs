using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace midterm
{
    public partial class LoginForm : Window
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string inputUsername = txtUsername.Text;
            string inputPassword = txtPassword.Password;

            if (!string.IsNullOrEmpty(inputUsername) && !string.IsNullOrEmpty(inputPassword))
            {
                // Proceed to login
                if (AuthenticateUser(inputUsername, inputPassword))
                {
                    MessageBox.Show("Login Success", "Welcome Back", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    var inventoryWindows = new inventory();
                    this.Hide();
                    inventoryWindows.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username and/or Password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // Inform the user to input both username and password
                MessageBox.Show("Please input both username and password.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private bool AuthenticateUser(string inputUsername, string inputPassword)
        {
            var user = db.Table_Users.SingleOrDefault(u => u.UserID == inputUsername);

            return user != null && user.Password == inputPassword;
        }
    }
}

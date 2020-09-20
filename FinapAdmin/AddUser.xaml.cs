using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Finapp.Authentication;
using Finapp.Database;
using Microsoft.EntityFrameworkCore;

namespace Finapp.Admin
{
    /// <summary>
    ///     Logika interakcji dla klasy AddUser.xaml
    /// </summary>
    public partial class AddUser
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private async void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var userName = UserNameTextBox.Text;
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Wrong user name", "Problem", MessageBoxButton.OK, MessageBoxImage.Warning);
                UserNameTextBox.Background = Brushes.Red;
                return;
            }

            var context = new FinappDbContext();

            var user = await context.Users.FirstOrDefaultAsync(finapUser => finapUser.Name == userName);
            if (user != null)
            {
                MessageBox.Show("This user already exists", "Problem", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var passwordHash = PasswordManager.GeneratePassword(password);

            await context.Users.AddAsync(new FinappUser
            {
                Name = userName,
                PasswordHash = passwordHash
            });

            await context.SaveChangesAsync();

            Close();
        }

        private void UserNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UserNameTextBox.Background != default)
            {
                UserNameTextBox.Background = default;
            }
        }
    }
}
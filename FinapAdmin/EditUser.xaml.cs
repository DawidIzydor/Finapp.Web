using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Finapp.Authentication;
using Finapp.Database;
using Microsoft.EntityFrameworkCore;

namespace Finapp.Admin
{
    /// <summary>
    ///     Logika interakcji dla klasy AddUser.xaml
    /// </summary>
    public partial class EditUser
    {
        private readonly string _userName;

        public EditUser(string userName)
        {
            _userName = userName;
            var context = new FinappDbContext();
            User = context.Users.FirstOrDefault(u => u.Name == userName);

            InitializeComponent();
        }

        public FinappUser User { get; }

        private async void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            var password = PasswordBox.Password;

            var context = new FinappDbContext();
            var userNameLower = _userName.ToLowerInvariant();
            var user = await context.Users.SingleOrDefaultAsync(u => u.Name == userNameLower);

            if (password != "")
            {
                user.PasswordHash = PasswordManager.GeneratePassword(password);
            }

            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }

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
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Finapp.Database;

namespace Finapp.Admin
{
    /// <summary>
    ///     Logika interakcji dla klasy ShowUsers.xaml
    /// </summary>
    public partial class ShowUsers
    {
        public ShowUsers()
        {
            InitializeComponent();
        }

        public IEnumerable<FinappUser> Users
        {
            get
            {
                var context = new FinappDbContext();
                return context.Users.OrderBy(u => u.Name).ToList();
            }
        }

        private void ShowEditUser(object sender, RoutedEventArgs e)
        {
            var userName = (string) ((Button) sender).Tag;
            var editUserWindow = new EditUser(userName);
            editUserWindow.Show();
        }
    }
}
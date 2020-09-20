using System.Windows;

namespace Finapp.Admin
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private AddUser AddUserWindow => SingletonManager.Get<AddUser>();

        private ShowUsers ShowUsers => SingletonManager.Get<ShowUsers>();

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow.Show();
        }

        private void ShowUsersButton_Click(object sender, RoutedEventArgs e)
        {
            ShowUsers.Show();
        }
    }
}
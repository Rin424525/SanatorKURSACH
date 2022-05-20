using MySql.Data.MySqlClient;
using Sanator.ModelDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sanator.View
{
    /// <summary>
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string login = txtUsername.Text;
            string password = PasswordBox.Password;
            var db = MySqlDB.GetDB();
            bool isLogin = false, isEditUser = false;
            if (db.OpenConnection())
            {
                string querystring = $"select id, login, password, name from users where login = @login and password = @password";
                using (MySqlCommand command = new MySqlCommand(querystring, MySqlDB.GetDB().GetConnection()))
                {
                    command.Parameters.AddWithValue("login", login);
                    command.Parameters.AddWithValue("password", password);
                    using (var dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            isLogin = dr.GetInt32("id") != 0;

                        }

                    }
                }
                db.CloseConnection();
            }

            if (isLogin)
            {
                if (isEditUser)
                    new Password().ShowDialog();
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow1 main = new MainWindow1();
                Close();
                main.Show();
            }
            else
                MessageBox.Show("Ошибка!", $"Неверный логин или пароль!", MessageBoxButton.OK, MessageBoxImage.Warning);

        }
    }
}

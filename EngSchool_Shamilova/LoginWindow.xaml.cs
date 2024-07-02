using EngSchool_Shamilova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EngSchool_Shamilova
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new BD_ShamilovaEntities())
                {
                    List<User> users = context.User.ToList();
                    User u = users.FirstOrDefault(p => p.Password == TbPass.Password && p.Login == TbLogin.Text);
                    if (u != null)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.CurrentUser = u;
                        mainWindow.Owner = this;
                        this.Hide();
                        mainWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите закрыть приложение?",
                "Выйти", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (x == MessageBoxResult.Cancel) 
                e.Cancel = true;
        }
    }
}

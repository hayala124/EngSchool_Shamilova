using EngSchool_Shamilova.Models;
using System;
using System.Windows;
using EngSchool_Shamilova.Pages;

namespace EngSchool_Shamilova
{
    public partial class MainWindow : Window
    {
        public User CurrentUser { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new CatalogOfGoodsPage());
            Manager.MainFrame = MainFrame;
        }
        private void WindowClosed(object sender, EventArgs e)
        {
            Owner.Show();
        }
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?", "Выйти", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (x == MessageBoxResult.Cancel)
                e.Cancel = true;
        }
        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
        private void BtnEditGoodsClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentUser.Login == "admin")
                {
                    MainFrame.Navigate(new GoodsPage());
                }
                else
                {
                    MessageBox.Show("Просмотреть список товаров в табличном виде могут только администраторы", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void MainFrameContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
                BtnEditGoods.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnBack.Visibility = Visibility.Collapsed;
                BtnEditGoods.Visibility = Visibility.Visible;
            }
        }
    }
}

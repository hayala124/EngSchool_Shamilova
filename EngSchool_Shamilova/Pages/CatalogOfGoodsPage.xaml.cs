using EngSchool_Shamilova.Models;
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

namespace EngSchool_Shamilova.Pages
{
    public partial class CatalogOfGoodsPage : Page
    {
        int _itemCount = 0;
        public CatalogOfGoodsPage()
        {
            InitializeComponent();
            using (var context = new BD_ShamilovaEntities())
            {
                var developers = context.Manufacturer.OrderBy(p => p.Name).ToList();
                developers.Insert(0, new Manufacturer
                {
                    Name = "Все типы"
                }
                );
                ComboDeveloper.ItemsSource = developers;
                ComboDeveloper.SelectedIndex = 0;
                LViewGoods.ItemsSource = context.Product.OrderBy(p => p.Name).ToList();
                _itemCount = LViewGoods.Items.Count;
                TextBlockCount.Text = $"Результат запроса: {_itemCount} записей из {_itemCount}";
            }
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            using (var context = new BD_ShamilovaEntities())
                if (Visibility == Visibility.Visible)
                {
                    context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    LViewGoods.ItemsSource = context.Product.OrderBy(p => p.Name).ToList();
                }

        }

        // Поиск товаров, которые содержат данную поисковую строку.
        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        // Поиск товаров конкретного производителя.
        private void ComboTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }
        // Метод для фильтрации и сортировки данных.
        private void UpdateData()
        {
            using (var context = new BD_ShamilovaEntities())
            {
                var currentGoods = context.Product.OrderBy(p => p.Name).ToList();
                if (ComboDeveloper.SelectedIndex > 0)
                    currentGoods = currentGoods.Where(p => p.ManufacturerID == (ComboDeveloper.SelectedItem as Manufacturer).ID).ToList();
                currentGoods = currentGoods.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
                if (ComboSort.SelectedIndex >= 0)
                {
                    if (ComboSort.SelectedIndex == 0)
                        currentGoods = currentGoods.OrderBy(p => p.Price).ToList();
                    if (ComboSort.SelectedIndex == 1)
                        currentGoods = currentGoods.OrderByDescending(p => p.Price).ToList();
                }
                LViewGoods.ItemsSource = currentGoods;
                TextBlockCount.Text = $"Результат запроса: {currentGoods.Count} записей из {_itemCount}";
            }
        }
        // сортировка товаров.
        private void ComboSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }
    }
}

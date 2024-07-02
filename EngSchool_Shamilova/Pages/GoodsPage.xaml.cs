﻿using EngSchool_Shamilova.Models;
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
    public partial class GoodsPage : Page
    {
        public GoodsPage()
        {
            InitializeComponent();
            Goods.ItemsSource = BD_ShamilovaEntities.GetContext().Product.OrderBy(p => p.Name).ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
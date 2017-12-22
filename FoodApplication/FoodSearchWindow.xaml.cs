using FoodApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace FoodApplication
{
    /// <summary>
    /// Logika interakcji dla klasy FoodSearchWindow.xaml
    /// </summary>
    public partial class FoodSearchWindow : Window
    {
        private MainWindow _window;

        private CollectionView _foodView;


        //TODO ZROBIC TO OKNO szukanie, dodawanie, i szukanie po rodzaju ;)
        public FoodSearchWindow(MainWindow window)
        {
            _window = window;
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgFoods.ItemsSource = _window.Db.Foods.Local;

            //For filtering
            _foodView = (CollectionView)CollectionViewSource.GetDefaultView(dgFoods.ItemsSource);
            _foodView.Filter = FoodNameFilter;


        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private bool FoodNameFilter(Object item)
        {
            if (String.IsNullOrEmpty(txtName.Text))
                return true;
            else
                return ((item as Food).Name.IndexOf(txtName.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dgFoods.ItemsSource).Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(dgFoods.SelectedItem!=null)
            {
                Food temp = (Food)dgFoods.SelectedItem;
                _window.AddFoodToCollection(temp.FoodId);
                dgFoods.SelectedItem = null;
            }
        }
        //Edit window from foodsearch window
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgFoods.SelectedItem != null)
            {
                Food temp = (Food)dgFoods.SelectedItem;
                EditFoodWindow win = new EditFoodWindow(temp, _window);
                win.ShowDialog();
            }
        }
    }
}

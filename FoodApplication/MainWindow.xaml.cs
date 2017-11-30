using FoodApplication.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace FoodApplication
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FoodContext _db = new FoodContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource foodViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("foodViewSource")));
            // Załaduj dane poprzez ustawienie właściwości CollectionViewSource.Source:
            // foodViewSource.Źródło = [ogólne źródło danych]
            System.Windows.Data.CollectionViewSource mealViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("mealViewSource")));
            // Załaduj dane poprzez ustawienie właściwości CollectionViewSource.Source:
            // mealViewSource.Źródło = [ogólne źródło danych]
            _db.Foods.Load();
            _db.Meals.Load();

            mealViewSource.Source = _db.Meals.Local;
            foodViewSource.Source = _db.Foods.Local;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _db.Dispose();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _db.SaveChanges();
            foodDataGrid.Items.Refresh();
            mealDataGrid.Items.Refresh();
        }
    }

   
}

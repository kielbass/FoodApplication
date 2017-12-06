using FoodApplication.Models;
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
using System.Windows.Shapes;

namespace FoodApplication
{
    /// <summary>
    /// Logika interakcji dla klasy EditFoodWindow.xaml
    /// </summary>
    partial class EditFoodWindow : Window
    {
        private Food _food;
        private MainWindow _window;
        public EditFoodWindow(Food food,MainWindow window)
        {
            _food = food;
            _window = window;
            InitializeComponent();

            SetProperties();
        }
        private void  SetProperties()
        {
            txtName.Text = _food.Name;
            txtCarbs.Text = _food.Carbs.ToString();
            txtFat.Text = _food.Fat.ToString();
            txtProteins.Text = _food.Proteins.ToString();
            txtKcal.Text = _food.Kcal.ToString();
            txtKind.Text = _food.Kind;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (FoodTextsCheck())
            {
                MessageBoxResult r = MessageBox.Show("Czy na pewno chcesz edytować?", _food.Name, MessageBoxButton.YesNo);
                if (r == MessageBoxResult.Yes)
                {
                    float.TryParse(txtKcal.Text, out float a);
                    float.TryParse(txtFat.Text, out float b);
                    float.TryParse(txtProteins.Text, out float c);
                    float.TryParse(txtCarbs.Text, out float d);

                    _food.Name = txtName.Text;
                    _food.Kind = txtName.Text;
                    _food.Kcal = a;
                    _food.Fat = b;
                    _food.Proteins = c;
                    _food.Carbs = d;
                    Close();
                    }
                }
        }
            private bool FoodTextsCheck()
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtKind.Text))
                return false;
            if (!float.TryParse(txtKcal.Text, out float a))
                return false;
            if (!float.TryParse(txtFat.Text, out float b))
                return false;
            if (!float.TryParse(txtProteins.Text, out float c))
                return false;
            if (!float.TryParse(txtCarbs.Text, out float d))
                return false;
            return true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _window.SaveAndRefresh();
        }
    }
}

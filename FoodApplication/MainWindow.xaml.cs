﻿using FoodApplication.Models;
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
        public void SaveAndRefresh()
        {
            _db.SaveChanges();
            foodDataGrid.Items.Refresh();
            mealDataGrid.Items.Refresh();
        }
        /// <summary>
        /// Checking if textboxes have correct values, and are not empty.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Making Food object from textboxes.
        /// </summary>
        /// <returns>Food object made from textboxes.</returns>
        private Food MakeFoodFromBoxes()
        {
            float.TryParse(txtKcal.Text, out float a);
            float.TryParse(txtFat.Text, out float b);
            float.TryParse(txtProteins.Text, out float c);
            float.TryParse(txtCarbs.Text, out float d);

            Food temp= new Food()
            {
                Name = txtName.Text,
                Kind = txtName.Text,
                Kcal = a,
                Fat = b,
                Proteins = c,
                Carbs = d
            };
            return temp;
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(FoodTextsCheck())
            {
                if(_db.Foods.Select(f => f.Name).Where(f=>f.Contains(txtName.Text)).Count() >0)
                {

                    MessageBoxResult r = MessageBox.Show("Produkt o nazwie: " + txtName.Text + " istnieje, czy na pewno chcesz go dodać?", txtName.Text, MessageBoxButton.YesNo);
                    if(r == MessageBoxResult.Yes)
                    {
                        _db.Foods.Add(MakeFoodFromBoxes());
                        SaveAndRefresh();
                    }
                }
                else
                {
                    _db.Foods.Add(MakeFoodFromBoxes());
                    SaveAndRefresh();
                }

            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(foodDataGrid.SelectedItem!=null)
            {
                Food temp = (Food)foodDataGrid.SelectedItem;
                if (FoodTextsCheck())
                {
                    MessageBoxResult r = MessageBox.Show("Czy edytować?", "Pytanie", MessageBoxButton.YesNo);
                    if (r == MessageBoxResult.Yes)
                    {
                        Food temp2 = MakeFoodFromBoxes();

                        temp.Carbs = temp2.Carbs;
                        temp.Proteins = temp2.Proteins;
                        temp.Fat = temp2.Fat;
                        temp.Kcal = temp2.Kcal;
                        temp.Name = temp2.Name;
                        temp.Kind = temp2.Kind;
                        SaveAndRefresh();
                    }

                }
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(foodDataGrid.SelectedItem!=null)
            {
                Food temp = (Food)foodDataGrid.SelectedItem;
                EditFoodWindow win = new EditFoodWindow(temp,this);
                win.ShowDialog();
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if(foodDataGrid.SelectedItem!=null)
            {
                MessageBoxResult r = MessageBox.Show("Napewno chcesz usunać?", "Usuwanko?", MessageBoxButton.YesNo);
                if(r == MessageBoxResult.Yes)
                {
                    Food temp = (Food)foodDataGrid.SelectedItem;
                    _db.Foods.Remove(temp);
                    SaveAndRefresh();
                }
            }
        }
    }

   
}

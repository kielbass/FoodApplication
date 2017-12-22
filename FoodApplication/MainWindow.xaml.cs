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
using FoodApplication.Classes;
using System.Collections.ObjectModel;

namespace FoodApplication
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FoodContext _db = new FoodContext();

        public FoodContext Db
        {
            get
            {
                return _db;
            }
        }
        private FoodSearchWindow _foodWin;

        private CollectionView _foodView;

        private ObservableCollection<ReadyMeal> _meals = new ObservableCollection<ReadyMeal>();

        private ObservableCollection<FoodIdWeightPair> _foodIdWeghhstPairList = new ObservableCollection<FoodIdWeightPair>();
        private ObservableCollection<Food> _foodMealList = new ObservableCollection<Food>();
        
        public MainWindow()
        {

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource foodViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("foodViewSource")));
            System.Windows.Data.CollectionViewSource mealViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("mealViewSource")));

            _db.Foods.Load();
            _db.Meals.Load();

            mealViewSource.Source = _db.Meals.Local;
            foodViewSource.Source = _db.Foods.Local;

            //For filtering in foodDatagrid
            _foodView = (CollectionView)CollectionViewSource.GetDefaultView(foodDataGrid.ItemsSource);
            _foodView.Filter = FoodNameFilter;

            //meals initialize
            CreateMealsForCollection();

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _db.Dispose();
            if (_foodWin != null && _foodWin.IsLoaded) _foodWin.Close(); 
        }
        public void SaveAndRefresh()
        {
            _db.SaveChanges();
            foodDataGrid.Items.Refresh();
            dgFoodsForMeal.Items.Refresh();
            dgFoodsWeight.Items.Refresh();
            _foodWin.dgFoods.Items.Refresh();
        }
        //TODO ZROBIC LISTE FOODSOW I DODAWAC JE I ODEJMOWAC
        private void CreateMealsForCollection()
        {
            dgMealsGrid.ItemsSource = _meals;
            dgFoodsForMeal.ItemsSource = _foodMealList;
            dgFoodsWeight.ItemsSource = _foodIdWeghhstPairList;
        }
        public void AddFoodToCollection(long i)
        {
            if (_foodIdWeghhstPairList.ToList().Find(x => x.FoodId == i) == null)
            {
                _foodIdWeghhstPairList.Add(new FoodIdWeightPair() { FoodId = i, FoodWeight = 0f });
                Food[] temp = _db.Foods.Select(x => x).Where(x => x.FoodId == i).ToArray();

                _foodMealList.Add(temp[0]);
            }
            else
            {
                Food[] temp = _db.Foods.Select(x => x).Where(x => x.FoodId == i).ToArray();

                MessageBox.Show("Dodałeś już " + temp[0].Name + "!","Powtóreczka?");
            }

        }
        private void btnAddFoods_Click(object sender, RoutedEventArgs e)
        {
            if(_foodWin==null)
            {
                _foodWin = new FoodSearchWindow(this);
                _foodWin.Show();
            }
            else if(!_foodWin.IsLoaded)
            {
                _foodWin = new FoodSearchWindow(this);
                _foodWin.Show();
            }
            else
            {
                _foodWin.Focus();
            }

        }
        //Context Menu Remove from Food collections in Meals edit
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (dgFoodsForMeal.SelectedItem != null)
            {
                Food temp = (Food)dgFoodsForMeal.SelectedItem;
                int i = _foodMealList.IndexOf(temp);
                _foodMealList.RemoveAt(i);
                _foodIdWeghhstPairList.RemoveAt(i);
            }

        }

        #region FOOD TAB
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
            if (!float.TryParse(txtPackageWeight.Text, out float e))
                return false;
            if (chkPackage.IsChecked == null)
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
            float.TryParse(txtPackageWeight.Text, out float e);
            bool f = (bool)chkPackage.IsChecked;

            Food temp = new Food()
            {
                Name = txtName.Text,
                Kind = txtKind.Text,
                Kcal = a,
                Fat = b,
                Proteins = c,
                Carbs = d,
                PackageWeight = e,
                IsUsed = true,
                Package = f

            };
            return temp;
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(FoodTextsCheck())
            {
                if(_db.Foods.Select(f => f.Name).Where(f=>f.Equals(txtName.Text)).Count() >0)
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

        
        /// <summary>
        /// Edit in new window. Just because i can :).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(foodDataGrid.SelectedItem!=null)
            {
                Food temp = (Food)foodDataGrid.SelectedItem;
                EditFoodWindow win = new EditFoodWindow(temp,this);
                win.ShowDialog();
            }
        }
        /// <summary>
        /// Deleting item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if(foodDataGrid.SelectedItem!=null)
            {
                if(!((Food)foodDataGrid.SelectedItem).IsUsed)
                {
                    MessageBoxResult r = MessageBox.Show("Napewno chcesz usunać?", "Usuwanko?", MessageBoxButton.YesNo);
                    if (r == MessageBoxResult.Yes)
                    {
                        Food temp = (Food)foodDataGrid.SelectedItem;
                        _db.Foods.Remove(temp);
                        SaveAndRefresh();
                    }
                }
                else
                {
                    MessageBox.Show("Nie możesz usunąć elementu, który został już raz użyty","STOP!");
                }
            }
        }
        private bool FoodNameFilter(Object item)
        {
            if (String.IsNullOrEmpty(txtFind.Text))
                return true;
            else
                return ((item as Food).Name.IndexOf(txtFind.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void txtFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(foodDataGrid.ItemsSource).Refresh();
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logika interakcji dla klasy PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {
        MainWindow _window;
        public PersonWindow(MainWindow win)
        {
            _window = win;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgPerson.ItemsSource = _window.Db.Persons.Local;
            dataPerson.SelectedDate = DateTime.Today;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_window.Db.Persons.Select(x=>x).Where(x=>x.Date == dataPerson.SelectedDate).Count()==0)
            {
                if (float.TryParse(txtPerson.Text, out float a))
                {
                    if(float.TryParse(txtProteins.Text,out float c))
                    {
                        if (float.TryParse(txtBilans.Text, out float b))
                            _window.Db.Persons.Add(new Models.Person
                            {
                                Weight = a,
                                Date = dataPerson.SelectedDate,
                                Bilans = b,
                                Proteins = c
                            });
                        _window.SaveAndRefresh();
                        dgPerson.Items.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Nie możesz dodać drugiego wpisu dla tego samego dnia!","EROR");
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_window.Db.Persons.Count()==0)
            {
                MessageBox.Show("Aby program działał poprawnie należy dodać przynajmniej jeden wpis do tabeli!", "ORDER 66");
                PersonWindow temp = new PersonWindow(_window);
                temp.ShowDialog();
            }
        }
        /// <summary>
        /// remove from tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgPerson.SelectedItem != null)
            {
                Models.Person temp = (Models.Person)dgPerson.SelectedItem;
                _window.Db.Persons.Remove(temp);
                _window.SaveAndRefresh();
                dgPerson.SelectedItem = null;
            }
        }
    }
}

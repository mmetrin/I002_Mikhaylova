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

namespace Storage
{
    /// <summary>
    /// Логика взаимодействия для EntityCounteragent.xaml
    /// </summary>
    public partial class EntityCounteragent : Page
    {
        ModelStorage db = new ModelStorage();
        public EntityCounteragent()
        {
            InitializeComponent();
            Load();
        }
        public void Load()
        {
            TableCounteragents.ItemsSource = db.Сounteragent.Where(p => p.Role == 1 && p.Removed == false).ToList();
        }

        private void DeleteCountreagentBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить контрагента?", "Удаление",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result==MessageBoxResult.Yes)
            {
                Сounteragent Btn = (sender as Button).DataContext as Сounteragent;
                var сounteragent = db.Сounteragent.Where(p => p.IdСounteragent == Btn.IdСounteragent).FirstOrDefault();
                сounteragent.Removed = true;
                db.SaveChanges();
                TableCounteragents.ItemsSource = db.Сounteragent.Where(p => p.Role == Btn.Role && p.Removed==false).ToList();
            }
            
        }

        private void ChangeCounteragentBtn_Click(object sender, RoutedEventArgs e)
        {
            Сounteragent Btn = (sender as Button).DataContext as Сounteragent;
            ChangeCounteagent changeCounteagent = new ChangeCounteagent(Btn.IdСounteragent, Btn.Name, Btn.Surname, Btn.MiddleName, Btn.INN, Btn.Adress, Btn.Phone, Btn.Role, TableCounteragents);
            changeCounteagent.ShowDialog();
        }

        private void AddCounteragent_Click(object sender, RoutedEventArgs e)
        {
            AddCounteragent add = new AddCounteragent(TableCounteragents);
            add.ShowDialog();
        }

        private void RadioSeller_Click(object sender, RoutedEventArgs e)
        {
            RadioSeller.IsChecked = true;
            RadioBuyer.IsChecked = false;
            TableCounteragents.ItemsSource = db.Сounteragent.Where(p => p.Role == 1 && p.Removed == false).ToList();
        }

        private void RadioBuyer_Click(object sender, RoutedEventArgs e)
        {
            RadioSeller.IsChecked = false;
            RadioBuyer.IsChecked = true;
            TableCounteragents.ItemsSource = db.Сounteragent.Where(p => p.Role == 2 && p.Removed == false).ToList();
        }
    }
}

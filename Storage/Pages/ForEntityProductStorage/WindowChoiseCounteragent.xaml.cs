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

namespace Storage
{
    /// <summary>
    /// Логика взаимодействия для WindowChoiseCounteragent.xaml
    /// </summary>
    public partial class WindowChoiseCounteragent : Window
    {
        ModelStorage db = new ModelStorage();
        int Role;
        public WindowChoiseCounteragent(int role)
        {
            InitializeComponent();
            Role = role;
            Load();
        }
        public void Load()
        {
            TableCounteragents.ItemsSource = db.Сounteragent.Where(p => p.Role == Role && p.Removed == false).ToList();
        }
        private void ChoiseCountreagentBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = (sender as Button).DataContext as Сounteragent;
            if (Role==1) OpenClass.DialogProduct.ReadCounteragent(btn.IdСounteragent.ToString(), btn.Surname, btn.Name, btn.MiddleName, btn.INN);
            else OpenClass.SellProduct.ReadCounteragent(btn.IdСounteragent.ToString(), btn.Surname, btn.Name, btn.MiddleName, btn.INN);
            this.Close();
            GC.Collect();
        }
    }
}

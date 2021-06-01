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
    /// Логика взаимодействия для EntityPurchase.xaml
    /// </summary>
    public partial class EntityPurchase : Page
    {
        ModelStorage db = new ModelStorage();
        public EntityPurchase()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            var ReportList = db.Purchase.Join(db.Сounteragent, p => p.IDCounteragent, c => c.IdСounteragent,
                (p, c) => new { Date = p.DatePurchase, IdPurchase = p.IDPurchase, IdCounteragent = p.IDCounteragent, Name = c.Name, Surname = c.Surname, MiddleName = c.MiddleName, INN = c.INN });

            foreach (var a in ReportList)
            {
                ReportPurchase rep = new ReportPurchase {Date = a.Date.ToShortDateString(), IdPurchase = a.IdPurchase, IdCounteragent = a.IdCounteragent, Name = a.Name, Surname = a.Surname, MiddleName = a.MiddleName, INN = a.INN };
                Tablereport.Items.Add(rep);
            }
        }

        private void ChoiseBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = (sender as Button).DataContext as ReportPurchase;
            ForRepPurchase forRepPurchase = new ForRepPurchase(btn.IdPurchase,2);
            forRepPurchase.ShowDialog();
        }
    }
}

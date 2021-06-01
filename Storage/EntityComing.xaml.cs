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
    /// Логика взаимодействия для EntityComing.xaml
    /// </summary>
    public partial class EntityComing : Page
    {
        ModelStorage db = new ModelStorage();

        public EntityComing()
        {
            InitializeComponent();
            Load();

        }
        public void Load()
        {
            var ReportList = db.Coming.Join(db.Сounteragent, p => p.IDСounteragent, c => c.IdСounteragent,
                (p, c) => new { Date = p.DateComing, IdComing = p.IDComing, IdCounteragent = p.IDСounteragent, Name = c.Name, Surname = c.Surname, MiddleName = c.MiddleName, INN = c.INN });

            foreach (var a in ReportList)
            {
                ReportPurchase rep = new ReportPurchase { Date = a.Date.ToShortDateString(), IdPurchase = a.IdComing, IdCounteragent = Convert.ToInt32(a.IdCounteragent), Name = a.Name, Surname = a.Surname, MiddleName = a.MiddleName, INN = a.INN };
                Tablereport.Items.Add(rep);
            }
        }
        private void ChoiseBtn_Click(object sender, RoutedEventArgs e)
        {

            var btn = (sender as Button).DataContext as ReportPurchase;
            ForRepPurchase forRepPurchase = new ForRepPurchase(btn.IdPurchase,1);
            forRepPurchase.ShowDialog();
        }
    }
}

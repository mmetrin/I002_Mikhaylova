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
    /// Логика взаимодействия для ForRepPurchase.xaml
    /// </summary>
    public partial class ForRepPurchase : Window
    {
        ModelStorage db = new ModelStorage();
        int IdPur;
        int Action;
        public ForRepPurchase(int id,int action)
        {
            InitializeComponent();
            IdPur = id;
            Action = action;
            Load();
        }
        public void Load()
        {
            if (Action == 1)
            {
                Tablereport.ItemsSource = db.ProductComing.Join(db.Product, pp => pp.IDProduct, p => p.ArticleNumber,
               (pp, p) => new { Id = pp.IDComing, Name = p.Name, Price = pp.Price, Quantity = pp.Quantity }).Where(a => a.Id == IdPur).ToList();
            }
            else 
            {
                Tablereport.ItemsSource = db.ProductPurchase.Join(db.Product, pp => pp.IDProduct, p => p.ArticleNumber,
               (pp, p) => new { Id = pp.IDPurchase, Name = p.Name, Price = pp.Price, Quantity = pp.Quantity }).Where(a => a.Id == IdPur).ToList();
            }
           
        }
    }
}

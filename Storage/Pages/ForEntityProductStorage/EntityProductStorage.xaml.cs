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
    /// Логика взаимодействия для EntityProductStorage.xaml
    /// </summary>
    public partial class EntityProductStorage : Page
    {
        ModelStorage db = new ModelStorage();
        public EntityProductStorage()
        {
            InitializeComponent();
            OpenClass.RefreshTable = this;
            Load();
        }
        public void Load()
        {
            TableProductOnStorage.ItemsSource = db.ProductOnStorage.Join(db.Product, Ps => Ps.IdProduct, Pr => Pr.ArticleNumber, 
                (Ps, Pr) => new { Name = Pr.Name, Quantity = Ps.Quantity,Price = Ps.Price}).ToList();
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            BuyProduct buyProduct = new BuyProduct();
            buyProduct.ShowDialog();
        }

        private void SellBtn_Click(object sender, RoutedEventArgs e)
        {
            SellProduct sellProduct = new SellProduct();
            sellProduct.ShowDialog();
        }

        
    }
}

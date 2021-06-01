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
    /// Логика взаимодействия для ProductAdd.xaml
    /// </summary>
    public partial class ProductAdd : Window
    {
        ModelStorage db = new ModelStorage();
        ListView TableProducts;
        public ProductAdd( ListView table)
        {
            InitializeComponent();
            TableProducts = table;
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            string NamePr = TxtForProduct.Text;
            if (NamePr != String.Empty)
            {
                Product product = new Product();
                product.Name = NamePr;
                product.Removed = false;
                db.Product.Add(product);
                db.SaveChanges();
                TableProducts.ItemsSource = db.Product.Where(p => p.Removed == false).ToList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не ввели данные!");
            }
        }
    }
}

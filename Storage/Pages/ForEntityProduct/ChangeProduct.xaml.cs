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
    /// Логика взаимодействия для ChangeProduct.xaml
    /// </summary>
    public partial class ChangeProduct : Window
    {
        ModelStorage db = new ModelStorage();
        int Number;
        ListView TableProducts;
        public ChangeProduct(string ProductName, int num, ListView table)
        {
            InitializeComponent();
            TxtForProduct.Text = ProductName;
            Number = num;
            TableProducts = table;

        }

        private void BtnChangeProduct_Click(object sender, RoutedEventArgs e)
        {
            string NamePr = TxtForProduct.Text;
            if (NamePr!=String.Empty)
            {
                var product = db.Product.Where(p => p.ArticleNumber == Number).FirstOrDefault();
                product.Name = NamePr;
                db.SaveChanges();
                TableProducts.ItemsSource =  db.Product.Where(p => p.Removed == false).ToList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не ввели данные!");
            }
        }
    }
}





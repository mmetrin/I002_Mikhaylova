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
    /// Логика взаимодействия для EntityProduct.xaml
    /// </summary>
    public partial class EntityProduct : Page
    {
        ModelStorage db = new ModelStorage();
        public EntityProduct()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            TableProducts.ItemsSource = db.Product.Where(p => p.Removed == false).ToList();
        }

        private void DeleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот продукт?", "Удаление",
               MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Product Btn = (sender as Button).DataContext as Product;
                var product = db.Product.Where(p => p.ArticleNumber == Btn.ArticleNumber).FirstOrDefault();
                product.Removed = true;
                db.SaveChanges();
                TableProducts.ItemsSource = db.Product.Where(p => p.Removed == false).ToList();
            }
        }

        private void ChangeProductBtn_Click(object sender, RoutedEventArgs e)
        {
            Product Btn = (sender as Button).DataContext as Product;
            var product = db.Product.Where(p => p.ArticleNumber == Btn.ArticleNumber).FirstOrDefault();
            ChangeProduct changeProduct = new ChangeProduct(Btn.Name, product.ArticleNumber, TableProducts);
            changeProduct.ShowDialog();

        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            ProductAdd addProduct = new ProductAdd(TableProducts);
            addProduct.ShowDialog();
        }
    }
}

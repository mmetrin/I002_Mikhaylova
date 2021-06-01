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
    /// Логика взаимодействия для SellProduct.xaml
    /// </summary>
    public partial class SellProduct : Window
    {
        ModelStorage db = new ModelStorage();
        int IDCounteragent;

        public SellProduct()
        {
            InitializeComponent();
            OpenClass.SellProduct = this;

        }
        public void ReadCounteragent(string counteragent, string surname, string name, string middleName, string Inn)
        {
            if (counteragent != null)
            {
                TxtCounteeragent.Text = "";
                IDCounteragent = Convert.ToInt32(counteragent);
                TxtCounteeragent.Text += surname + " ";
                TxtCounteeragent.Text += name + " ";
                TxtCounteeragent.Text += middleName + "  ИНН:";
                TxtCounteeragent.Text += Inn;
            }

        }

        private void ChoiseCounteragent_Click(object sender, RoutedEventArgs e)
        {
            WindowChoiseCounteragent windowChoise = new WindowChoiseCounteragent(2);
            windowChoise.ShowDialog();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (TxtCounteeragent.Text != String.Empty)
            {
                DialogSellProduct sellProduct = new DialogSellProduct(TableProductOnStorage);
                sellProduct.ShowDialog();
            }
            else MessageBox.Show("Сначала выберите контрагента!");

        }

        private void DeleteProduct_Click_1(object sender, RoutedEventArgs e)
        {
            if (TableProductOnStorage.SelectedItem != null)
            {
                TableProductOnStorage.Items.Remove(TableProductOnStorage.SelectedItem);
            }
            else MessageBox.Show("Выберите товар для удаления");
        }

        private void BtnSellProduct_Click(object sender, RoutedEventArgs e)
        {
            if (TableProductOnStorage.HasItems)
            {
                Purchase purchase = new Purchase();
                purchase.IDCounteragent = IDCounteragent;
                purchase.DatePurchase = DateTime.Now;
                db.Purchase.Add(purchase);
                db.SaveChanges();
                int MaxPurchase = db.Purchase.OrderByDescending(x => x.IDPurchase).FirstOrDefault().IDPurchase;
                foreach (ContainerItem item in TableProductOnStorage.Items)
                {
                    ProductPurchase productPurchase = new ProductPurchase { IDPurchase = MaxPurchase, IDProduct = item.Id, Price = Convert.ToDecimal(item.Price), Quantity = Convert.ToInt32(item.Quantity) };
                    db.ProductPurchase.Add(productPurchase);
                    db.SaveChanges();
                }
                foreach (ContainerItem item in TableProductOnStorage.Items)
                {
                    int QuantityInTable =item.Quantity;
                    int QuantityInStorage = db.ProductOnStorage.Where(p => p.IdProduct == item.Id).FirstOrDefault().Quantity;
                    int NewQuantity = QuantityInStorage - QuantityInTable;
                    if (NewQuantity != 0)
                    {
                        ProductOnStorage product = db.ProductOnStorage.Where(p => p.IdProduct == item.Id).FirstOrDefault();
                        product.Quantity = NewQuantity;
                        db.SaveChanges();
                    }
                    else 
                    {
                        ProductOnStorage product = db.ProductOnStorage.Where(p => p.IdProduct == item.Id).FirstOrDefault();
                        db.ProductOnStorage.Remove(product);
                        db.SaveChanges();
                    }

                }
                OpenClass.RefreshTable.Load();
                MessageBox.Show("Товар успешно продан!");
                this.Close();
            }
            else MessageBox.Show("Вы не выбрали ни один товар!");
        }

       
    }
}

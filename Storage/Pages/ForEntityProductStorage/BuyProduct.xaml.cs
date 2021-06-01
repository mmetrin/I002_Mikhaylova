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
    /// Логика взаимодействия для BuyProduct.xaml
    /// </summary>
    public partial class BuyProduct : Window
    {
        ModelStorage db = new ModelStorage();
        int IDCounteragent;

        public BuyProduct()
        {
            InitializeComponent();
            OpenClass.DialogProduct = this;
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
            WindowChoiseCounteragent windowChoise = new WindowChoiseCounteragent(1);
            windowChoise.ShowDialog();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (TxtCounteeragent.Text != String.Empty)
            {
                WindowDialogForProduct windowDialogFor = new WindowDialogForProduct(TableProductOnStorage);
                windowDialogFor.ShowDialog();
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

        private void BtnBuyProduct_Click(object sender, RoutedEventArgs e)
        {
            if (TableProductOnStorage.HasItems)
            {
                Coming coming = new Coming();
                coming.IDСounteragent = IDCounteragent;
                coming.DateComing = DateTime.Now;
                db.Coming.Add(coming);
                db.SaveChanges();
                int MaxComing = db.Coming.OrderByDescending(x => x.IDComing).FirstOrDefault().IDComing;
                foreach (ContainerItem item in TableProductOnStorage.Items)
                {
                    ProductComing productComing = new ProductComing { IDComing = MaxComing, IDProduct = item.Id, Price = Convert.ToDouble(item.Price), Quantity = Convert.ToInt32(item.Quantity) };
                    db.ProductComing.Add(productComing);
                    db.SaveChanges();
                }
                foreach (ContainerItem item in TableProductOnStorage.Items)
                {
                    if (db.ProductOnStorage.Where(p => p.IdProduct == item.Id).FirstOrDefault() != null)
                    {
                        double NewPrice = Convert.ToDouble(item.Price);
                        int NewQuantity = Convert.ToInt32(item.Quantity);
                        double PriceInStorage = db.ProductOnStorage.Where(p => p.IdProduct == item.Id).FirstOrDefault().Price;
                        int QuantityInStorage = db.ProductOnStorage.Where(p => p.IdProduct == item.Id).FirstOrDefault().Quantity;
                        var Product = db.ProductOnStorage.Where(p => p.IdProduct == item.Id).FirstOrDefault();
                        Product.Price = Math.Round((PriceInStorage * QuantityInStorage + NewPrice * NewQuantity) / (NewQuantity + QuantityInStorage), 2);
                        Product.Quantity = NewQuantity + QuantityInStorage;
                        db.SaveChanges();
                    }
                    else
                    {
                        ProductOnStorage productOnStorage = new ProductOnStorage { IdProduct = item.Id, Quantity = Convert.ToInt32(item.Quantity), Price = Convert.ToDouble(item.Price) };
                        db.ProductOnStorage.Add(productOnStorage);
                        db.SaveChanges();
                    }
                }
                OpenClass.RefreshTable.Load();
                MessageBox.Show("Товар успешно закуплен!");
                this.Close();
            }
            else MessageBox.Show("Вы не выбрали ни один товар!");


        }
    }
}

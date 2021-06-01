using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для DialogSellProduct.xaml
    /// </summary>
    /// 
    public partial class DialogSellProduct : Window
    {
        public string prevTextBox2 = "";
        public int prevIndexTextbox2 = 0;
        ModelStorage db = new ModelStorage();
        ListView TableForProduct;
        int IDProduct;
        string NameProduct;
        int Quantity;

        public DialogSellProduct(ListView table)
        {
            InitializeComponent();
            Load();
            TableForProduct = table;
        }
        public void Load()
        {
           var ProductsList= db.ProductOnStorage.Join(db.Product, Ps => Ps.IdProduct, Pr => Pr.ArticleNumber,
                (Ps, Pr) => new {Id = Ps.IdProduct, Name = Pr.Name, Quantity = Ps.Quantity, Price = Ps.Price }).ToList();
            foreach(var a in ProductsList)
            {
                ForProductStorage productStorage = new ForProductStorage {Id=a.Id,Name = a.Name, Price = a.Price,Quantity = a.Quantity };
                TableProducts.Items.Add(productStorage);
            }

        }

        private void ChoiseProduct_Click(object sender, RoutedEventArgs e)
        {
            var btn = (sender as Button).DataContext as ForProductStorage;
            ForPrice.Text =Convert.ToString(btn.Price);
            NameProduct = db.Product.Where(p => p.ArticleNumber == btn.Id).FirstOrDefault().Name;
            ForProduct.Text = NameProduct;
            IDProduct = Convert.ToInt32(btn.Id);
            Quantity = btn.Quantity;
        }
        private void ChoiseProductMain_Click(object sender, RoutedEventArgs e)
        {

            if (ForProduct.Text != String.Empty && ForQuantity.Text != String.Empty)
            {
                if (Convert.ToInt32(ForQuantity.Text) <= Quantity && Convert.ToInt32(ForQuantity.Text) > 0)
                {
                    bool check = false;
                    foreach (ContainerItem item in TableForProduct.Items)
                    {

                        if (item.Id == IDProduct)
                        {
                            MessageBox.Show("Вы уже выбрали такой товар!");
                            check = true;
                        }
                    }
                    if (!check)
                    {
                        ContainerItem it = new ContainerItem();
                        it.Id = IDProduct;
                        it.Name = NameProduct;
                        it.Quantity = Convert.ToInt32(ForQuantity.Text);
                        it.Price = Convert.ToDouble(ForPrice.Text);
                        TableForProduct.Items.Add(it);

                    }
                    this.Close();
                }

                else
                {
                    MessageBox.Show($"Такого количества товара нет на складе!\n Максимальное количество: {Quantity}");

                }


            }
            else MessageBox.Show("Вы не заполнили поля!");


        }
        private void ForQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void ForPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}

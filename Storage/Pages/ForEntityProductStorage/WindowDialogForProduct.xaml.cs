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
    /// Логика взаимодействия для WindowDialogForProduct.xaml
    /// </summary>
    public partial class WindowDialogForProduct : Window
    {
        public string prevTextBox2 = "";
        public int prevIndexTextbox2 = 0;
        ModelStorage db = new ModelStorage();
        ListView TableForProduct;
        int IDProduct;
        string NameProduct;
        public WindowDialogForProduct(ListView table)
        {
            InitializeComponent();
            Load();
            TableForProduct = table;
        }
        public void Load()
        {
            TableProducts.ItemsSource = db.Product.Where(p => p.Removed == false).ToList();
        }

        private void ChoiseProduct_Click(object sender, RoutedEventArgs e)
        {
            var btn = (sender as Button).DataContext as Product;
            ForProduct.Text = NameProduct= btn.Name;
            IDProduct = btn.ArticleNumber;


        }

        private void ChoiseProductMain_Click(object sender, RoutedEventArgs e)
        {

            if (ForProduct.Text != String.Empty && ForQuantity.Text != String.Empty && ForPrice.Text != String.Empty)
            {
                bool check = false;
                foreach (ContainerItem item in TableForProduct.Items)
                {

                    if (item.Id== IDProduct)
                    {
                        double PriceInTable = Convert.ToDouble(item.Price);
                        int QuantityInTable = Convert.ToInt32(item.Quantity);
                        double NewPrice = Convert.ToDouble(ForPrice.Text.Replace('.', ','));
                        int NewQuantity = Convert.ToInt32(ForQuantity.Text);
                        item.Price = Math.Round((PriceInTable * QuantityInTable + NewPrice * NewQuantity)/(QuantityInTable+ NewQuantity),2);
                        item.Quantity = (QuantityInTable + NewQuantity);
                        TableForProduct.Items.Refresh();
                        check = true;
                    }
                }
                if (!check)
                {
                    ContainerItem it = new ContainerItem();
                    it.Id = IDProduct;
                    it.Name = NameProduct;
                    it.Quantity =Convert.ToInt32(ForQuantity.Text);
                    it.Price = Convert.ToDouble(ForPrice.Text.Replace('.',','));
                    TableForProduct.Items.Add(it);

                }
                this.Close();
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
            var tb = (TextBox)sender;
            // Здесь возможно попадется десятичная запятая
            var f = CultureInfo.CurrentUICulture.NumberFormat;

            // Жестко задаем десятичную точку
            f = CultureInfo.GetCultureInfo("en-US").NumberFormat;

            var str = tb.Text;
            var regex = new Regex($"^\\{f.NegativeSign}?\\d*(\\{f.CurrencyDecimalSeparator}\\d*)?$");
            if (regex.IsMatch(str))
            {
                prevTextBox2 = str;
                prevIndexTextbox2 = tb.CaretIndex;
            }
            else
            {
                var savedPrevIndex = prevIndexTextbox2;
                tb.Text = prevTextBox2;
                prevIndexTextbox2 = savedPrevIndex;
                tb.CaretIndex = savedPrevIndex;
            }
        }
    }
 }


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
    /// Логика взаимодействия для AddCounteragent.xaml
    /// </summary>
    public partial class AddCounteragent : Window
    {
        ModelStorage db = new ModelStorage();
        ListView TableCounteragents;

        public AddCounteragent(ListView Table)
        {
            InitializeComponent();
            TableCounteragents = Table;
        }

        private void BtnAddCounteragent_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Выберите роль:\n Yes - Поставщик \n No - ПОкупатель", "Удаление",
                 MessageBoxButton.YesNo, MessageBoxImage.Question);
            int role;
            if (result == MessageBoxResult.Yes) role = 1;

            else role = 2;
            
            
            if (TxtForName.Text!=String.Empty && TxtForSurname.Text!= String.Empty&& TxtForMiddle.Text != String.Empty&& TxtForInn.Text != String.Empty&& TxtForPhone.Text!= String.Empty)
            {
                var checkCounteragent = db.Сounteragent.Where(p => p.INN == TxtForInn.Text).FirstOrDefault();
                if (checkCounteragent==null)
                {
                    Сounteragent сounteragent = new Сounteragent();
                    сounteragent.Name = TxtForName.Text;
                    сounteragent.Surname = TxtForSurname.Text;
                    сounteragent.MiddleName = TxtForMiddle.Text;
                    сounteragent.INN = TxtForInn.Text;
                    сounteragent.Adress = TxtForAdress.Text;
                    сounteragent.Phone = TxtForPhone.Text;
                    сounteragent.Role = role;
                    сounteragent.Removed = false;
                    db.Сounteragent.Add(сounteragent);
                    db.SaveChanges();
                    TableCounteragents.ItemsSource = db.Сounteragent.Where(p => p.Role == 1 && p.Removed == false).ToList();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Такой контерагент уже существует!");
                }
              
            }
            else
            {
                MessageBox.Show("Вы не заполнили поля!");
            }
          
        }
    }
}

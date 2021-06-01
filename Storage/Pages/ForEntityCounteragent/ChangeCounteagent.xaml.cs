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
    /// Логика взаимодействия для ChangeCounteagent.xaml
    /// </summary>
    public partial class ChangeCounteagent : Window
    {

        ModelStorage db = new ModelStorage();
        ListView TableCounteragents;
        int IdCounter, roleCounter;
        public ChangeCounteagent(int id, string  nameCoun, string Surname, string Middle, string Inn, string adress, string Phone, int Role, ListView Table)
        {
            InitializeComponent();
            TableCounteragents = Table;
            IdCounter = id;
            TxtForName.Text = nameCoun;
            TxtForSurname.Text = Surname;
            TxtForMiddle.Text = Middle;
            TxtForInn.Text = Inn;
            TxtForAdress.Text = adress;
            TxtForPhone.Text = Phone;
            roleCounter = Role;
        }

        private void BtnChangeCounteragent_Click(object sender, RoutedEventArgs e)
        {

            if (TxtForName.Text != String.Empty && TxtForSurname.Text != String.Empty && TxtForMiddle.Text != String.Empty && TxtForInn.Text != String.Empty && TxtForPhone.Text != String.Empty)
            {
                var checkCounteragent = db.Сounteragent.Where(p => p.INN == TxtForInn.Text).FirstOrDefault();
                if(checkCounteragent == null)
                {
                    var сounteragent = db.Сounteragent.Where(p => p.IdСounteragent == IdCounter).FirstOrDefault();
                    сounteragent.Name = TxtForName.Text;
                    сounteragent.Surname = TxtForSurname.Text;
                    сounteragent.MiddleName = TxtForMiddle.Text;
                    сounteragent.INN = TxtForInn.Text;
                    сounteragent.Adress = TxtForAdress.Text;
                    сounteragent.Phone = TxtForPhone.Text;
                    сounteragent.Role = roleCounter;
                    сounteragent.Removed = false;
                    db.SaveChanges();
                    TableCounteragents.ItemsSource = db.Сounteragent.Where(p => p.Role == roleCounter && p.Removed == false).ToList();
                    this.Close();
                }
                else MessageBox.Show("Такой контерагент уже существует!");

            }
            else
            {
                MessageBox.Show("Вы не заполнили поля!");
            }
        }
    }
}

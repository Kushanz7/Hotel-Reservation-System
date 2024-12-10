using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
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

namespace HotelReservationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Pass WHERE Username=@v1 AND Password=@v2", con);
            cmd.Parameters.AddWithValue("@v1", txtUsername.Text);
            cmd.Parameters.AddWithValue("@v2", txtPassword.Password);

            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                Dashboard dash = new Dashboard();
                dash.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect");
            }

            con.Close();


        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

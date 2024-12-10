using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Data;

namespace HotelReservationSystem
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RoomTypes roomTypes = new RoomTypes();
            roomTypes.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Rooms rooms = new Rooms();
            rooms.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Reservation reservation = new Reservation();
            reservation.Show();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin login = new AdminLogin();
            login.Show();
            this.Close();
        }

        

    }
}

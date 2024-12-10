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

namespace HotelReservationSystem
{
    /// <summary>
    /// Interaction logic for RoomTypes.xaml
    /// </summary>
    public partial class RoomTypes : Window
    {
        public RoomTypes()
        {
            InitializeComponent();
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            this.Close();
            dashboard.Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            
            Application.Current.Shutdown();
        }

    }
}

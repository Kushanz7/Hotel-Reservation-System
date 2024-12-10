using System;
using System.Collections.Generic;
using System.Data;
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

namespace HotelReservationSystem
{
    /// <summary>
    /// Interaction logic for Rooms.xaml
    /// </summary>
    public partial class Rooms : Window
    {
        public Rooms()
        {
            InitializeComponent();
            LoadDataGridView();
        }

        public void LoadDataGridView()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM rooms", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "rooms");

                dataGridRooms.ItemsSource = ds.Tables["rooms"].DefaultView;

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO rooms VALUES (@v1,@v2,@v3,@v4)", con);
                cmd.Parameters.AddWithValue("@v1", txtRoomID.Text);
                cmd.Parameters.AddWithValue("@v2", cmbRoomType.Text);
                cmd.Parameters.AddWithValue("@v3", cmbView.Text);
                cmd.Parameters.AddWithValue("@v4", cmbAvailability.Text);

                int reslut = cmd.ExecuteNonQuery();

                if (reslut == 1)
                {
                    LoadDataGridView();
                }
                else
                {
                    MessageBox.Show("Somthing went wrong!!");
                }

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE rooms SET roomtype = @v2, view = @v3, availability = @v4 WHERE roomid = @v1", con);
                cmd.Parameters.AddWithValue("@v1", txtRoomID.Text);
                cmd.Parameters.AddWithValue("@v2", cmbRoomType.Text);
                cmd.Parameters.AddWithValue("@v3", cmbView.Text);
                cmd.Parameters.AddWithValue("@v4", cmbAvailability.Text);

                int reslut = cmd.ExecuteNonQuery();

                if (reslut == 1)
                {
                    LoadDataGridView();
                }
                else
                {
                    MessageBox.Show("Somthing went wrong!!");
                }

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtRoomID.Clear();
            cmbAvailability.Text = "";
            cmbFilterRoomType.Text = "";
            cmbRoomType.Text = "";
            cmbView.Text = "";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Confirm Delete?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DeleteAction();
            }
            else
            {

            }
        }


        private void DeleteAction()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM rooms WHERE roomid = @v1", con);
                cmd.Parameters.AddWithValue("@v1", txtRoomID.Text);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    LoadDataGridView();
                }
                else
                {
                    MessageBox.Show("No record found with the provided Employee ID.");
                }

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void dataGridRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridRooms.SelectedItem != null)
            {
                DataRowView row = dataGridRooms.SelectedItem as DataRowView;
                if(row != null)
               { txtRoomID.Text = row[0].ToString();
                cmbRoomType.Text = row[1].ToString();
                cmbView.Text = row[2].ToString();
                    cmbAvailability.Text = row[3].ToString();
                }
            }
            else
            {

            }
        }

        private void cmbFilterRoomType_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void cmbFilterView_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM rooms WHERE roomtype = @filterroomtype", con);
                adapter.SelectCommand.Parameters.AddWithValue("@filterroomtype", cmbFilterRoomType.Text);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "rooms");

                dataGridRooms.ItemsSource = ds.Tables["rooms"].DefaultView;

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());

            }
        }
    }
}

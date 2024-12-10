using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using System.Data.SqlClient;
using System.Data;
using System.Windows.Markup;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace HotelReservationSystem
{
    /// <summary>
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {

        public Reservation()
        {
            InitializeComponent();
            LoadDataGridView();
            GetPricesPerNight();
            LoadAvailDataGrid();
            string lastCid = GetLastCid();
            lblpreviousCustID.Content = $"{lastCid}";
        }
        
        public (float singlePricePerNight, float kingPricePerNight, float suitePricePerNight) GetPricesPerNight()
        {
            float singlePricePerNight = 0;
            float kingPricePerNight = 0;
            float suitePricePerNight = 0;

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                con.Open();

                string query = "SELECT roomType, price FROM pricePnight";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string roomType = reader["roomType"].ToString();
                            float price = Convert.ToSingle(reader["price"]);

                            switch (roomType)
                            {
                                case "Single Room":
                                    singlePricePerNight = price;
                                    break;
                                case "King Room":
                                    kingPricePerNight = price;
                                    break;
                                case "Suite Room":
                                    suitePricePerNight = price;
                                    break;
                            }
                        }
                    }
                }
                con.Close();
            }

            return (singlePricePerNight, kingPricePerNight, suitePricePerNight);
        }

        public void LoadDataGridView()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM reservation", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "reservation");

                ReservationViewGrid.ItemsSource = ds.Tables["reservation"].DefaultView;

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        public void LoadAvailDataGrid()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT roomid,[view],availability FROM rooms", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "rooms");

                ResevAvailGridView.ItemsSource = ds.Tables["rooms"].DefaultView;

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        /*float singlepricepernight = 5000;
        float kingpricepernight = 10000;
        float suitepricepernight = 20000;*/
            //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Documents\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30";

            // Initialize the variables

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

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var prices = GetPricesPerNight();
                float singlepricepernight = prices.singlePricePerNight;
                float kingpricepernight = prices.kingPricePerNight;
                float suitepricepernight = prices.suitePricePerNight;

                string RoomType = cmbRoomType.Text;
                float NumberofNights = float.Parse(txtNumberOfNights.Text);
                //double NumberodGuests = Convert.ToDouble(txtNumofGuest.Text);
                string AddOns = cmbAddOns.Text;
                float Total = 0;
                string roomid = txtRoomID.Text;
                
                switch (AddOns)
                {
                    case "Pool Access":
                        singlepricepernight += 500;
                        kingpricepernight += 1000;
                        suitepricepernight += 1500;
                        break;
                    case "Gym and Spa Access":
                        singlepricepernight += 1000;
                        kingpricepernight += 2000;
                        suitepricepernight += 2500;
                        break;
                    case "Roof Top Bar Access":
                        singlepricepernight += 1500;
                        kingpricepernight += 2500;
                        suitepricepernight += 3000;
                        break;
                }

                switch (RoomType)
                {
                    case "Single Room":
                        Total = NumberofNights * singlepricepernight;
                        break;
                    case "King Room":
                        Total = NumberofNights * kingpricepernight;
                        break;
                    case "Suite Room":
                        Total = NumberofNights * suitepricepernight;
                        break;
                }

                txtFinalBill.Text = Total.ToString();
                int NumberOfNights = int.Parse(txtNumberOfNights.Text);
                DateTime checkin = DateTime.Now;
                DateTime checkout = checkin.AddDays(NumberOfNights);
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                
                    con.Open();

                
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM rooms WHERE roomid = @v8 AND availability = 'Available'", con);
                checkCmd.Parameters.AddWithValue("@v8", txtRoomID.Text);
                int count = (int)checkCmd.ExecuteScalar();
                bool capacityExceeded = false;
                if (count > 0)
                {
                    switch (cmbRoomType.Text)
                    {
                        case "Single Room":
                            if (int.Parse(txtNumberOfGuests.Text) > 2)
                            {
                                label10.Content = "Capacity Exceeded!!";
                                capacityExceeded = true;
                            }
                            break;
                        case "King Room":
                            if (int.Parse(txtNumberOfGuests.Text) > 4)
                            {
                                label10.Content = "Capacity Exceeded!!";
                                capacityExceeded = true;
                            }
                            break;
                        case "Suite Room":
                            if (int.Parse(txtNumberOfGuests.Text) > 6)
                            {
                                label10.Content = "Capacity Exceeded!!";
                                capacityExceeded = true;
                            }
                            break;
                    }
                    if (!capacityExceeded)
                    {

                        SqlCommand cmdemail = new SqlCommand("INSERT INTO email VALUES (@v1,@v2)",con);
                        cmdemail.Parameters.AddWithValue("@v1", txtCustomerID.Text);
                        cmdemail.Parameters.AddWithValue("@v2", txtCustomerEmail.Text);
                        cmdemail.ExecuteNonQuery();
                        SqlCommand cmd = new SqlCommand("INSERT INTO reservation VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8,@v9,@v10)", con);
                        cmd.Parameters.AddWithValue("@v1", txtCustomerID.Text);
                        cmd.Parameters.AddWithValue("@v2", txtCustomerName.Text);
                        cmd.Parameters.AddWithValue("@v3", int.Parse(txtNumberOfGuests.Text));
                        cmd.Parameters.AddWithValue("@v4", int.Parse(txtNumberOfNights.Text));
                        cmd.Parameters.AddWithValue("@v5", Convert.ToString(checkin));
                        cmd.Parameters.AddWithValue("@v6", Convert.ToString(checkout));
                        cmd.Parameters.AddWithValue("@v7", cmbRoomType.Text);
                        cmd.Parameters.AddWithValue("@v8", txtRoomID.Text);
                        cmd.Parameters.AddWithValue("@v9", cmbAddOns.Text);
                        cmd.Parameters.AddWithValue("@v10", float.Parse(txtFinalBill.Text));

                        int result = cmd.ExecuteNonQuery();
                        if (result == 1)
                        {
                            MessageBox.Show("Record added successfully!");
                            SqlCommand updateCmd = new SqlCommand("UPDATE rooms SET availability = 'Unavailable' WHERE roomid = @v8", con);
                            updateCmd.Parameters.AddWithValue("@v8", txtRoomID.Text);
                            updateCmd.ExecuteNonQuery();
                            SendConfirmationEmail(txtCustomerEmail.Text, txtCustomerName.Text, RoomType, NumberOfNights, Total, checkin, checkout);
                        }
                    }
                    
                }
                else
                {
                    label11.Content = "Check Room ID!!";
                    MessageBox.Show("The selected room is either unavailable or does not exist.");
                }

                con.Close();
                LoadDataGridView();
                LoadAvailDataGrid();

            }
            catch (Exception ee) {MessageBox.Show(ee.ToString());}
        }

        private void SendConfirmationEmail(string customerEmail, string customerName, string roomType, int numberOfNights, float totalAmount, DateTime checkInDate, DateTime checkOutDate)
        {
            string fromAddress = "youremail@gmail.com";
            string fromPassword = "youaccesspass";

            string subject = "Reservation Confirmation - Kamui Residence";
            string body = $@"
        <html>
        <body>
            <div style='font-family: Arial, sans-serif; color: #333;'>
                <h2>Reservation Confirmation</h2>
                <p>Dear {customerName},</p>
                <p>Your reservation has been successfully made. Here are the details of your reservation:</p>
                <table style='width: 100%; border-collapse: collapse;'>
                    <tr>
                        <td style='padding: 8px; border: 1px solid #ddd;'>Room Type:</td>
                        <td style='padding: 8px; border: 1px solid #ddd;'>{roomType}</td>
                    </tr>
                    <tr>
                        <td style='padding: 8px; border: 1px solid #ddd;'>Number of Nights:</td>
                        <td style='padding: 8px; border: 1px solid #ddd;'>{numberOfNights}</td>
                    </tr>
                    <tr>
                        <td style='padding: 8px; border: 1px solid #ddd;'>Check-In Date:</td>
                        <td style='padding: 8px; border: 1px solid #ddd;'>{checkInDate.ToShortDateString()}</td>
                    </tr>
                    <tr>
                        <td style='padding: 8px; border: 1px solid #ddd;'>Check-Out Date:</td>
                        <td style='padding: 8px; border: 1px solid #ddd;'>{checkOutDate.ToShortDateString()}</td>
                    </tr>
                    <tr>
                        <td style='padding: 8px; border: 1px solid #ddd;'>Total Amount:</td>
                        <td style='padding: 8px; border: 1px solid #ddd;'>${totalAmount}</td>
                    </tr>
                </table>
                <p>Thank you for choosing Kamui Residence!</p>
                <p>Best regards,<br />Kamui Residence</p>
            </div>
        </body>
        </html>";

            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress, fromPassword)
                };

                var message = new MailMessage(fromAddress, customerEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Email sending failed: " + ex.Message);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM reservation WHERE customerid = @v1", con);
                
                cmd.Parameters.AddWithValue("@v1", txtCustomerID.Text);
               
                SqlDataReader rdr = cmd.ExecuteReader();
                
                if (rdr.Read())
                {
                    txtCustomerName.Text = rdr[1].ToString();
                    txtNumberOfGuests.Text = rdr[2].ToString();
                    txtNumberOfNights.Text = rdr[3].ToString();
                    cmbRoomType.Text = rdr[6].ToString();
                    txtRoomID.Text = rdr[7].ToString();
                    cmbAddOns.Text = rdr[8].ToString();
                    txtFinalBill.Text = rdr[9].ToString();
                }
                else
                {
                    MessageBox.Show("No record found with the provided Customer ID.");
                }
                con.Close();

                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                conn.Open();
                SqlCommand cmdd = new SqlCommand("SELECT * FROM email WHERE cid = @v1", conn);
                cmdd.Parameters.AddWithValue("@v1", txtCustomerID.Text);
                SqlDataReader rdrr = cmdd.ExecuteReader();
                if (rdrr.Read())
                {
                    txtCustomerEmail.Text = rdrr[1].ToString();
                }
                else
                {
                    MessageBox.Show("No record found with the provided Customer ID.");
                }
                conn.Close();
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

                SqlCommand cmd = new SqlCommand("UPDATE reservation SET  customerid=@v1,customername=@v2,numofguests=@v3,numofnights@v4,roomtype=@v7,roomid=@v8,addon=@v9,finalbill=@v10)", con);
                cmd.Parameters.AddWithValue("@v1", txtCustomerID.Text);
                cmd.Parameters.AddWithValue("@v2", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@v3", int.Parse(txtNumberOfGuests.Text));
                cmd.Parameters.AddWithValue("@v4", int.Parse(txtNumberOfNights.Text));
                cmd.Parameters.AddWithValue("@v7", cmbRoomType.Text);
                cmd.Parameters.AddWithValue("@v8", txtRoomID.Text);
                cmd.Parameters.AddWithValue("@v9", cmbAddOns.Text);
                cmd.Parameters.AddWithValue("@v10", float.Parse(txtFinalBill.Text));

                int reslut = cmd.ExecuteNonQuery();

                if (reslut == 1)
                {
                    LoadDataGridView();
                    LoadAvailDataGrid();
                }
                else
                {
                    MessageBox.Show("Somthing went wrong!!");
                }

                con.Close();
            }
            catch(Exception ee) { MessageBox.Show(ee.ToString()); }
            


        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtCustomerID.Clear();
            txtCustomerName.Clear();
            txtFinalBill.Clear();
            txtNumberOfGuests.Clear();
            txtNumberOfNights.Clear();
            txtRoomID.Clear();
            txtCustomerEmail.Clear();
            cmbAddOns.Text = "";
            cmbRoomType.Text = "";
            label10.Content = "";
            label11.Content = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM reservation WHERE roomtype = @filterroomtype", con);
                adapter.SelectCommand.Parameters.AddWithValue("@filterroomtype", cmbroomtype.Text);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "reservation");

                ReservationViewGrid.ItemsSource = ds.Tables["reservation"].DefaultView;

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());

            }
        }

        private void ReservationViewGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReservationViewGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)ReservationViewGrid.SelectedItem;
                txtcustid.Text = row[0].ToString();
            }
            else
            {

            }
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
                SqlCommand getRoomCmd = new SqlCommand("SELECT roomid FROM reservation WHERE customerid = @v1", con);
                getRoomCmd.Parameters.AddWithValue("@v1", txtcustid.Text);
                string roomId = getRoomCmd.ExecuteScalar()?.ToString();
                SqlCommand cmd = new SqlCommand("DELETE FROM reservation WHERE customerid = @v1", con);
                cmd.Parameters.AddWithValue("@v1", txtcustid.Text);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    SqlCommand updateCmd = new SqlCommand("UPDATE rooms SET availability = 'Available' WHERE roomid = @roomId", con);
                    updateCmd.Parameters.AddWithValue("@roomId", roomId);
                    updateCmd.ExecuteNonQuery();

                    LoadDataGridView();
                    LoadAvailDataGrid();
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
        private string GetLastCid()
        {
            string lastCid = string.Empty; string query = "SELECT TOP 1 cid FROM [dbo].[email] ORDER BY cid DESC"; 
            try 
            { using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30")) 
                { con.Open(); 
                    using (SqlCommand cmd = new SqlCommand(query, con)) 
                    { 
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read()) 
                        { 
                            lastCid = reader["cid"].ToString(); 
                        } 
                    } 
                } 
            } catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
            return lastCid;
        }
    }
}

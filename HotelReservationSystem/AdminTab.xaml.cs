using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using PdfSharp.Drawing;

namespace HotelReservationSystem
{
    /// <summary>
    /// Interaction logic for AdminTab.xaml
    /// </summary>
    public partial class AdminTab : Window
    {
        public AdminTab()
        {
            InitializeComponent();
            LoadAdminLog();
            LoadFrontLog();
            LoadPrice();
            LoadEmail();
            LoadEvent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void LoadPrice()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM pricePnight", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "pricePnight");

                RoomPriceDataGridView.ItemsSource = ds.Tables["pricePnight"].DefaultView;

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        public void LoadFrontLog()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Pass", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Pass");

                FrontLogDataGridView.ItemsSource = ds.Tables["Pass"].DefaultView;

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        public void LoadAdminLog()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM AdminLog", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "AdminLog");

                AdminLogDataGridView.ItemsSource = ds.Tables["AdminLog"].DefaultView;

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

                SqlCommand cmd = new SqlCommand("UPDATE pricePnight SET price = @v2 WHERE roomType = @v1", con);
                cmd.Parameters.AddWithValue("@v1", lblRoomType.Content);
                cmd.Parameters.AddWithValue("@v2",float.Parse(txtPrice.Text));

                int reslut = cmd.ExecuteNonQuery();

                if (reslut == 1)
                {
                    LoadPrice();
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

        private void RoomPriceDataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomPriceDataGridView.SelectedItem != null)
            {
                DataRowView row = RoomPriceDataGridView.SelectedItem as DataRowView;
                if(row != null)
                {lblRoomType.Content = row[0].ToString();
                    txtPrice.Text = row[1].ToString();
                }
            }
            else
            {

            }
        }

        private void FrontLogDataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FrontLogDataGridView.SelectedItem != null)
            {
                DataRowView row = FrontLogDataGridView.SelectedItem as DataRowView; 
                if (row != null) 
                { 
                    txtUsername.Text = row[0].ToString();
                    txtPassword.Text = row[1].ToString(); 
                }
            }
        }

        private void AdminLogDataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AdminLogDataGridView.SelectedItem != null)
            {
                DataRowView row = AdminLogDataGridView.SelectedItem as DataRowView;
                if(row != null)
                {
                    txtAdminUser.Text = row[0].ToString();
                    txtAdminPass.Text = row[1].ToString();
                }
                
            }
            else
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Pass VALUES (@v1,@v2)", con);
                cmd.Parameters.AddWithValue("@v1", txtUsername.Text);
                cmd.Parameters.AddWithValue("@v2", txtPassword.Text);

                int reslut = cmd.ExecuteNonQuery();

                if (reslut == 1)
                {
                    LoadFrontLog();
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

        private void DeleteActionFront()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Pass WHERE Username = @v1", con);
                cmd.Parameters.AddWithValue("@v1", txtUsername.Text);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    LoadFrontLog();
                }
                else
                {
                    MessageBox.Show("No record found with the provided Username ID.");
                }

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void DeleteActionAdmin()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM AdminLog WHERE AdminUser = @v1", con);
                cmd.Parameters.AddWithValue("@v1", txtAdminUser.Text);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    LoadAdminLog();
                }
                else
                {
                    MessageBox.Show("No record found with the provided Admin Username ID.");
                }

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Confirm Delete?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DeleteActionFront();
            }
            else
            {

            }
        }

        private void btnAdminAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO AdminLog VALUES (@v1,@v2)", con);
                cmd.Parameters.AddWithValue("@v1", txtAdminUser.Text);
                cmd.Parameters.AddWithValue("@v2", txtAdminPass.Text);

                int reslut = cmd.ExecuteNonQuery();

                if (reslut == 1)
                {
                    LoadAdminLog();
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

        private void btnAdminDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Confirm Delete?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DeleteActionAdmin();
            }
            else
            {

            }
        }

        public void LoadEmail()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM email", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "email");

                EmailGridView.ItemsSource = ds.Tables["email"].DefaultView;

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        public void LoadEvent()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM events", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "events");

                EventGridView.ItemsSource = ds.Tables["events"].DefaultView;

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void btnEAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO events VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7)", con);
                cmd.Parameters.AddWithValue("@v1", txtEventID.Text);
                cmd.Parameters.AddWithValue("@v2", txtEventTitle.Text);
                cmd.Parameters.AddWithValue("@v3",txtEventDesc.Text);
                cmd.Parameters.AddWithValue("@v4", txthlight1.Text);
                cmd.Parameters.AddWithValue("@v5",txthlight2.Text);
                cmd.Parameters.AddWithValue("@v6",txtdate.Text);
                cmd.Parameters.AddWithValue("@v7",txtTime.Text);

                int reslut = cmd.ExecuteNonQuery();

                if (reslut == 1)
                {
                    LoadEvent();
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

        private void btnEUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE events SET eventtitle = @v2,eventdesc = @v3,eventhone=@v4,eventhtwo=@v5,eventdate=@v6,eventtime=@v7 WHERE eventid = @v1", con);
                cmd.Parameters.AddWithValue("@v1", txtEventID.Text);
                cmd.Parameters.AddWithValue("@v2", txtEventTitle.Text);
                cmd.Parameters.AddWithValue("@v3", txtEventDesc.Text);
                cmd.Parameters.AddWithValue("@v4", txthlight1.Text);
                cmd.Parameters.AddWithValue("@v5", txthlight2.Text);
                cmd.Parameters.AddWithValue("@v6", txtdate.Text);
                cmd.Parameters.AddWithValue("@v7", txtTime.Text);

                int reslut = cmd.ExecuteNonQuery();

                if (reslut == 1)
                {
                    LoadEvent();
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

        private void DeleteActionEvent()
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM events WHERE eventid = @v1", con);
                cmd.Parameters.AddWithValue("@v1", txtEventID.Text);

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    LoadAdminLog();
                }
                else
                {
                    MessageBox.Show("No record found with the provided Event ID.");
                }

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        private void btnEDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Confirm Delete?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DeleteActionEvent();
            }
            else
            {

            }
        }

        private void EventGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventGridView.SelectedItem != null)
            {
                DataRowView row = EventGridView.SelectedItem as DataRowView;
                if(row != null)
                {txtEventID.Text = row[0].ToString();
                txtEventTitle.Text = row[1].ToString();
                txtEventDesc.Text = row[2].ToString();
                txthlight1.Text = row[3].ToString();
                txthlight2.Text = row[4].ToString();
                txtdate.Text = row[5].ToString();
                    txtTime.Text = row[6].ToString();
                }
            }
            else
            {

            }
        }

        private void btnEUSend_Click(object sender, RoutedEventArgs e)
        {
            string eventTitle = "";
            string eventDescription = "";
            string eventHOne = "";
            string eventHTwo = "";
            string date = "";
            string time = "";

            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT eventtitle,eventdesc,eventhone,eventhtwo,eventdate,eventtime FROM events WHERE eventid = @EventID", con);
            cmd.Parameters.AddWithValue("@EventID",txtEventID.Text);
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                eventTitle = reader["eventtitle"].ToString();
                eventDescription = reader["eventdesc"].ToString();
                eventHOne = reader["eventhone"].ToString();
                eventHTwo = reader["eventhtwo"].ToString();
                date = reader["eventdate"].ToString();
                time = reader["eventtime"].ToString();
            }
            reader.Close();

            List<string> emailList = new List<string>();
            cmd = new SqlCommand("SELECT email FROM email",con);
            reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                emailList.Add(reader["email"].ToString());
            }

            con.Close();
            foreach(string email in emailList)
            {
                SendEmail(email,eventTitle,eventDescription,eventHOne,eventHTwo,date,time);
            }
        }

        public void SendEmail(string toEmail,string subject,string eventdesc,string h1,string h2,string Date,string Time)
        {
            string fromAddress = "kushanrathnayake5@gmail.com";
            string fromPassword = "ytftnxlwsdljhiqx";

            
            string body = $@"
        <html>
        <body>
            <div style='font-family: Arial, sans-serif; color: #333;'>
                <h2>{subject}</h2>
                <p>{eventdesc}</p>
                <h3>Event Highlights!</h3>
                <ul>
                    <li>{h1}</li>
                    <li>{h2}</li>
                </ul>
                    <p><strong>Date: </strong>{Date}</p>
                    <p><strong>Time: </strong>{Time}</p>
                    <p><strong>Location: </strong>Kamui Residence</p>
                    <p>Bring your family and friends for a day of unforgettable fun!</p>
                <p>Thank you for choosing Kamui Residence!</p>
                <p>Best regards,<br />Kamui Hotel</p>
                <p>Contact for more information: Kushan - 0759811633</p>
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

                var message = new MailMessage(fromAddress, toEmail)
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

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtEventDesc.Clear();
            txtEventID.Clear();
            txtEventTitle.Clear();
            txthlight1.Clear();
            txthlight2.Clear();
            txtdate.Clear();
            txtTime.Clear();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DELL\\Desktop\\NIBM\\GUI\\CW\\CW\\HotelReservationSystem\\HotelReservationSystem\\DB\\HotelDB.mdf;Integrated Security=True;Connect Timeout=30";
            decimal totalIncome = 0;
            int singleRoomTotal = 0;
            int suiteRoomTotal = 0;
            int kingRoomTotal = 0;
            int singleRoomsBooked = 0;
            int suiteRoomsBooked = 0;
            int kingRoomsBooked = 0;
            DataTable bookedRoomsTable = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string totalRoomsQuery = "SELECT roomtype, COUNT(*) as count FROM rooms GROUP BY roomtype";
                    SqlCommand totalRoomsCmd = new SqlCommand(totalRoomsQuery, con);
                    SqlDataReader reader = totalRoomsCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string roomType = reader["roomtype"].ToString();
                        int count = Convert.ToInt32(reader["count"]);
                        switch (roomType)
                        {
                            case "Single Room":
                                singleRoomTotal = count;
                                break;
                            case "Suite Room":
                                suiteRoomTotal = count;
                                break;
                            case "King Room":
                                kingRoomTotal = count;
                                break;
                        }
                    }
                    reader.Close();
                    string bookedRoomsQuery = "SELECT roomid, customerid, checkin, checkout, roomtype,finalbill FROM reservation";
                    SqlDataAdapter adapter = new SqlDataAdapter(bookedRoomsQuery, con);
                    adapter.Fill(bookedRoomsTable);

                    string totalIncomeQuery = "SELECT SUM(finalbill) FROM reservation";
                    SqlCommand incomeCmd = new SqlCommand(totalIncomeQuery, con);
                    object result = incomeCmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null) { totalIncome = Convert.ToDecimal(result); }
                    foreach (DataRow row in bookedRoomsTable.Rows)
                    {
                        string roomType = row["roomtype"].ToString();
                        switch (roomType)
                        {
                            case "Single Room":
                                singleRoomsBooked++;
                                break;
                            case "Suite Room":
                                suiteRoomsBooked++;
                                break;
                            case "King Room":
                                kingRoomsBooked++;
                                break;
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message);
                return;
            }
            int singleRoomsRemaining = singleRoomTotal - singleRoomsBooked; int suiteRoomsRemaining = suiteRoomTotal - suiteRoomsBooked; int kingRoomsRemaining = kingRoomTotal - kingRoomsBooked;


            string contact = "Contact - 0754567003";
            string email = "Email - residencekamui@gmail.com";


            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {

                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                pdf.Info.Title = "Hotel Reservation Report";


                PdfSharp.Pdf.PdfPage page = pdf.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);


                XFont titleFont = new XFont("Arial", 30);
                XFont sectionFont = new XFont("Arial", 12);
                XFont normalFont = new XFont("Arial", 12);
                XFont footerFont = new XFont("Arial", 10);

                gfx.DrawString("Booked Rooms Report", titleFont, XBrushes.Black, new XPoint(200, 30));


                gfx.DrawLine(XPens.Black, 20, 50, page.Width - 20, 50);


                gfx.DrawString("Date: " + DateTime.Now, normalFont, XBrushes.Black, new XPoint(20, 70));
                gfx.DrawString("Contact: " + contact, normalFont, XBrushes.Black, new XPoint(20, 90));
                gfx.DrawString("Email: " + email, normalFont, XBrushes.Black, new XPoint(20, 110));

                double yPosition = 140;

                gfx.DrawString("Booked Rooms Details", sectionFont, XBrushes.Red, new XPoint(40, yPosition));
                yPosition += 20;

                gfx.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);
                yPosition += 16;


                foreach (DataRow row in bookedRoomsTable.Rows)
                {
                    if (yPosition > page.Height - 50)
                    {
                        page = pdf.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        yPosition = 40;
                    }

                    gfx.DrawString("Room Number: " + row["roomid"], normalFont, XBrushes.Black, new XPoint(20, yPosition));
                    gfx.DrawString("Guest Name: " + row["customerid"], normalFont, XBrushes.Black, new XPoint(page.Width / 2, yPosition));
                    yPosition += 20;
                    gfx.DrawString("Check-In Date: " + row["checkin"], normalFont, XBrushes.Black, new XPoint(20, yPosition));
                    gfx.DrawString("Check-Out Date: " + row["checkout"], normalFont, XBrushes.Black, new XPoint(page.Width / 2, yPosition));
                    yPosition += 20;
                    gfx.DrawString("Amount: Rs." + row["finalbill"], normalFont, XBrushes.Black, new XPoint(20, yPosition));
                    yPosition += 30;

                    gfx.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);
                    yPosition += 16;
                }
                if (yPosition > page.Height - 50)
                {
                    page = pdf.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPosition = 40;
                }

                gfx.DrawString("Summary", sectionFont, XBrushes.Black, new XPoint(40, yPosition));
                yPosition += 20;
                gfx.DrawString("Total Income: $" + totalIncome, normalFont, XBrushes.Black, new XPoint(20, yPosition));
                yPosition += 20;
                gfx.DrawString("Single Rooms Remaining: " + singleRoomsRemaining, normalFont, XBrushes.Black, new XPoint(20, yPosition)); yPosition += 20; gfx.DrawString("Suite Rooms Remaining: " + suiteRoomsRemaining, normalFont, XBrushes.Black, new XPoint(20, yPosition)); yPosition += 20; gfx.DrawString("King Rooms Remaining: " + kingRoomsRemaining, normalFont, XBrushes.Black, new XPoint(20, yPosition)); yPosition += 30;

                gfx.DrawString("KAMUI Residence", footerFont, XBrushes.Black, new XPoint(20, yPosition));
                yPosition += 20;

                gfx.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);

                pdf.Save(saveFileDialog.FileName);

                MessageBox.Show("Report has been saved as a PDF.");
            }


        }
    }
}

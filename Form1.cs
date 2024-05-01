using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRUD_App
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        private string connectionString = "Server=localhost;Database=product;Uid=root;Pwd=;";

        public Form1()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            connection = new MySqlConnection(connectionString);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = textBoxUserName.Text;
            string password = textBoxPass.Text;

            try
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM users WHERE user_name = @username AND user_pass = @password";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    DashboardForm dashboardForm = new DashboardForm(username); 
                    dashboardForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            signupForm signupForm = new signupForm();
            signupForm.Show();
            this.Hide();
        }
    }
}

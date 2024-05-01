using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRUD_App
{
    public partial class signupForm : Form
    {
        private MySqlConnection connection;
        private string connectionString = "Server=localhost;Database=product;Uid=root;Pwd=;"; 

        public signupForm()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            connection = new MySqlConnection(connectionString);
        }

        private void SignupButton_Click(object sender, EventArgs e)
        {
            
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = textBoxUserName.Text;
            string password = textBoxPass.Text;
            string email = textBoxEmail.Text;

            try
            {
                connection.Open();

                string query = "INSERT INTO users (user_name, user_pass, user_email) VALUES (@username, @password, @email)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Signup successful!");
                }
                else
                {
                    MessageBox.Show("Signup failed!");
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

        private void label5_Click(object sender, EventArgs e)
        {
            Form1 signinForm = new Form1();
            signinForm.Show();
            this.Hide();
        }
    }
}

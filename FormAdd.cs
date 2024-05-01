using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace CRUD_App
{
    public partial class FormAdd : Form
    {
        private MySqlConnection connection;
        private string connectionString = "Server=localhost;Database=product;Uid=root;Pwd=;";
        private string username;

        public FormAdd(string username)
        {
            this.username = username;
            InitializeComponent();
            connection = new MySqlConnection(connectionString);
            labelUserName.Text = username;
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            string productName = textBoxProductName.Text;
            int productPrice = Convert.ToInt32(textBoxPrice.Text);
            int productQuantity = Convert.ToInt32(textBoxQuantity.Text);

            string insertQuery = "INSERT INTO products (product_name, product_price, instock) VALUES (@productName, @productPrice, @productQuantity)";
            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@productName", productName);
                insertCommand.Parameters.AddWithValue("@productPrice", productPrice);
                insertCommand.Parameters.AddWithValue("@productQuantity", productQuantity);

                try
                {
                    connection.Open();
                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Product added successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding product: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm(username);
            dashboardForm.Show();
            this.Hide();
        }

        private void labelUserName_Click(object sender, EventArgs e)
        {
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            connection.Dispose();
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRUD_App
{
    public partial class DashboardForm : Form
    {
        private MySqlConnection connection;
        private string connectionString = "Server=localhost;Database=product;Uid=root;Pwd=;";
        public string username;

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        public DashboardForm(string username)
        {
            InitializeComponent();
            this.username = username;

            InitializeDatabaseConnection();
            labelUserName.Text = username;
            LoadProducts();

            this.FormClosing += DashboardForm_FormClosing;
        }

        private void InitializeDatabaseConnection()
        {
            connection = new MySqlConnection(connectionString);
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadProducts()
        {
            string query = "SELECT * FROM products";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
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
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
           FormAdd dashboardForm = new FormAdd(username);
            dashboardForm.Show();
            this.Hide();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                DataGridViewCell selectedCell = dataGridView1.SelectedCells[0];

                if (selectedCell.ReadOnly)
                {
                    MessageBox.Show("Selected cell is not editable.");
                    return;
                }

                int rowIndex = selectedCell.RowIndex;
                int columnIndex = selectedCell.ColumnIndex;
                object newValue = selectedCell.Value;

                int selectedRowID = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["product_id"].Value);

                string columnName = dataGridView1.Columns[columnIndex].Name;

                string updateQuery = $"UPDATE products SET {columnName} = @newValue WHERE product_id = @product_id";
                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@newValue", newValue);
                    updateCommand.Parameters.AddWithValue("@product_id", selectedRowID);

                    try
                    {
                        connection.Open();
                        updateCommand.ExecuteNonQuery();
                        MessageBox.Show("Cell updated successfully!");
                        LoadProducts(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating cell: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a cell to update.");
            }
        }



        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["product_id"].Value);

                string deleteQuery = "DELETE FROM products WHERE product_id = @product_id";
                using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@product_id", selectedRowID);

                    try
                    {
                        connection.Open();
                        deleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Row deleted successfully!");
                        LoadProducts(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting row: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void labelUserName_Click(object sender, EventArgs e)
        {
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            LoadProducts();

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonAll_Click_1(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm(username);
            dashboardForm.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

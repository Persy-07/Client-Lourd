using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace GestionDeEmployer
{
    public partial class Salaire : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=employeDB;Integrated Security=True;");



        public Salaire()
        {
            InitializeComponent();

            displayEmployees();
            disableFields();

        }

        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }

            displayEmployees();
            disableFields();
        }

        public void disableFields()
        {
            SalaireMatricule_ID.Enabled = false;
            SalaireNoms.Enabled = false;
            Salaire_Poste.Enabled = false;
        }

        public void displayEmployees()
        {
            SalaireData ed = new SalaireData();
            List<SalaireData> listData = ed.salaryEmployeeListData();

            dataGridView1.DataSource = listData;
        }



        private void salary_updateBtn_Click(object sender, EventArgs e)
        {
            if(SalaireMatricule_ID.Text == ""
                || SalaireNoms.Text == ""
                || Salaire_Poste.Text == ""
                || Salaire_Salaire.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs vides", "Erreur Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Etes-vous sûr de vouloir METTRE À JOUR Employe ID: "
                    + SalaireMatricule_ID.Text.Trim() + "?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(check == DialogResult.Yes)
                {
                    if(connect.State == ConnectionState.Closed)
                    {
                        try
                        {
                            connect.Open();
                            DateTime today = DateTime.Today;

                            string updateData = "UPDATE employes SET salaire = @salaire" +
                                ", date_mise_a_jour = @date_mise_a_jour WHERE id_employe  = @id_employe";

                            using(SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@salaire", Salaire_Salaire.Text.Trim());
                                cmd.Parameters.AddWithValue("@date_mise_a_jour", today);
                                cmd.Parameters.AddWithValue("@id_employe", SalaireMatricule_ID.Text.Trim());

                                cmd.ExecuteNonQuery();

                                displayEmployees();

                                MessageBox.Show("Mis à jour avec succés!"
                                    , "Information Message", MessageBoxButtons.OK
                                    , MessageBoxIcon.Information);

                                clearFields();

                            }
                        }catch(Exception ex)
                        {
                            MessageBox.Show("Erreur: " + ex, "Erreur Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Annulé", "Information Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void clearFields()
        {
            SalaireMatricule_ID.Text = "";
            SalaireNoms.Text = "";
            Salaire_Poste.Text = "";
            Salaire_Salaire.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                SalaireMatricule_ID.Text = row.Cells[0].Value.ToString();
                SalaireNoms.Text = row.Cells[1].Value.ToString();
                Salaire_Poste.Text = row.Cells[4].Value.ToString();
                Salaire_Salaire.Text = row.Cells[5].Value.ToString();
            }
        }

        private void salary_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void searchButton_Click_1(object sender, EventArgs e)
        {

        }
    }
}

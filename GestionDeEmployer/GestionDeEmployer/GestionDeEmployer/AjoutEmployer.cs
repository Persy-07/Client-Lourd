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
using System.IO;

namespace GestionDeEmployer
{
    public partial class AjoutEmployer : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=employeDB;Integrated Security=True;");


        public AjoutEmployer()
        {
            InitializeComponent();

            // TO DISPLAY THE DATA FROM DATABASE TO YOUR DATA GRID VIEW
            displayEmployeeData();

         
        }

        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }
            displayEmployeeData();
        }

        public void displayEmployeeData()
        {
            EmployerData ed = new EmployerData();
            List<EmployerData> listData = ed.employeeListData();

            dataGridView1.DataSource = listData;



        }

        private void addEmployee_addBtn_Click(object sender, EventArgs e)
        {
            if(AjoutMatricule_ID.Text == ""
                || AjoutNoms_Employe.Text == ""
                || AjoutGenre_Employe.Text == ""
                || AjoutNumero_Employe.Text == ""
                || AjoutPoste_Employe.Text == ""
                || AjoutStatut_Employe.Text == ""
                || AjoutPhoto_Employe.Image == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs vides"
                    , "Erreur Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        string checkEmID = "SELECT COUNT(*) FROM  employes WHERE id_employe = @emID AND date_suppression IS NULL";

                        using(SqlCommand checkEm = new SqlCommand(checkEmID, connect))
                        {
                            checkEm.Parameters.AddWithValue("@emID", AjoutMatricule_ID.Text.Trim());
                            int count = (int)checkEm.ExecuteScalar();

                            if(count >= 1)
                            {
                                MessageBox.Show(AjoutMatricule_ID.Text.Trim() + " est déjà pris"
                                    , "Erreur Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DateTime today = DateTime.Today;
                                string insertData = "INSERT INTO employes " +
                                    "(id_employe, nom_complet, genre, numero_contact" +
                                    ", poste, image, salaire, date_insertion, statut) " +
                                    "VALUES(@id_employe, @nom_complet, @genre, @numero_contact" +
                                    ", @poste, @image, @salaire, @date_insertion, @statut)";

                                string path = Path.Combine(@"C:\Users\Perside Menayame\source\repos\GestionDeEmployer\GestionDeEmployer\GestionDeEmployer\Directory\"
                                    + AjoutMatricule_ID.Text.Trim() + ".jpg");

                                string directoryPath = Path.GetDirectoryName(path);

                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                File.Copy(AjoutPhoto_Employe.ImageLocation, path, true);

                                using(SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@id_employe", AjoutMatricule_ID.Text.Trim());
                                    cmd.Parameters.AddWithValue("@nom_complet", AjoutNoms_Employe.Text.Trim());
                                    cmd.Parameters.AddWithValue("@genre", AjoutGenre_Employe.Text.Trim());
                                    cmd.Parameters.AddWithValue("@numero_contact", AjoutNumero_Employe.Text.Trim());
                                    cmd.Parameters.AddWithValue("@poste", AjoutPoste_Employe.Text.Trim());
                                    cmd.Parameters.AddWithValue("@image", path);
                                    cmd.Parameters.AddWithValue("@salaire", 0);
                                    cmd.Parameters.AddWithValue("@date_insertion", today);
                                    cmd.Parameters.AddWithValue("@statut", AjoutStatut_Employe.Text.Trim());

                                    cmd.ExecuteNonQuery();

                                    displayEmployeeData();

                                    MessageBox.Show("Ajouté avec succès !"
                                        , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    clearFields();
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Erreur: " + ex
                    , "Erreur Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void addEmployee_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                string imagePath = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    AjoutPhoto_Employe.ImageLocation = imagePath;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erreur: " + ex, "Erreur Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                AjoutMatricule_ID.Text = row.Cells[0].Value.ToString();
                AjoutNoms_Employe.Text = row.Cells[1].Value.ToString();
                AjoutGenre_Employe.Text = row.Cells[2].Value.ToString();
                AjoutNumero_Employe.Text = row.Cells[3].Value.ToString();
                AjoutPoste_Employe.Text = row.Cells[4].Value.ToString();

                string imagePath = row.Cells[5].Value.ToString();

                if(imagePath != null)
                {
                    AjoutPhoto_Employe.Image = Image.FromFile(imagePath);
                }
                else
                {
                    AjoutPhoto_Employe.Image = null;
                }

                AjoutStatut_Employe.Text = row.Cells[7].Value.ToString();
            }
        }

        public void clearFields()
        {
            AjoutMatricule_ID.Text = "";
            AjoutNoms_Employe.Text = "";
            AjoutGenre_Employe.SelectedIndex = -1;
            AjoutNumero_Employe.Text = "";
            AjoutPoste_Employe.SelectedIndex = -1;
            AjoutStatut_Employe.SelectedIndex = -1;
            AjoutPhoto_Employe.Image = null;
        }


        //Enleve fonctionnalité bouton pour l'Etat de besoin en Ajouter ici 

        private void addEmployee_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void addEmployee_deleteBtn_Click(object sender, EventArgs e)
        {
            if (AjoutMatricule_ID.Text == ""
                || AjoutNoms_Employe.Text == ""
                || AjoutGenre_Employe.Text == ""
                || AjoutNumero_Employe.Text == ""
                || AjoutPoste_Employe.Text == ""
                || AjoutStatut_Employe.Text == ""
                || AjoutPhoto_Employe.Image == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs vides"
                    , "Erreur Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Etes-vous sûr que vous voulez supprimer " +
                    "Employe ID: " + AjoutMatricule_ID.Text.Trim() + "?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (check == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();
                        DateTime today = DateTime.Today;

                        string updateData = "UPDATE employes SET date_suppression = @date_suppression " +
                            "WHERE id_employe = @id_employe";

                        using (SqlCommand cmd = new SqlCommand(updateData, connect))
                        {
                            cmd.Parameters.AddWithValue("@date_suppression", today);
                            cmd.Parameters.AddWithValue("@id_employe", AjoutMatricule_ID.Text.Trim());

                            cmd.ExecuteNonQuery();

                            displayEmployeeData();

                            MessageBox.Show("Mise à jour réussie !"
                                , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            clearFields();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur: " + ex
                        , "Erreur Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Annulé."
                        , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }
    }
}

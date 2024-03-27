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
    public partial class RegisterForm : Form
    {
        SqlConnection connect 
            = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=employeDB;Integrated Security=True;");
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signup_loginBtn_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void signup_btn_Click(object sender, EventArgs e)
        {
            if(Inscrit_Noms.Text == ""
                || Inscrit_Mdp.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs vides"
                    , "Erreur Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();
                        // POUR VÉRIFIER SI L'UTILISATEUR EXISTE DÉJÀ
                        string selectUsername = "SELECT COUNT(id) FROM utilisateurs WHERE nom_utilisateur = @nom_utilisateur";

                        using(SqlCommand checkUser = new SqlCommand(selectUsername, connect))
                        {
                            checkUser.Parameters.AddWithValue("@nom_utilisateur", Inscrit_Noms.Text.Trim());
                            int count = (int)checkUser.ExecuteScalar();

                            if(count >= 1)
                            {
                                MessageBox.Show(Inscrit_Noms.Text.Trim() + " est déjà pris"
                                    , "Erreur Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DateTime today = DateTime.Today;

                                string insertData = "INSERT INTO utilisateurs " +
                                    "(nom_utilisateur, mot_de_passe, date_enregistrement) " +
                                    "VALUES(@nom_utilisateur, @mot_de_passe, @date_enregistrement)";

                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@nom_utilisateur", Inscrit_Noms.Text.Trim());
                                    cmd.Parameters.AddWithValue("@mot_de_passe", Inscrit_Mdp.Text.Trim());
                                    cmd.Parameters.AddWithValue("@date_enregistrement", today);

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Enregistré avec succès!"
                                        , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Form1 loginForm = new Form1();
                                    loginForm.Show();
                                    this.Hide();
                                }
                            }
                        }

                        

                    }catch(Exception ex)
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

        private void signup_showPass_CheckedChanged(object sender, EventArgs e)
        {
            Inscrit_Mdp.PasswordChar = signup_showPass.Checked ? '\0' : '*';
        }
    }
}

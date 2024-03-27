using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GestionDeEmployer
{
    class EmployerData
    {


        public string MatriculeID { set; get; } // 1
        public string Nom { set; get; } // 2
        public string Genre { set; get; } // 3
        public string Contact { set; get; } // 4
        public string Position { set; get; } // 5
        public string Image { set; get; } // 6
        public int Salaire { set; get; } // 7
        public string Statut { set; get; } // 8


        SqlConnection connect = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=employeDB;Integrated Security=True;");
    

        public List<EmployerData> employeeListData()
        {
            List<EmployerData> listdata = new List<EmployerData>();

            if(connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM employes WHERE date_suppression IS NULL";

                    using(SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            EmployerData ed = new EmployerData();
                            // ed.ID = (int)reader["id"]; // Cette ligne doit être enlevée si la colonne `id` n'existe pas.
                            ed.MatriculeID = reader["id_employe"].ToString();
                            ed.Nom = reader["nom_complet"].ToString();
                            ed.Genre = reader["genre"].ToString();
                            ed.Contact = reader["numero_contact"].ToString();
                            ed.Position = reader["poste"].ToString();
                            ed.Image = reader["image"].ToString(); // Assure-toi que ceci est correct pour le chemin de l'image.
                            ed.Salaire = reader["salaire"] != DBNull.Value ? Convert.ToInt32(reader["salaire"]) : 0;
                            ed.Statut = reader["statut"].ToString();

                            listdata.Add(ed);
                        }

                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listdata;
        }

        public List<EmployerData> salaryEmployeeListData()
        {
            List<EmployerData> listdata = new List<EmployerData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM employes WHERE date_suppression IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            EmployerData ed = new EmployerData();
                            ed.MatriculeID = reader["id_employe"].ToString();
                            ed.Nom = reader["nom_complet"].ToString();
                            ed.Position = reader["poste"].ToString();
                            ed.Salaire = reader["salaire"] != DBNull.Value ? Convert.ToInt32(reader["salaire"]) : 0;

                            listdata.Add(ed);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listdata;
        }
    }
}

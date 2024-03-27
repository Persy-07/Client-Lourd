using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GestionDeEmployer
{
    class SalaireData
    {

        public string MatriculeID { set; get; } // 0
        public string Nom { set; get; } // 1
        public string Genre { set; get; } // 2
        public string Contact { set; get; } // 3
        public string Position { set; get; } // 4
        public int Salaire { set; get; } // 5

        SqlConnection connect = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=employeDB;Integrated Security=True;");

        public List<SalaireData> salaryEmployeeListData()
        {
            List<SalaireData> listdata = new List<SalaireData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM employes WHERE statut = 'Actif' " +
                        "AND date_suppression IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SalaireData sd = new SalaireData();
                            sd.MatriculeID = reader["id_employe"].ToString();
                            sd.Nom = reader["nom_complet"].ToString();
                            sd.Genre = reader["genre"].ToString();
                            sd.Contact = reader["numero_contact"].ToString();
                            sd.Position = reader["poste"].ToString();
                            sd.Salaire = reader["salaire"] != DBNull.Value ? Convert.ToInt32(reader["salaire"]) : 0;

                            listdata.Add(sd);
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

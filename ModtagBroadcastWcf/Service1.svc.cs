using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ModtagBroadcastWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static string GetConnectionString()
        {
            ConnectionStringSettingsCollection connectionStringSettingsCollection = ConfigurationManager.ConnectionStrings;
            ConnectionStringSettings connStringSettings = connectionStringSettingsCollection["MikDatabaseAzure"];
            return connStringSettings.ConnectionString;
        }


        public IList<Temperatur> GetAllTemps()
        {
            const string selectAllTemps = "Select * from Temps";

            using(SqlConnection databaseConnection = new SqlConnection(GetConnectionString()))
            {
                databaseConnection.Open();
                using(SqlCommand selectCommand = new SqlCommand(selectAllTemps, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())

                    {
                        List<Temperatur> tempList = new List<Temperatur>();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string temp = reader.GetString(1);

                            Temperatur t1 = new Temperatur()
                            {
                                Id = id,
                                Temp = temp
                            };
                            tempList.Add(t1);
                        }
                        return tempList;
                    }
                }
            }
        }

        public int PostTempToList(string temp)
        {
            const string postTemp = "insert into Temps (Temps) values (@temps)";
            using (SqlConnection databaseConnection = new SqlConnection(GetConnectionString()))
            {
                databaseConnection.Open();
                using(SqlCommand insertCommand = new SqlCommand(postTemp, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@temps", temp);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;

                }
            }
        }

    }
}

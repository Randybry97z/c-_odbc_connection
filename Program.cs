using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 1. Dim strConn as String=”data source=localhost; initial catalog=dbAlumnos;”
2. Dim objConn as New SqlConnection(strConn)
3. Dim cmdTbProveedores as New Data.SqlClient.SqlCommand(“Select * From
Alumno”, objConn)
4. Try
5. objConn.Open
6. Dim dr as SqlDataReader
7. dr = cmdTbProveedores.ExecuteReader()
8. Do While dr.Read()
9. TextCta.Text = dr.Items(“NumCta”)
10. TextNombre.Text = dr.Items(“Nombre”)
11. TextPaterno.Text = dr.Items(“APaterno”)
12. Loop
13. dr.Close()
14. objConn.Close()
15. Catch (e as Exception)
16. MsgBox(e.message)
17. End Try
*/


namespace EvidenciaAprendizaje
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Connection string for Connector/ODBC 3.51
                string server = "SERVER=localhost;";
                string db = "DATABASE=desastres;";
                string user = "UID=root; PASSWORD=;";
                string MyConString = "DRIVER={MySQL ODBC 3.51 Driver};" +server + db + user;

                //Connect to MySQL using Connector/ODBC
                OdbcConnection MyConnection = new OdbcConnection(MyConString);
                MyConnection.Open();

                Console.WriteLine("\n !!! success, connected successfully !!!\n");

                //Display connection information
                Console.WriteLine("Connection Information:");
                Console.WriteLine("\tConnection String:" +
                                  MyConnection.ConnectionString);
                Console.WriteLine("\tConnection Timeout:" +
                                  MyConnection.ConnectionTimeout);
                Console.WriteLine("\tDatabase:" +
                                  MyConnection.Database);
                Console.WriteLine("\tDataSource:" +
                                  MyConnection.DataSource);
                Console.WriteLine("\tDriver:" +
                                  MyConnection.Driver);
                Console.WriteLine("\tServerVersion:" +
                                  MyConnection.ServerVersion);
            }
            catch (OdbcException MyOdbcException) //Catch any ODBC exception ..
            {
                for (int i = 0; i < MyOdbcException.Errors.Count; i++)
                {
                    Console.Write("ERROR #" + i + "\n" +
                                  "Message: " +
                                  MyOdbcException.Errors[i].Message + "\n" +
                                  "Native: " +
                                  MyOdbcException.Errors[i].NativeError.ToString() + "\n" +
                                  "Source: " +
                                  MyOdbcException.Errors[i].Source + "\n" +
                                  "SQL: " +
                                  MyOdbcException.Errors[i].SQLState + "\n");
                }
            }
        }
    }
}

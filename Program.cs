using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidenciaAprendizaje
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Parametros de la conexión
                string server = "SERVER=localhost;";
                string db = "DATABASE=desastres;";
                string user = "UID=root; PASSWORD=;";
                string MyConString = "DRIVER={MySQL ODBC 3.51 Driver};" +server + db + user;
                bool fin = false;
                //Conexión a MySQL con el conector ODBC
                OdbcConnection MyConnection = new OdbcConnection(MyConString);
                MyConnection.Open();

                Console.WriteLine("\n !!! success, connected successfully !!!\n");

                //Muestra la información de la conexión
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

                OdbcCommand MyCommand = new OdbcCommand(null, MyConnection);

                //Generación de id único
                string id = Guid.NewGuid().ToString("N");
                string id_final = id.Substring(0, 20);

                do
                {
                    Console.WriteLine("Ingrese el número de la operación a realizar");
                    Console.WriteLine("1.- insertar resultados");
                    Console.WriteLine("2.- consultar tabla");
                    int opcion = Int32.Parse(Console.ReadLine());
                    switch (opcion)
                    {
                        case 1:
                        //Insertar datos
                        Console.WriteLine("Indique la tabla a insertar datos: regiones / desastres");
                        string table = Console.ReadLine();

                        if (table == "regiones")
                        {
                            Console.WriteLine("Ingrese el nombre de la región");
                            string nombre = Console.ReadLine();
                            string sql = "INSERT INTO " + table + " VALUES('" + id_final + "','" + nombre + "')";

                            Console.WriteLine(sql);
                            MyCommand.CommandText = sql;
                            Console.WriteLine("INSERT, Total rows affected:" +
                                              MyCommand.ExecuteNonQuery()); ;

                        }
                        else if (table == "desastres")
                        {
                            Console.WriteLine("Ingrese los siguientes datos nombre, tipo, fecha de inicio(año-mes-dia), fecha de finalización(año-mes-dia) y el id de la región asociada");
                            string nombre = Console.ReadLine();
                            string tipo = Console.ReadLine();
                            string fecha_inicio = Console.ReadLine();
                            string fecha_fin = Console.ReadLine();
                            string id_region = Console.ReadLine();
                            string sql = "INSERT INTO " + table + " VALUES('" + id_final + "','" + nombre + "','" + tipo + "','" + fecha_inicio + "','" + fecha_fin +
                                    "','" + id_region + "')";

                            Console.WriteLine(sql);
                            MyCommand.CommandText = sql;
                            Console.WriteLine("INSERT, Total rows affected:" +
                                              MyCommand.ExecuteNonQuery());

                        }
                        else
                        {
                            Console.WriteLine("La tabla indicada no existe");

                        }
                            Console.WriteLine("Desea realizar otra operación  Si. No.");
                            string desicion = Console.ReadLine();
                            if (desicion == "no")
                            {
                                fin = false;
                            }
                            else if (desicion == "si")
                            {
                                fin = true;
                            }
                            else
                            {
                                Console.WriteLine("La opción no existe");
                                fin = false;
                            }
                            break;

                        case 2:
                            //Operación Select
                            Console.WriteLine("Indique la tabla que quiere consultar regiones / desastres");
                            table = Console.ReadLine();
                            MyCommand.CommandText = "SELECT * FROM " + table;
                            if (table == "regiones")
                            {
                                OdbcDataReader MyDataReader;
                                MyDataReader = MyCommand.ExecuteReader();
                                while (MyDataReader.Read())
                                {
                                    if (string.Compare(MyConnection.Driver, "myodbc3.dll") == 0)
                                    {
                                        //Mostramos los datos
                                        Console.WriteLine("Data: " + "id: " + MyDataReader.GetString(0) + " " + "nombre: " +
                                                          MyDataReader.GetString(1) + " ");

                                    }
                                    else
                                    {
                                        Console.WriteLine("Data: " + "id: " + MyDataReader.GetString(0) + " " + "nombre: " +
                                                          MyDataReader.GetString(1) + " ");
                                    }
                                }
                                //Cerramos el dataReader
                                MyDataReader.Close();
                            }
                            else if (table == "desastres")
                            {
                                OdbcDataReader MyDataReader;
                                MyDataReader = MyCommand.ExecuteReader();
                                while (MyDataReader.Read())
                                {
                                    if (string.Compare(MyConnection.Driver, "myodbc3.dll") == 0)
                                    {
                                        //Mostramos los datos
                                        Console.WriteLine("Data: \n" + "{\n id: \t" + MyDataReader.GetString(0) + " " + "\n nombre: \t" +
                                                          MyDataReader.GetString(1) + " " + "\n tipo: \t" + MyDataReader.GetString(2) + " " + "\n fecha de inicio: \t" +
                                                          MyDataReader.GetString(3) + " " + "\n fecha finalización: \t" + MyDataReader.GetString(4) + " " + "\n id de la región: \t" +
                                                          MyDataReader.GetString(5) + " \n}");

                                    }
                                    else
                                    {
                                        Console.WriteLine("Data: \n" + "{\n id: \t" + MyDataReader.GetString(0) + " " + "\n nombre: \t" +
                                                          MyDataReader.GetString(1) + " " + "\n tipo: \t" + MyDataReader.GetString(2) + " " + "\n fecha de inicio: \t" +
                                                          MyDataReader.GetString(3) + " " + "\n fecha finalización: \t" + MyDataReader.GetString(4) + " " + "\n id de la región: \t" +
                                                          MyDataReader.GetString(5) + " \n}");
                                    }
                                }
                                //Cerramos el dataReader
                                MyDataReader.Close();
                            }
                            else
                            {
                                Console.WriteLine("La tabla no existe");
                            }

                            Console.WriteLine("Desea realizar otra operación  Si. No.");
                            desicion = Console.ReadLine();
                            if(desicion == "no")
                            {
                                fin = false;
                            }
                            else if(desicion == "si")
                            {
                                fin = true;
                            }
                            else
                            {
                                Console.WriteLine("La opción no existe");
                                fin = false;
                            }
                            break;
                        default:
                            Console.WriteLine("Selecciona una opción");

                            break;
                    }

                } while (fin);

                MyConnection.Close();
            }
            catch (OdbcException MyOdbcException) //Capturando cualquier excepción de odbc
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

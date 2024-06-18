// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace TrackerCarbonFootprint
{
    public class Menu
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=TU_SERVIDOR;Database=CarbonFootprintTrackerDB;Trusted_Connection=True;";
            List<RecyclingEntry> entries = new List<RecyclingEntry>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Menú de Huella de Carbono ====");
                Console.WriteLine("1. Registrar entrada de reciclaje");
                Console.WriteLine("2. Ver todas las entradas");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterEntry(entries, connectionString);
                        break;
                    case "2":
                        ViewEntries(connectionString);
                        break;
                    case "3":
                        Console.WriteLine("Saliendo...");
                        return;
                    default:
                        Console.WriteLine("Opción no válida, intente nuevamente.");
                        break;
                }
            }
        }
    }

    public static void RegisterEntry(List<RecyclingEntry> entries, string connectionString)
    {
        Console.Clear();
        Console.WriteLine("=== Registro de Reciclaje ===");
        Console.Write("Nombre: ");
        string name = Console.ReadLine();
        Console.Write("ID: ");
        string id = Console.ReadLine();
        Console.Write("Fecha (dd/mm/aaaa): ");
        string date = Console.ReadLine();
        Console.Write("Cantidad de Plástico (kg): ");
        double plastic = Convert.ToDouble(Console.ReadLine());
        Console.Write("Cantidad de Metal (kg): ");
        double metal = Convert.ToDouble(Console.ReadLine());
        Console.Write("Cantidad de Cartón (kg): ");
        double cardboard = Convert.ToDouble(Console.ReadLine());
        Console.Write("Ubicación: ");
        string location = Console.ReadLine();

        RecyclingEntry entry = new RecyclingEntry(name, id, date, plastic, metal, cardboard, location);
        entries.Add(entry);

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO RecyclingEntries (Name, ID, Date, Plastic, Metal, Cardboard, Location) " +
                           "VALUES (@Name, @ID, @Date, @Plastic, @Metal, @Cardboard, @Location)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Date", DateTime.Parse(date));
                cmd.Parameters.AddWithValue("@Plastic", plastic);
                cmd.Parameters.AddWithValue("@Metal", metal);
                cmd.Parameters.AddWithValue("@Cardboard", cardboard);
                cmd.Parameters.AddWithValue("@Location", location);

                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Entrada registrada exitosamente en la base de datos.");
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }


    
    public static void ViewEntries(string connectionString)
        {
            Console.Clear();
            Console.WriteLine("=== Entradas de Reciclaje Registradas ===");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Name, ID, Date, Plastic, Metal, Cardboard, Location FROM RecyclingEntries";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RecyclingEntry entry = new RecyclingEntry(
                            reader["Name"].ToString(),
                            reader["ID"].ToString(),
                            ((DateTime)reader["Date"]).ToString("dd/MM/yyyy"),
                            Convert.ToDouble(reader["Plastic"]),
                            Convert.ToDouble(reader["Metal"]),
                            Convert.ToDouble(reader["Cardboard"]),
                            reader["Location"].ToString()
                        );

                        Console.WriteLine(entry);
                        Console.WriteLine("-------------------------------");
                    }
                }
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        //creacion de la instancia
        public class RecyclingEntry
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Date { get; set; }
        public double Plastic { get; set; }
        public double Metal { get; set; }
        public double Cardboard { get; set; }
        public string Location { get; set; }

        public RecyclingEntry(string name, string id, string date, double plastic, double metal, double cardboard, string location)
        {
            Name = name;
            ID = id;
            Date = date;
            Plastic = plastic;
            Metal = metal;
            Cardboard = cardboard;
            Location = location;
        }

        public override string ToString()
        {
            return "Nombre: " + Name + "\nID: " + ID + "\nFecha: " + Date + "\nPlástico: " + Plastic + " kg\nMetal: " + Metal + " kg\nCartón: " + Cardboard + " kg\nUbicación: " + Location;
        }
        //coneccion a la base de datos
        public string connectionString = "localhost;Database=CarbonFootprintTrackerDB;Trusted_Connection=True;";
    }
}

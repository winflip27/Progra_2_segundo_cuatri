// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;

namespace TrackerCarbonFootprint
{
    class Menu
    {
        static void Main(string[] args)
        {
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
                        RegisterEntry(entries);
                        break;
                    case "2":
                        ViewEntries(entries);
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

        static void RegisterEntry(List<RecyclingEntry> entries)
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

            Console.WriteLine("Entrada registrada exitosamente.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void ViewEntries(List<RecyclingEntry> entries)
        {
            Console.Clear();
            Console.WriteLine("=== Entradas de Reciclaje Registradas ===");
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
                Console.WriteLine("-------------------------------");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
    //creacion de la instancia
    class RecyclingEntry
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
    }
}

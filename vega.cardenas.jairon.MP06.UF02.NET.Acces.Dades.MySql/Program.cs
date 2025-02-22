using System;
using MySql.Data.MySqlClient;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            //InsertarVehiculo("Toyota", "Corolla", 450);
            //LeerVehiculos();
            //ActualizarVehiculo(3, "Honda", "Civic", 500);
            //LeerVehiculos();
            //EliminarVehiculo(3);
            //LeerVehiculos();

            /*
            Console.WriteLine("Añade los datos del vehiculo");
            Console.Write("Marca: ");
            string marca = Console.ReadLine();
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();
            Console.Write("Capacidad Maletero: ");
            int capacidadMaletero = int.Parse(Console.ReadLine());
            InsertarVehiculo(marca, modelo, capacidadMaletero);
            LeerVehiculos();
            */
            

            /*
            Console.WriteLine("Actualizar Vehiculo:");
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nueva Marca: ");
            string marca = Console.ReadLine();
            Console.Write("Nuevo Modelo: ");
            string modelo = Console.ReadLine();
            Console.Write("Nueva Capacidad Maletero: ");
            int capacidadMaletero = int.Parse(Console.ReadLine());
            ActualizarVehiculo(id, marca, modelo, capacidadMaletero);

            Console.WriteLine("Leer Vehiculos:");
            LeerVehiculos();
            */

            
            Console.WriteLine("Eliminar Vehiculo:");
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            EliminarVehiculo(id);

            Console.WriteLine("Leer Vehiculos:");
            LeerVehiculos();

            
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    static MySqlConnection ConnectToMYSQL()
    {
        string connectionString = "Server=localhost;Database=MP06_UF02_AEA1;User=root;Password=root";
        MySqlConnection connection = new MySqlConnection(connectionString);

        return connection;
    }

    public static void InsertarVehiculo(string marca, string modelo, int capacidadMaletero)
    {
        string query = "INSERT INTO Vehicles (Marca, Model, CapacitatMaleter) VALUES (@Marca, @Model, @Capacidad)";
        MySqlConnection connection = ConnectToMYSQL();
        connection.Open();
        using (connection)
        {
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Marca", marca);
                cmd.Parameters.AddWithValue("@Model", modelo);
                cmd.Parameters.AddWithValue("@Capacidad", capacidadMaletero);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Vehiculo insertado correctamente.");
            }
        }
    }

    public static void LeerVehiculos()
    {
        string query = "SELECT * FROM Vehicles";
        MySqlConnection connection = ConnectToMYSQL();
        connection.Open();
        using (connection)
        {
            using (var command = new MySqlCommand(query, connection))
            using (var datosDevueltos = command.ExecuteReader())
            {
                while (datosDevueltos.Read())
                {
                    Console.WriteLine($"ID: {datosDevueltos["Id"]}, Marca: {datosDevueltos["Marca"]}, Modelo: {datosDevueltos["Model"]}, Capacidad Maletero: {datosDevueltos["CapacitatMaleter"]}");
                }
                Console.WriteLine("Final de la lectura");
            }
        }
    }

    public static void ActualizarVehiculo(int id, string marca, string modelo, int capacidadMaletero)
    {
        string query = "UPDATE Vehicles SET Marca = @Marca, Model = @Model, CapacitatMaleter = @Capacidad WHERE Id = @Id";
        MySqlConnection connection = ConnectToMYSQL();
        connection.Open();
        using (connection)
        { 
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Marca", marca);
                cmd.Parameters.AddWithValue("@Model", modelo);
                cmd.Parameters.AddWithValue("@Capacidad", capacidadMaletero);
                cmd.Parameters.AddWithValue("@Id", id);
                int filasAfectadas = cmd.ExecuteNonQuery();
                Console.WriteLine(filasAfectadas > 0 ? "Vehiculo actualizado." : "No se encontró el vehiculo.");
            }
        }
    }

    public static void EliminarVehiculo(int id)
    {
        string query = "DELETE FROM Vehicles WHERE Id = @Id";
        MySqlConnection connection = ConnectToMYSQL();
        connection.Open();
        using (connection)
        {
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                int filasAfectadas = cmd.ExecuteNonQuery();
                Console.WriteLine(filasAfectadas > 0 ? "Vehiculo eliminado." : "No se encontró el vehiculo.");
            }
        }
    }
}

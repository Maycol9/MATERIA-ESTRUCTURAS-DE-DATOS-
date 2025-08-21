using System;
using System.Collections.Generic;

// Nodo para la lista enlazada de asientos
class Asiento
{
    public string Nombre { get; set; }
    public int Numero { get; set; }
    public Asiento Siguiente { get; set; }
}

class Program
{
    static Queue<string> colaEspera = new Queue<string>();
    static Asiento cabeza = null;
    static Stack<string> pilaSalidos = new Stack<string>();
    static int ASIENTOS_MAX = 30;

    static void Main(string[] args)
    {
        Console.WriteLine("Simulación de Cola para Atracción en Parque de Diversiones\n");
        Console.WriteLine("Ingresa los nombres de las personas en la fila (uno por línea, deja vacío para terminar):");

        // El usuario agrega los nombres manualmente
        while (true)
        {
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
                break;
            colaEspera.Enqueue(nombre.Trim());
        }

        // Asignación de asientos en orden de llegada
        int asientosDisponibles = ASIENTOS_MAX;
        int numeroAsiento = 1;
        List<string> rechazados = new List<string>();

        while (colaEspera.Count > 0)
        {
            string persona = colaEspera.Dequeue();
            if (asientosDisponibles > 0)
            {
                InsertarAsientoFinal(persona, numeroAsiento);
                Console.WriteLine($"{persona} recibe el asiento #{numeroAsiento}.");
                numeroAsiento++;
                asientosDisponibles--;
            }
            else
            {
                Console.WriteLine($"{persona} no consiguió asiento y fue rechazado.");
                rechazados.Add(persona);
            }
        }

        Console.WriteLine("\nAsientos ocupados:");
        MostrarAsientos();

        // Buscar persona
        Console.WriteLine("\nIngresa un nombre para buscar en los asientos:");
        string buscar = Console.ReadLine();
        int asientoEncontrado = BuscarPersona(buscar);
        if (asientoEncontrado != -1)
            Console.WriteLine($"{buscar} se encuentra en el asiento: {asientoEncontrado}");
        else
            Console.WriteLine($"{buscar} NO se encuentra en los asientos.");

        // Eliminar persona (simula que se baja de la atracción)
        Console.WriteLine("\nIngresa el nombre de quien abandona la atracción:");
        string eliminar = Console.ReadLine();
        if (EliminarAsiento(eliminar))
        {
            pilaSalidos.Push(eliminar);
            Console.WriteLine($"{eliminar} salió de la atracción.");
        }
        else
        {
            Console.WriteLine($"{eliminar} no estaba en la lista de asientos.");
        }

        Console.WriteLine("\nAsientos después de eliminación:");
        MostrarAsientos();

        // Ordenar asientos por nombre
        Console.WriteLine("\nAsientos ordenados por nombre:");
        OrdenarAsientosPorNombre();
        MostrarAsientos();

        // Mostrar pila de salidos
        Console.WriteLine("\nPersonas que salieron de la atracción:");
        while (pilaSalidos.Count > 0)
            Console.WriteLine(pilaSalidos.Pop());

        // Mostrar rechazados
        if (rechazados.Count > 0)
        {
            Console.WriteLine("\nPersonas que no consiguieron asiento:");
            foreach (var nombre in rechazados)
                Console.WriteLine(nombre);
        }
    }

    // Métodos de la lista enlazada para asientos
    static void InsertarAsientoFinal(string nombre, int numero)
    {
        Asiento nuevo = new Asiento { Nombre = nombre, Numero = numero, Siguiente = null };
        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            Asiento actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
    }

    static int BuscarPersona(string nombre)
    {
        Asiento actual = cabeza;
        while (actual != null)
        {
            if (actual.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                return actual.Numero;
            actual = actual.Siguiente;
        }
        return -1;
    }

    static bool EliminarAsiento(string nombre)
    {
        Asiento actual = cabeza, previo = null;
        while (actual != null)
        {
            if (actual.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
            {
                if (previo == null)
                    cabeza = actual.Siguiente;
                else
                    previo.Siguiente = actual.Siguiente;
                return true;
            }
            previo = actual;
            actual = actual.Siguiente;
        }
        return false;
    }

    static void MostrarAsientos()
    {
        Asiento actual = cabeza;
        while (actual != null)
        {
            Console.WriteLine($"Asiento #{actual.Numero}: {actual.Nombre}");
            actual = actual.Siguiente;
        }
    }

    // Ordenar asientos por nombre (burbuja sobre lista enlazada)
    static void OrdenarAsientosPorNombre()
    {
        if (cabeza == null) return;
        bool huboCambios;
        do
        {
            huboCambios = false;
            Asiento actual = cabeza;
            while (actual.Siguiente != null)
            {
                if (string.Compare(actual.Nombre, actual.Siguiente.Nombre, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    // Intercambiar datos
                    string tempNombre = actual.Nombre;
                    int tempNumero = actual.Numero;
                    actual.Nombre = actual.Siguiente.Nombre;
                    actual.Numero = actual.Siguiente.Numero;
                    actual.Siguiente.Nombre = tempNombre;
                    actual.Siguiente.Numero = tempNumero;
                    huboCambios = true;
                }
                actual = actual.Siguiente;
            }
        } while (huboCambios);
    }
}

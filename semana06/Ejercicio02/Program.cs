using System;

// Clase para representar un nodo de la lista enlazada
public class Nodo
{
    public int Dato { get; set; }
    public Nodo Siguiente { get; set; }

    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

// Clase para la lista enlazada
public class ListaEnlazada
{
    private Nodo cabeza;

    public ListaEnlazada()
    {
        cabeza = null;
    }

    // Método para agregar un elemento a la lista
    public void Agregar(int dato)
    {
        Nodo nuevoNodo = new Nodo(dato);
        
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }

    // Método de búsqueda que retorna el número de veces que aparece el dato
    public int Buscar(int datoBuscado)
    {
        int contador = 0;
        Nodo actual = cabeza;

        while (actual != null)
        {
            if (actual.Dato == datoBuscado)
            {
                contador++;
            }
            actual = actual.Siguiente;
        }

        if (contador == 0)
        {
            Console.WriteLine("El dato no fue encontrado.");
        }

        return contador;
    }
}

// Programa principal para probar la implementación
public class Program
{
    public static void Main()
    {
        ListaEnlazada lista = new ListaEnlazada();

        // Agregar elementos
        lista.Agregar(10);
        lista.Agregar(20);
        lista.Agregar(10);
        lista.Agregar(30);
        lista.Agregar(10);

        // Probar el método de búsqueda
        int resultado1 = lista.Buscar(10); // Debería retornar 3
        Console.WriteLine($"El dato 10 aparece {resultado1} veces.");

        int resultado2 = lista.Buscar(20); // Debería retornar 1
        Console.WriteLine($"El dato 20 aparece {resultado2} veces.");

        int resultado3 = lista.Buscar(50); // Debería mostrar mensaje y retornar 0
        Console.WriteLine($"El dato 50 aparece {resultado3} veces.");
    }
}

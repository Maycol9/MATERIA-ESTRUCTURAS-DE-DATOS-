using System;

// Definición del nodo para la lista enlazada
public class Nodo
{
    public int Dato { get; set; }         // Valor almacenado en el nodo
    public Nodo Siguiente { get; set; }   // Referencia al siguiente nodo

    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

// Definición de la lista enlazada simple
public class ListaEnlazada
{
    public Nodo Inicio { get; set; }  // Apunta al primer nodo de la lista

    public ListaEnlazada()
    {
        Inicio = null; // Lista vacía al inicio
    }

    // Método para agregar un dato al final de la lista
    public void Agregar(int dato)
    {
        Nodo nuevo = new Nodo(dato);
        if (Inicio == null)
        {
            Inicio = nuevo; // Si la lista está vacía, el nuevo nodo es el inicio
        }
        else
        {
            Nodo actual = Inicio;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo; // Se agrega al final
        }
    }

    // Método para buscar cuántas veces aparece un valor en la lista
    public int Buscar(int valor)
    {
        int contador = 0;
        Nodo actual = Inicio;
        while (actual != null)
        {
            if (actual.Dato == valor)
                contador++;
            actual = actual.Siguiente;
        }

        if (contador == 0)
            Console.WriteLine($"El dato {valor} NO fue encontrado en la lista.");

        return contador;
    }

    // Método para imprimir la lista enlazada
    public void Imprimir()
    {
        Nodo actual = Inicio;
        while (actual != null)
        {
            Console.Write(actual.Dato + " -> ");
            actual = actual.Siguiente;
        }
        Console.WriteLine("null");
    }
}

// Programa principal
class Program
{
    static void Main(string[] args)
    {
        ListaEnlazada lista = new ListaEnlazada();

        // Agregamos algunos datos de ejemplo
        lista.Agregar(5);
        lista.Agregar(10);
        lista.Agregar(7);
        lista.Agregar(10);
        lista.Agregar(3);

        Console.WriteLine("La lista es:");
        lista.Imprimir();

        // Se pide al usuario el dato a buscar
        Console.Write("\nIngrese el dato a buscar: ");
        int valor = int.Parse(Console.ReadLine());

        // Se llama al método de búsqueda
        int veces = lista.Buscar(valor);

        if (veces > 0)
            Console.WriteLine($"El dato {valor} se encuentra {veces} vez/veces en la lista.");
    }
}

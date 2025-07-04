using System;

public class Nodo
{
    public int Dato;
    public Nodo Siguiente;

    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class ListaEnlazada
{
    public Nodo Cabeza;

    public ListaEnlazada()
    {
        Cabeza = null;
    }

    // Método para agregar al final
    public void Agregar(int dato)
    {
        Nodo nuevo = new Nodo(dato);
        if (Cabeza == null)
        {
            Cabeza = nuevo;
        }
        else
        {
            Nodo actual = Cabeza;
            while (actual.Siguiente != null)
                actual = actual.Siguiente;
            actual.Siguiente = nuevo;
        }
    }

    // Método para invertir la lista enlazada
    public void Invertir()
    {
        Nodo anterior = null;
        Nodo actual = Cabeza;
        Nodo siguiente = null;

        while (actual != null)
        {
            siguiente = actual.Siguiente;
            actual.Siguiente = anterior;
            anterior = actual;
            actual = siguiente;
        }
        Cabeza = anterior;
    }

    // Método para mostrar la lista
    public void Mostrar()
    {
        Nodo actual = Cabeza;
        while (actual != null)
        {
            Console.Write(actual.Dato + " -> ");
            actual = actual.Siguiente;
        }
        Console.WriteLine("null");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ListaEnlazada lista = new ListaEnlazada();
        lista.Agregar(10);
        lista.Agregar(20);
        lista.Agregar(30);
        lista.Agregar(40);

        Console.WriteLine("Lista original:");
        lista.Mostrar();

        lista.Invertir();

        Console.WriteLine("Lista invertida:");
        lista.Mostrar();
    }
}

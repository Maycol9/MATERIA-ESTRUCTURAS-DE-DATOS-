using System;

public class Nodo<T>
{
    public T Dato { get; set; }
    public Nodo<T> Siguiente { get; set; }

    public Nodo(T dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class ListaEnlazada<T>
{
    private Nodo<T> cabeza;

    public ListaEnlazada()
    {
        cabeza = null;
    }

    public void Agregar(T dato)
    {
        Nodo<T> nuevoNodo = new Nodo<T>(dato);
        
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            Nodo<T> actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }

    public void Invertir()
    {
        if (cabeza == null || cabeza.Siguiente == null)
        {
            return;
        }

        Nodo<T> anterior = null;
        Nodo<T> actual = cabeza;
        Nodo<T> siguiente = null;

        while (actual != null)
        {
            siguiente = actual.Siguiente;
            actual.Siguiente = anterior;
            anterior = actual;
            actual = siguiente;
        }

        cabeza = anterior;
    }

    public void MostrarLista()
    {
        if (cabeza == null)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }

        Nodo<T> actual = cabeza;
        while (actual != null)
        {
            Console.Write(actual.Dato);
            if (actual.Siguiente != null)
            {
                Console.Write(" -> ");
            }
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }
}

public class Program
{
    public static void Main()
    {
        ListaEnlazada<int> lista = new ListaEnlazada<int>();

        lista.Agregar(1);
        lista.Agregar(2);
        lista.Agregar(3);
        lista.Agregar(4);
        lista.Agregar(5);

        Console.WriteLine("Lista original:");
        lista.MostrarLista();

        lista.Invertir();

        Console.WriteLine("Lista invertida:");
        lista.MostrarLista();
    }
}

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Crear la lista con los números del 1 al 10
        List<int> numeros = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            numeros.Add(i);
        }

        // Invertir la lista
        numeros.Reverse();

        // Mostrar los números separados por comas
        Console.WriteLine(string.Join(", ", numeros));

        Console.ReadLine(); // Pausa la consola
    }
}

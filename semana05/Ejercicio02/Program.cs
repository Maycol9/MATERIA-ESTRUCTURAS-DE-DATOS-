using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numerosLoteria = new List<int>();
        int cantidadNumeros = 6;

        Console.WriteLine("Ingrese los 6 números ganadores de la lotería primitiva:");

        for (int i = 0; i < cantidadNumeros; i++)
        {
            Console.Write($"Número {i + 1}: ");
            int numero;

            while (!int.TryParse(Console.ReadLine(), out numero) || numero < 1 || numero > 49)
            {
                Console.Write("Número inválido. Ingrese un número entre 1 y 49: ");
            }

            numerosLoteria.Add(numero);
        }

        numerosLoteria.Sort();

        Console.WriteLine("\nNúmeros ganadores ordenados de menor a mayor:");
        foreach (int num in numerosLoteria)
        {
            Console.WriteLine(num);
        }

        Console.ReadLine(); // Para que no se cierre la consola
    }
}

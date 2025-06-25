using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Crear la lista con el abecedario
        List<char> abecedario = new List<char>();
        for (char letra = 'A'; letra <= 'Z'; letra++)
        {
            abecedario.Add(letra);
        }

        // Eliminar letras en posiciones múltiplos de 3 (1-based index)
        for (int i = abecedario.Count - 1; i >= 0; i--)
        {
            if ((i + 1) % 3 == 0)
            {
                abecedario.RemoveAt(i);
            }
        }

        // Mostrar la lista resultante
        Console.WriteLine("Letras restantes:");
        foreach (char letra in abecedario)
        {
            Console.Write(letra + " ");
        }

        Console.ReadLine(); // Pausa la consola
    }
}

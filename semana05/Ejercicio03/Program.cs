using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Lista de asignaturas
        List<string> asignaturas = new List<string>()
        {
            "Matemáticas",
            "Física",
            "Química",
            "Historia",
            "Lengua"
        };

        // Diccionario para almacenar las notas por asignatura
        Dictionary<string, double> notas = new Dictionary<string, double>();

        // Pedir las notas al usuario
        foreach (string asignatura in asignaturas)
        {
            Console.Write($"¿Qué nota sacaste en {asignatura}? ");
            double nota;

            // Validar que la entrada sea un número
            while (!double.TryParse(Console.ReadLine(), out nota) || nota < 0 || nota > 10)
            {
                Console.Write("Nota inválida. Ingresa un número entre 0 y 10: ");
            }

            notas[asignatura] = nota;
        }

        // Mostrar resultados
        Console.WriteLine("\nResumen de notas:");
        foreach (var par in notas)
        {
            Console.WriteLine($"En {par.Key} has sacado {par.Value}");
        }

        Console.ReadLine(); // Pausa la consola
    }
}

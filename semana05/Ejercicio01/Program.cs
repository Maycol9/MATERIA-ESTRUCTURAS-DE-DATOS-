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

        // Mostrar las asignaturas
        Console.WriteLine("Asignaturas del curso:");
        foreach (string asignatura in asignaturas)
        {
            Console.WriteLine("- " + asignatura);
        }

        // Esperar para que no se cierre la consola de inmediato
        Console.ReadLine();
    }
}

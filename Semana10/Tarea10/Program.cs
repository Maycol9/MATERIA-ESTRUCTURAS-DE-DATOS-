using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

record Citizen(int Id, string Name);

class Program
{
    // Parámetros del problema
    const int TOTAL_CIUDADANOS = 500;
    const int PFIZER_COUNT = 75;
    const int ASTRA_COUNT = 75;

    static void Main()
    {
        // Semilla fija para resultados reproducibles (puedes cambiarla o quitarla)
        var rnd = new Random(42);

        // 1) Generar universo de 500 ciudadanos
        var citizens = GenerateCitizens(TOTAL_CIUDADANOS, rnd);

        // Índices y Diccionario auxiliar para mapear ID -> Nombre
        var idUniverse = new HashSet<int>(citizens.Select(c => c.Id));
        var idToName = citizens.ToDictionary(c => c.Id, c => c.Name);

        // 2) Crear conjuntos ficticios de vacunados
        //    Elegimos 75 IDs distintos para Pfizer y 75 para AstraZeneca (con posible intersección)
        var pfizer = PickRandomIds(PFIZER_COUNT, TOTAL_CIUDADANOS, rnd);
        var astra  = PickRandomIds(ASTRA_COUNT,  TOTAL_CIUDADANOS, rnd);

        // 3) Operaciones de teoría de conjuntos
        var unionPfizerAstra = new HashSet<int>(pfizer);
        unionPfizerAstra.UnionWith(astra);

        var noVacunados = new HashSet<int>(idUniverse);
        noVacunados.ExceptWith(unionPfizerAstra);                 // U \ (A ∪ B)

        var ambasVacunas = new HashSet<int>(pfizer);
        ambasVacunas.IntersectWith(astra);                         // A ∩ B

        var soloPfizer = new HashSet<int>(pfizer);
        soloPfizer.ExceptWith(astra);                              // A \ B

        var soloAstra = new HashSet<int>(astra);
        soloAstra.ExceptWith(pfizer);                              // B \ A

        // 4) Reporte en consola
        Console.OutputEncoding = Encoding.UTF8;
        PrintHeader("RESUMEN CAMPAÑA DE VACUNACIÓN (DATOS FICTICIOS)");
        Console.WriteLine($"Total ciudadanos (U): {TOTAL_CIUDADANOS}");
        Console.WriteLine($"Pfizer (|A|): {pfizer.Count}");
        Console.WriteLine($"AstraZeneca (|B|): {astra.Count}");
        Console.WriteLine($"Ambas vacunas (A ∩ B): {ambasVacunas.Count}");
        Console.WriteLine($"Solo Pfizer (A \\ B): {soloPfizer.Count}");
        Console.WriteLine($"Solo AstraZeneca (B \\ A): {soloAstra.Count}");
        Console.WriteLine($"No vacunados (U \\ (A ∪ B)): {noVacunados.Count}");
        Console.WriteLine();

        // 5) Exportar listados a TXT
        WriteList("no_vacunados.txt", noVacunados, idToName);
        WriteList("ambas_vacunas.txt", ambasVacunas, idToName);
        WriteList("solo_pfizer.txt", soloPfizer, idToName);
        WriteList("solo_astrazeneca.txt", soloAstra, idToName);

        // 6) Exportar CSV de resumen global
        WriteCsvResumen("vacunacion_resumen.csv", idUniverse, pfizer, astra, idToName);

        // 7) Mostrar muestra en consola (opcional)
        PrintSample("MUESTRA: NO VACUNADOS", noVacunados, idToName, 10);
        PrintSample("MUESTRA: AMBAS VACUNAS (A ∩ B)", ambasVacunas, idToName, 10);
        PrintSample("MUESTRA: SOLO PFIZER (A \\ B)", soloPfizer, idToName, 10);
        PrintSample("MUESTRA: SOLO ASTRAZENECA (B \\ A)", soloAstra, idToName, 10);

        Console.WriteLine("\nArchivos generados:");
        Console.WriteLine(" - no_vacunados.txt");
        Console.WriteLine(" - ambas_vacunas.txt");
        Console.WriteLine(" - solo_pfizer.txt");
        Console.WriteLine(" - solo_astrazeneca.txt");
        Console.WriteLine(" - vacunacion_resumen.csv");
    }

    static void PrintHeader(string title)
    {
        Console.WriteLine(new string('=', title.Length));
        Console.WriteLine(title);
        Console.WriteLine(new string('=', title.Length));
    }

    static void PrintSample(string title, HashSet<int> set, Dictionary<int, string> idToName, int count)
    {
        Console.WriteLine($"\n{title} (primeros {count}):");
        foreach (var id in set.OrderBy(x => x).Take(count))
        {
            Console.WriteLine($"  {id:0000} - {idToName[id]}");
        }
    }

    static void WriteList(string fileName, HashSet<int> ids, Dictionary<int, string> idToName)
    {
        var lines = ids.OrderBy(x => x).Select(id => $"{id:0000};{idToName[id]}");
        File.WriteAllLines(fileName, lines, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
    }

    static void WriteCsvResumen(string fileName, HashSet<int> universe, HashSet<int> pfizer, HashSet<int> astra, Dictionary<int, string> idToName)
    {
        using var sw = new StreamWriter(fileName, false, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
        sw.WriteLine("Id,Nombre,Pfizer,AstraZeneca,Estado");

        foreach (var id in universe.OrderBy(x => x))
        {
            bool p = pfizer.Contains(id);
            bool a = astra.Contains(id);
            string estado = (!p && !a) ? "No vacunado"
                           : (p && a)   ? "Ambas vacunas"
                           : (p && !a)  ? "Solo Pfizer"
                           : "Solo AstraZeneca";

            sw.WriteLine($"{id:0000},{EscapeCsv(idToName[id])},{p},{a},{estado}");
        }
    }

    static string EscapeCsv(string input)
    {
        if (input.Contains(',') || input.Contains('"') || input.Contains('\n'))
        {
            return $"\"{input.Replace("\"", "\"\"")}\"";
        }
        return input;
    }

    static List<Citizen> GenerateCitizens(int n, Random rnd)
    {
        var firstNames = new[]
        {
            "Liam","Noah","Oliver","Elijah","Mateo","Santiago","Sebastian","Lucas","Ethan","Mason",
            "Benjamin","Alexander","Henry","Theodore","Jack","Levi","Daniel","Michael","Samuel","David",
            "Gabriel","Tomás","Nikolai","Viktor","Omar","Youssef","Hiroshi","Søren","Finn","Arthur",
            "Hugo","Enzo","Matías","Bruno","Iker","Álvaro","Diego","Pablo","Carlos","Juan",
            "Andrés","Alan","Kevin","Dylan","Leo","William","Nathan","Jason","Aaron","Marco",
            "Luca","Giovanni","Emir","Mustafa","Arjun","Ravi","Johan","Frederik","Thiago","Gael"
        };

        var lastNames = new[]
        {
            "Smith","Johnson","Williams","Brown","Jones","Miller","Davis","García","Martínez","López",
            "González","Rodríguez","Hernández","Pérez","Sánchez","Ramírez","Torres","Flores","Rivera","Gómez",
            "Silva","Almeida","Costa","Santos","Rossi","Bianchi","Moretti","Kowalski","Nowak","Novák",
            "Popov","Ivanov","Petrov","Kuznetsov","Yamamoto","Tanaka","Suzuki","Kobayashi","Nakamura","Kim",
            "Park","Choi","Schmidt","Müller","Weber","Dupont","Moreau","Lefevre","O’Connor","Murphy",
            "Khan","Hussain","Patel","Singh","Andersson","Johansson","Nielsen","Hansen","Østergaard","Eriksen"
        };

        var usedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var citizens = new List<Citizen>(n);

        int id = 1;
        while (citizens.Count < n)
        {
            string name = $"{firstNames[rnd.Next(firstNames.Length)]} {lastNames[rnd.Next(lastNames.Length)]}";
            if (usedNames.Add(name))
            {
                citizens.Add(new Citizen(id, name));
                id++;
            }
        }
        return citizens;
    }

    static HashSet<int> PickRandomIds(int count, int total, Random rnd)
    {
        // IDs válidos: 1..total
        var pool = Enumerable.Range(1, total).ToList();
        // Mezclar (Fisher–Yates)
        for (int i = pool.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            (pool[i], pool[j]) = (pool[j], pool[i]);
        }
        return new HashSet<int>(pool.Take(count));
    }
}


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    // Diccionarios internos con claves normalizadas (sin tildes y en minúsculas)
    static Dictionary<string, string> esToEn = new Dictionary<string, string>();
    static Dictionary<string, string> enToEs = new Dictionary<string, string>();

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        InicializarDiccionariosBase();

        while (true)
        {
            Console.WriteLine("\n==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            var opt = Console.ReadLine()?.Trim();

            switch (opt)
            {
                case "1":
                    OpcionTraducir();
                    break;
                case "2":
                    OpcionAgregarPalabra();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    static void OpcionTraducir()
    {
        Console.WriteLine("\nSeleccione el sentido de traducción:");
        Console.WriteLine("1. Español → Inglés");
        Console.WriteLine("2. Inglés → Español");
        Console.Write("Opción: ");
        var sentido = Console.ReadLine()?.Trim();

        Console.Write("\nIngrese la frase: ");
        var frase = Console.ReadLine() ?? "";

        bool esToEnDir = sentido == "1";
        string resultado = TraducirFrase(frase, esToEnDir ? esToEn : enToEs);

        Console.WriteLine("\nTraducción:");
        Console.WriteLine(resultado);
    }

    static void OpcionAgregarPalabra()
    {
        Console.WriteLine("\n¿En qué dirección desea agregar?");
        Console.WriteLine("1. Español → Inglés");
        Console.WriteLine("2. Inglés → Español");
        Console.Write("Opción: ");
        var dir = Console.ReadLine()?.Trim();

        if (dir == "1")
        {
            Console.Write("Palabra en español: ");
            string es = Console.ReadLine() ?? "";
            Console.Write("Equivalente en inglés: ");
            string en = Console.ReadLine() ?? "";

            AgregarPar(es, en, esToEn, enToEs);
            Console.WriteLine("Agregado: ES→EN y EN→ES.");
        }
        else if (dir == "2")
        {
            Console.Write("Word in English: ");
            string en = Console.ReadLine() ?? "";
            Console.Write("Equivalente en español: ");
            string es = Console.ReadLine() ?? "";

            AgregarPar(es, en, esToEn, enToEs);
            Console.WriteLine("Agregado: EN→ES y ES→EN.");
        }
        else
        {
            Console.WriteLine("Dirección no válida.");
        }
    }

    static void InicializarDiccionariosBase()
    {
        // Lista base sugerida (inglés — español)
        var basePairs = new (string en, string es)[]
        {
            ("time", "tiempo"),
            ("person", "persona"),
            ("year", "año"),
            ("way", "camino"),
            ("way", "forma"),          // sinónimos
            ("day", "día"),
            ("thing", "cosa"),
            ("man", "hombre"),
            ("world", "mundo"),
            ("life", "vida"),
            ("hand", "mano"),
            ("part", "parte"),
            ("child", "niño"),
            ("child", "niña"),
            ("eye", "ojo"),
            ("woman", "mujer"),
            ("place", "lugar"),
            ("work", "trabajo"),
            ("week", "semana"),
            ("case", "caso"),
            ("point", "punto"),
            ("point", "tema"),
            ("government", "gobierno"),
            ("company", "empresa"),
            ("company", "compañía")
        };

        foreach (var (en, es) in basePairs)
        {
            AgregarPar(es, en, esToEn, enToEs);
        }
    }

    static void AgregarPar(string es, string en, Dictionary<string, string> esEn, Dictionary<string, string> enEs)
    {
        string kEs = Normalizar(es);
        string kEn = Normalizar(en);

        // En caso de sinónimos: preferimos no sobreescribir; si no existe, añadimos
        if (!esEn.ContainsKey(kEs)) esEn[kEs] = en.Trim();
        if (!enEs.ContainsKey(kEn)) enEs[kEn] = es.Trim();
    }

    static string TraducirFrase(string frase, Dictionary<string, string> dic)
    {
        // Partir conservando separadores (espacios, comas, puntos, etc.)
        // Grupo 1 = palabra (letras/apóstrofos), Grupo 2 = no-palabra
        var tokens = Regex.Matches(frase, "([\\p{L}]+(?:'[\\p{L}]+)?)|([^\\p{L}]+)")
                          .Cast<Match>()
                          .Select(m => m.Value)
                          .ToList();

        for (int i = 0; i < tokens.Count; i++)
        {
            if (Regex.IsMatch(tokens[i], "^[\\p{L}]+(?:'[\\p{L}]+)?$"))
            {
                string original = tokens[i];
                string clave = Normalizar(original);

                if (dic.TryGetValue(clave, out string traduccion))
                {
                    tokens[i] = AplicarMayusculasSegunPatron(traduccion, original);
                }
            }
        }

        return string.Concat(tokens);
    }

    // Normaliza: minúsculas + sin tildes/diacríticos
    static string Normalizar(string s)
    {
        s = (s ?? "").Trim().ToLowerInvariant();
        var formD = s.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in formD)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(c);
            if (uc != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    // Mantener estilo de mayúsculas:
    // - TODO MAYÚSCULAS → TODO MAYÚSCULAS
    // - Capitalizada (Primera) → Capitalizada
    // - Todo minúsculas → minúsculas
    static string AplicarMayusculasSegunPatron(string palabra, string patron)
    {
        if (string.IsNullOrEmpty(palabra)) return palabra;

        bool esUpper = patron.All(ch => !char.IsLetter(ch) || char.IsUpper(ch));
        bool esCapitalizada = char.IsLetter(patron.FirstOrDefault()) &&
                              char.IsUpper(patron.First()) &&
                              patron.Skip(1).All(ch => !char.IsLetter(ch) || char.IsLower(ch));

        if (esUpper) return palabra.ToUpperInvariant();
        if (esCapitalizada)
            return char.ToUpperInvariant(palabra[0]) + (palabra.Length > 1 ? palabra.Substring(1).ToLowerInvariant() : "");

        return palabra.ToLowerInvariant();
    }
}

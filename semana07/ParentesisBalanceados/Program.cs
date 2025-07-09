using System;
using System.Collections.Generic;

class BalanceadorParentesis
{
    static bool EstáBalanceado(string expr)
    {
        // Diccionario para emparejar cierres con aperturas
        var pares = new Dictionary<char, char>
        {
            { ')', '(' },
            { '}', '{' },
            { ']', '[' }
        };

        var pila = new Stack<char>();

        foreach (char c in expr)
        {
            // Si es paréntesis, llave o corchete de apertura, lo apilamos
            if (c == '(' || c == '{' || c == '[')
            {
                pila.Push(c);
            }
            // Si es símbolo de cierre, comprobamos
            else if (c == ')' || c == '}' || c == ']')
            {
                // Si pila vacía o par incorrecto, no está balanceado
                if (pila.Count == 0 || pila.Pop() != pares[c])
                    return false;
            }
            // Ignoramos otros caracteres
        }

        // Al final, debe estar vacía para estar balanceado
        return pila.Count == 0;
    }

    static void Main()
    {
        // Ejemplos de prueba
        var ejemplos = new[]
        {
            "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}",
            "( [ ) ]",
            "((2+3)*5)"
        };

        foreach (var expr in ejemplos)
        {
            bool balanceado = EstáBalanceado(expr);
            Console.WriteLine($"{expr} -> {(balanceado ? "Fórmula balanceada." : "Fórmula NO balanceada.")}");
        }

        // Para lectura desde consola:
        Console.WriteLine("\nIngrese una expresión a verificar:");
        string entrada = Console.ReadLine();
        Console.WriteLine(EstáBalanceado(entrada)
            ? "Fórmula balanceada."
            : "Fórmula NO balanceada.");
    }
}

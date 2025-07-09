using System;
using System.Collections.Generic;

namespace TorresHanoi
{
    class Program
    {
        static void MoverDiscos(int n, Stack<int> origen, Stack<int> aux, Stack<int> destino,
                                string nOrigen, string nAux, string nDestino)
        {
            if (n == 1)
            {
                int disco = origen.Pop();
                destino.Push(disco);
                Console.WriteLine($"Mover disco {disco} de {nOrigen} a {nDestino}");
                return;
            }
            MoverDiscos(n - 1, origen, destino, aux, nOrigen, nDestino, nAux);
            MoverDiscos(1, origen, aux, destino, nOrigen, nAux, nDestino);
            MoverDiscos(n - 1, aux, origen, destino, nAux, nOrigen, nDestino);
        }

        static void Main()
        {
            Console.Write("Número de discos: ");
            int n = int.Parse(Console.ReadLine());
            var A = new Stack<int>();
            var B = new Stack<int>();
            var C = new Stack<int>();
            for (int i = n; i >= 1; i--) A.Push(i);

            Console.WriteLine("\nMovimientos:");
            MoverDiscos(n, A, B, C, "A", "B", "C");
        }
    }
}

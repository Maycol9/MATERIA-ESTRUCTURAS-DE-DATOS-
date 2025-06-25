/*
 * Nombre: Maycol Sleyter Perez Figueroa
 * Fecha: 07 de Junio de 2025
 * Hoja: 1
 * 
 * Tarea: Clases de Figuras Geométricas
 * Descripción: Implementación de dos clases (Circulo y Rectangulo) que encapsulan
 * tipos de datos primitivos y proporcionan métodos para calcular área y perímetro.
 */

using System;

namespace FigurasGeometricas
{
    // Clase Circulo que encapsula las propiedades y métodos de un círculo
    public class Circulo
    {
        // Tipo de dato primitivo encapsulado - radio del círculo (private para encapsulación)
        private double radio;

        // Constructor que inicializa el radio del círculo
        // Recibe como parámetro el radio del círculo
        public Circulo(double radio)
        {
            // Validación para asegurar que el radio sea positivo
            if (radio <= 0)
            {
                throw new ArgumentException("El radio debe ser mayor que cero");
            }
            this.radio = radio;
        }

        // Propiedad pública para acceder al radio (encapsulación)
        // Permite obtener y establecer el valor del radio de forma controlada
        public double Radio
        {
            get { return radio; }
            set 
            { 
                if (value <= 0)
                {
                    throw new ArgumentException("El radio debe ser mayor que cero");
                }
                radio = value; 
            }
        }

        // CalcularArea es una función que devuelve un valor double
        // Se utiliza para calcular el área de un círculo usando la fórmula π * r²
        // Requiere que el radio esté previamente establecido
        public double CalcularArea()
        {
            return Math.PI * radio * radio;
        }

        // CalcularPerimetro es una función que devuelve un valor double
        // Se utiliza para calcular el perímetro (circunferencia) de un círculo usando la fórmula 2 * π * r
        // Requiere que el radio esté previamente establecido
        public double CalcularPerimetro()
        {
            return 2 * Math.PI * radio;
        }

        // Método ToString sobrescrito para mostrar información del círculo
        // Devuelve una representación en cadena del objeto círculo
        public override string ToString()
        {
            return $"Círculo con radio: {radio:F2}";
        }
    }

    // Clase Rectangulo que encapsula las propiedades y métodos de un rectángulo
    public class Rectangulo
    {
        // Tipos de datos primitivos encapsulados - base y altura del rectángulo
        private double baseRectangulo;
        private double altura;

        // Constructor que inicializa la base y altura del rectángulo
        // Recibe como parámetros la base y altura del rectángulo
        public Rectangulo(double baseRectangulo, double altura)
        {
            // Validación para asegurar que base y altura sean positivas
            if (baseRectangulo <= 0 || altura <= 0)
            {
                throw new ArgumentException("La base y altura deben ser mayores que cero");
            }
            this.baseRectangulo = baseRectangulo;
            this.altura = altura;
        }

        // Propiedad pública para acceder a la base (encapsulación)
        // Permite obtener y establecer el valor de la base de forma controlada
        public double Base
        {
            get { return baseRectangulo; }
            set 
            { 
                if (value <= 0)
                {
                    throw new ArgumentException("La base debe ser mayor que cero");
                }
                baseRectangulo = value; 
            }
        }

        // Propiedad pública para acceder a la altura (encapsulación)
        // Permite obtener y establecer el valor de la altura de forma controlada
        public double Altura
        {
            get { return altura; }
            set 
            { 
                if (value <= 0)
                {
                    throw new ArgumentException("La altura debe ser mayor que cero");
                }
                altura = value; 
            }
        }

        // CalcularArea es una función que devuelve un valor double
        // Se utiliza para calcular el área de un rectángulo usando la fórmula base * altura
        // Requiere que la base y altura estén previamente establecidas
        public double CalcularArea()
        {
            return baseRectangulo * altura;
        }

        // CalcularPerimetro es una función que devuelve un valor double
        // Se utiliza para calcular el perímetro de un rectángulo usando la fórmula 2 * (base + altura)
        // Requiere que la base y altura estén previamente establecidas
        public double CalcularPerimetro()
        {
            return 2 * (baseRectangulo + altura);
        }

        // Método ToString sobrescrito para mostrar información del rectángulo
        // Devuelve una representación en cadena del objeto rectángulo
        public override string ToString()
        {
            return $"Rectángulo con base: {baseRectangulo:F2} y altura: {altura:F2}";
        }
    }

    // Clase principal para demostrar el uso de las figuras geométricas
    class Program
    {
        // Método Main - punto de entrada del programa
        // Demuestra el uso de las clases Circulo y Rectangulo
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== DEMOSTRACIÓN DE FIGURAS GEOMÉTRICAS ===\n");

                // Crear instancia de Circulo con radio 5.0
                Circulo circulo = new Circulo(5.0);
                
                Console.WriteLine(circulo.ToString());
                Console.WriteLine($"Área del círculo: {circulo.CalcularArea():F2}");
                Console.WriteLine($"Perímetro del círculo: {circulo.CalcularPerimetro():F2}\n");

                // Crear instancia de Rectangulo con base 8.0 y altura 4.0
                Rectangulo rectangulo = new Rectangulo(8.0, 4.0);
                
                Console.WriteLine(rectangulo.ToString());
                Console.WriteLine($"Área del rectángulo: {rectangulo.CalcularArea():F2}");
                Console.WriteLine($"Perímetro del rectángulo: {rectangulo.CalcularPerimetro():F2}\n");

                // Demostrar el uso de propiedades para modificar valores
                Console.WriteLine("=== MODIFICANDO VALORES ===\n");
                
                circulo.Radio = 3.0;
                Console.WriteLine($"Nuevo radio del círculo: {circulo.Radio}");
                Console.WriteLine($"Nueva área del círculo: {circulo.CalcularArea():F2}\n");

                rectangulo.Base = 10.0;
                rectangulo.Altura = 6.0;
                Console.WriteLine($"Nuevas dimensiones del rectángulo - Base: {rectangulo.Base}, Altura: {rectangulo.Altura}");
                Console.WriteLine($"Nueva área del rectángulo: {rectangulo.CalcularArea():F2}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
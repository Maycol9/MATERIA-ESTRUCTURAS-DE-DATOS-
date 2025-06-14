using System;

namespace RegistroEstudiante
{
    // Clase que representa la estructura de un estudiante
    public class Estudiante
    {
        // Campos privados que contienen los datos del estudiante
        private int id;                        // Identificador único del estudiante
        private string nombres;               // Nombres del estudiante
        private string apellidos;             // Apellidos del estudiante
        private string direccion;             // Dirección domiciliaria
        private string[] telefonos = new string[3]; // Array que almacena tres números telefónicos




        /// <summary>
        /// Constructor que inicializa un objeto Estudiante con todos sus datos
        /// </summary>
        /// <param name="id">Identificador único del estudiante</param>
        /// <param name="nombres">Nombres completos</param>
        /// <param name="apellidos">Apellidos completos</param>
        /// <param name="direccion">Dirección de residencia</param>
        /// <param name="telefonos">Array con exactamente 3 números de teléfono</param>
        public Estudiante(int id, string nombres, string apellidos, string direccion, string[] telefonos)
        {
            // Asignamos los valores a los atributos correspondientes
            this.id = id;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.direccion = direccion;

            // Validación: se asegura que el array contenga exactamente 3 teléfonos
            if (telefonos.Length == 3)
            {
                this.telefonos = telefonos;
            }
            else
            {

                throw new ArgumentException("Debe ingresar exactamente 3 números de teléfono.");
            }
        }

        /// <summary>
        /// Método que imprime en consola todos los datos del estudiante de forma ordenada
        /// </summary>
        public void MostrarDatos()
        {
            Console.WriteLine("========= DATOS DEL ESTUDIANTE =========");
            Console.WriteLine("ID: " + id);
            Console.WriteLine("Nombres: " + nombres);
            Console.WriteLine("Apellidos: " + apellidos);
            Console.WriteLine("Dirección: " + direccion);
            Console.WriteLine("Teléfonos registrados:");
            for (int i = 0; i < telefonos.Length; i++)
            {
                Console.WriteLine($"Teléfono {i + 1}: {telefonos[i]}");
            }
        }
    }




    // Clase principal donde se ejecuta el programa
    class Program
    {
        static void Main(string[] args)
        {
            // Creamos un array con tres números telefónicos del estudiante
            string[] telefonosJuan = { "0991112233", "0987654321", "0960004455" };

            // Creamos una instancia de la clase Estudiante con datos de ejemplo
            Estudiante estudianteJuan = new Estudiante(
                id: 1001,
                nombres: "Juan Carlos",
                apellidos: "Rodríguez López",
                direccion: "Av. 10 de Agosto y Colón, Quito",
                telefonos: telefonosJuan
            );
            // Mostramos todos los datos del estudiante en la consola
            estudianteJuan.MostrarDatos();

            // Pausamos la consola para visualizar los resultados antes de que se cierre (opcional en Visual Studio)
            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromedioTresGrupos
{
    public class Randomizer {

        private Random _random;
        public Randomizer() {
            _random = new Random();
        }

        public double GenerateRandomGrade() {
            double grade = -1.0;
            grade = _random.NextDouble() * 10.0;
            return grade;
        }

        public void FillWithRandomGrades(double[] grades) {
            int index = -1;
            if (!(grades is null)) {
                foreach (var grade in grades) {
                    index++;
                    grades[index] = GenerateRandomGrade();
                }
            }
        }
    } //++: FIN clase Randomizer
    class Program
    {
        public static void ShowMultiGroupGrades(double[][] multiGroupGrades) {
            int controlIndex;
            for (int idx = 0; idx < multiGroupGrades.Length; idx++) {
                Console.WriteLine($"--Calificaciones del grupo {idx + 1}: ");
                controlIndex = -1;
                foreach (double grade in multiGroupGrades[idx]) {
                    controlIndex++;
                    if (controlIndex != multiGroupGrades[idx].Length - 1) {
                        //Console.Write(grade + ", ");
                        Console.Write("{0:f2}{1}", grade, ", ");
                    }
                    else {
                        //Console.Write(grade);
                        Console.Write("{0:f2}", grade);
                    }
                    
                }
                Console.WriteLine($"\n##--FIN de Calificaciones del grupo {idx + 1}--##");
            }
        }

        public static void InitMGGWithRandomValues(double[][] multiGruopGrades) {
            Randomizer randomizer = new Randomizer();
            for (int idx = 0; idx < multiGruopGrades.Length; idx++) {
                randomizer.FillWithRandomGrades(multiGruopGrades[idx]);
            }
        }

        public static double PromedioGrupal(double[] grades) {
            double promedio = 0.0, acumulador = 0.0;
            foreach (double grade in grades) {
                acumulador += grade;
            }
            promedio = acumulador / grades.Length;
            return promedio;
        }

        public static void CalcularPromedios(double[][] multiGroupGrades) {
            double[] promedios = new double[3];
            double promedioGlobal;
            int idxTmp = 0;
            for (int idx = 0; idx < multiGroupGrades.Length; idx++) {
                promedios[idx] = PromedioGrupal(multiGroupGrades[idx]);
            }

            promedioGlobal = PromedioGrupal(promedios);

            foreach (double promedio in promedios) {
                idxTmp++;
                Console.WriteLine($"Promedio del GRUPO {idxTmp}: {promedio}");
            }
            Console.WriteLine($"Promedio GLOBAL (de los 3 Grupos): {promedioGlobal}");

        }
        static void Main(string[] args)
        {
            Randomizer randomizer = new Randomizer();
            char continueP = 'n';
            byte numGrupo = 0;
            int numAlumnosTmp;
            bool successfulAlumnRead;
            // jagged Array para almacenar las Calificaciones de los 3 Grupos
            double[][] multiGroupGrades = new double[3][]; 
            do {
                Console.Clear();
                Console.WriteLine("##--Promedio de 3 Grupos--##");

                // 1. Solicitud del tamaño de cada grupo
                for(int idx = 0; idx < multiGroupGrades.Length; idx++) {
                    numGrupo++;
                    Console.Write($"Ingrese el número de alumnos del Grupo {numGrupo}: ");
                    successfulAlumnRead = int.TryParse(Console.ReadLine(), out numAlumnosTmp);
                    if (successfulAlumnRead) {
                        multiGroupGrades[idx] = new double[numAlumnosTmp];
                    }
                }

                // 2. Llenado de Calificaciones Aleatorias
                Console.WriteLine("Inicializando calificaciones aleatorias...");
                InitMGGWithRandomValues(multiGroupGrades);

                // 3. Mostrar Calificaciones en Pantalla:
                ShowMultiGroupGrades(multiGroupGrades);

                // 4. Calcular PROMEDIOS
                CalcularPromedios(multiGroupGrades);

                Console.Write("\n\t¿Desea calcular el promedio de OTROS 3 Grupos? [y/n]: ");
                continueP = Console.ReadKey().KeyChar;
            } while (Char.ToLower(continueP).Equals('y'));
        }
    }
}

using System;

namespace Lab01_FranciscoMorales_1223319
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuar = true;
            do
            {
                Console.WriteLine("Ingrese una expresion algebraica:");
                string algexp = Console.ReadLine();
                Parser parser = new Parser();
                try
                {
                    double result = parser.Parse(algexp);
                    Console.WriteLine($"El resultado es: {result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.Write("¿Desea seguir ingresando expresiones? (y/n) ");
                if (Console.ReadKey().KeyChar.ToString().ToLower() == "n")
                    continuar = false;
                else
                    Console.WriteLine();
            } while (continuar);
        }
    }
}

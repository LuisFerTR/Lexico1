using System;

namespace sintaxis1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (Lenguaje l = new Lenguaje())
                {
                    /*while (!l.FinDeArchivo())
                    {
                        l.NextToken();
                    }*/
                    l.match("#");
                    l.match("include");
                    l.match("<");
                    l.match(Token.clasificaciones.identificador);
                    l.match(".");
                    l.match("h");
                    l.match(">");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

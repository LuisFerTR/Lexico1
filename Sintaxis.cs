using System;
using System.Collections.Generic;
using System.Text;


namespace sintaxis3
{
    class Sintaxis: Lexico
    {
        public Sintaxis()
        {
            Console.WriteLine("Iniciando analisis sintactico.");
            NextToken();
        }

        public Sintaxis(string nombre): base(nombre)
        {
            Console.WriteLine("Iniciando analisis sintactico.");
            NextToken();
        }

        protected void match(string espera)
        {
            if (espera == getContenido())
            {
                NextToken();
            }
            else
            {
                throw new Exception("Error de sintaxis: Se espera un " + espera);
            }
        }

        protected void match(clasificaciones espera)
        {
            if (espera == getClasificacion())
            {
                NextToken();
            }
            else
            {
                throw new Exception("Error de sintaxis: Se espera un " + espera);
            }
        }
    }
}

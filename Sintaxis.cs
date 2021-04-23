using System;
using System.Collections.Generic;
using System.Text;

<<<<<<< HEAD

namespace sintaxis1
=======
namespace sintaxis3
>>>>>>> ccfe9b2 (Agregar archivos de proyecto.)
{
    class Sintaxis: Lexico
    {
        public Sintaxis()
        {
            Console.WriteLine("Iniciando analisis sintactico.");
            NextToken();
        }

<<<<<<< HEAD
        public Sintaxis(string nombre): base(nombre)
=======
        public Sintaxis(string nombre) : base(nombre)
>>>>>>> ccfe9b2 (Agregar archivos de proyecto.)
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

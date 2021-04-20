using System;
using System.Collections.Generic;
using System.Text;

// Requerimiento 1: Agregar en el archivo log la fecha y la hora de compilación.

namespace sintaxis1
{
    class Sintaxis: Lexico
    {
        public Sintaxis()
        {
            NextToken();
        }

        public void match(string espera)
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

        public void match(clasificaciones espera)
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

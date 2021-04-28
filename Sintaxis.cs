﻿using System;
using System.Collections.Generic;
using System.Text;

namespace sintaxis3
{
    class Sintaxis: Lexico
    {
        int caracterAnterior;
        public Sintaxis()
        {
            Console.WriteLine("Iniciando analisis sintactico.");
            caracterAnterior = 0;
            NextToken();
        }

        public Sintaxis(string nombre): base(nombre)
        {
            Console.WriteLine("Iniciando analisis sintactico.");
            caracterAnterior = 0;
            NextToken();
        }

        protected void match(string espera)
        {
            // Console.WriteLine(getContenido() + " = " + espera);
            if (espera == getContenido())
            {                
                NextToken(); 
            }
            else
            {
                string mensaje = String.Format("Error de sintaxis linea {0} caracter {1}: " +
                                               "Se espera un {2}", linea, caracter, espera);
                throw new Exception(mensaje);
            }
        }

        protected void match(clasificaciones espera)
        {
            // Console.WriteLine(getContenido() + " = " + espera);
            if (espera == getClasificacion())
            {
                caracterAnterior = caracter;
                NextToken();                
            }
            else
            {
                int lineaError = linea;
                if(espera == clasificaciones.finSentencia || espera == clasificaciones.finBloque)
                {
                    lineaError--;
                }
                string mensaje = String.Format("Error de sintaxis linea {0} caracter {1}: " +
                                               "Se espera un {2}", linea, caracterAnterior, espera);
                throw new Exception(mensaje);
            }
        }
    }
}

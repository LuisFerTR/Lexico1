using System;
using System.Collections.Generic;
using System.Text;

// Requerimiento 1: Separar el nombre del archivo y el directorio en el constructor Lexico(string)
// Requerimiento 2: Validar en el constructor Lexico(string) que la extensión del archivo deba de ser
// cpp y levantar una excepción en caso contrario.

namespace sintaxis3
{
    class Lenguaje: Sintaxis
    {
        public Lenguaje()
        {
            Console.WriteLine("Iniciando analisis gramatical.");
        }

        public Lenguaje(string nombre): base(nombre)
        {
            Console.WriteLine("Iniciando analisis gramatical.");
        }

        // Programa -> Libreria Main
        public void Programa()
        {
            Libreria();
            Main();
        }

        // Libreria -> (#include <identificador.h> Libreria) ?
        private void Libreria()
        {
            if (getContenido() == "#")
            {
                match("#");
                match("include");
                match("<");
                match(Token.clasificaciones.identificador);

                if (getContenido() == ".")
                {
                    match(".");
                    match("h");
                }

                match(">");

                Libreria();
            }
        }

        // Libreria -> #include <identificador(.h)?> Libreria ?
        private void Libreria2()
        {
            match("#");
            match("include");
            match("<");
            match(Token.clasificaciones.identificador);

            if (getContenido() == ".")
            {
                match(".");
                match("h");
            }

            match(">");

            if (getContenido() == "#")
            {
                Libreria2();
            }
        }

        // Main -> void main() { (Variables)? Instrucciones } 
        private void Main()
        {
            match("void");
            match("main");
            match("(");
            match(")");
            match(clasificaciones.inicioBloque);

            if (getClasificacion() == clasificaciones.tipoDato)
            {
                Variables();
            }

            Instrucciones();

            match(clasificaciones.finBloque);
        }

        // Lista_IDs -> identificador (,Lista_IDs)?
        private void Lista_IDs()
        {
            match(clasificaciones.identificador);

            if (getContenido() == ",")
            {
                match(",");
                Lista_IDs();
            }
        }

        // Variables -> tipoDato Lista_IDs; (Variables)?
        private void Variables()
        {
            match(clasificaciones.tipoDato);
            Lista_IDs();
            match(clasificaciones.finSentencia);

            if (getClasificacion() == clasificaciones.tipoDato)
            {
                Variables();
            }
        }

        // Instruccion -> (inicializacion | printf(cadena | identificador | numero)) ;
        private void Instruccion()
        {
            if (getContenido() == "printf")
            {
                match("printf");
                match("(");

                if (getClasificacion() == clasificaciones.numero)
                {
                    match(clasificaciones.numero);
                }
                else if (getClasificacion() == clasificaciones.cadena)
                {
                    match(clasificaciones.cadena);
                }
                else
                {
                    match(clasificaciones.identificador);
                }

                match(")");
            }
            else
            {
                match(clasificaciones.identificador);
                match(clasificaciones.inicializacion);

                if (getClasificacion() == clasificaciones.numero)
                {
                    match(clasificaciones.numero);
                }
                else if (getClasificacion() == clasificaciones.cadena)
                {
                    match(clasificaciones.cadena);
                }
                else
                {
                    match(clasificaciones.identificador);
                }

            }

            match(clasificaciones.finSentencia);
        }

        // Instrucciones -> Instruccion Instrucciones?
        private void Instrucciones()
        {
            Instruccion();

            if (getClasificacion() == clasificaciones.identificador)
            {
                Instrucciones();
            }
        }
    }
}

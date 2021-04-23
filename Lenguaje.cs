using System;
using System.Collections.Generic;
using System.Text;

<<<<<<< HEAD
// Requerimiento 1: Ajustar el constructor Lexico(string) para que substraiga el nombre del archivo
// y el directorio.

namespace sintaxis1
=======
// Requerimiento 1: Separar el nombre del archivo y el directorio en el constructor Lexico(string)
// Requerimiento 2: Validar en el constructor Lexico(string) que la extensión del archivo deba de ser
// cpp y levantar una excepción en caso contrario.

namespace sintaxis3
>>>>>>> ccfe9b2 (Agregar archivos de proyecto.)
{
    class Lenguaje: Sintaxis
    {
        public Lenguaje()
        {
            Console.WriteLine("Iniciando analisis gramatical.");
        }

<<<<<<< HEAD
        public Lenguaje(string nombre): base(nombre)
=======
        public Lenguaje(string nombre) : base(nombre)
>>>>>>> ccfe9b2 (Agregar archivos de proyecto.)
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
<<<<<<< HEAD
                match(".");
                match("h");
                match(">");
            
=======
                if (getContenido() == ".")
                {
                    match(".");
                    match("h");
                }

                match(">");

>>>>>>> ccfe9b2 (Agregar archivos de proyecto.)
                Libreria();
            }
        }

<<<<<<< HEAD
        // Libreria -> #include <identificador.h> Libreria ?
=======
        // Libreria -> #include <identificador(.h)?> Libreria ?
>>>>>>> ccfe9b2 (Agregar archivos de proyecto.)
        private void Libreria2()
        {
            match("#");
            match("include");
            match("<");
            match(Token.clasificaciones.identificador);
<<<<<<< HEAD
            match(".");
            match("h");
=======

            if (getContenido() == ".")
            {
                match(".");
                match("h");
            }

>>>>>>> ccfe9b2 (Agregar archivos de proyecto.)
            match(">");

            if (getContenido() == "#")
            {
                Libreria2();
            }
        }

<<<<<<< HEAD
        // Main -> void main() { (Variables)? identificador := numero; } 
=======
        // Main -> void main() { (Variables)? Instrucciones } 
>>>>>>> ccfe9b2 (Agregar archivos de proyecto.)
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
<<<<<<< HEAD
            }            

            match(clasificaciones.identificador);
            match(clasificaciones.inicializacion);
            match(clasificaciones.numero);
            match(clasificaciones.finSentencia);
=======
            }

            Instrucciones();

>>>>>>> ccfe9b2 (Agregar archivos de proyecto.)
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

<<<<<<< HEAD
=======
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
>>>>>>> ccfe9b2 (Agregar archivos de proyecto.)
    }
}

using System;
using System.Collections.Generic;
using System.Text;

// Requerimiento 1: Ajustar el constructor Lexico(string) para que substraiga el nombre del archivo
// y el directorio.

namespace sintaxis1
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
                match(".");
                match("h");
                match(">");
            
                Libreria();
            }
        }

        // Libreria -> #include <identificador.h> Libreria ?
        private void Libreria2()
        {
            match("#");
            match("include");
            match("<");
            match(Token.clasificaciones.identificador);
            match(".");
            match("h");
            match(">");

            if (getContenido() == "#")
            {
                Libreria2();
            }
        }

        // Main -> void main() { (Variables)? identificador := numero; } 
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

            match(clasificaciones.identificador);
            match(clasificaciones.inicializacion);
            match(clasificaciones.numero);
            match(clasificaciones.finSentencia);
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

    }
}

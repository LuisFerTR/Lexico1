using System;
using System.Collections.Generic;
using System.Text;

// Requerimiento 2: Ajustar el constructor Lexico(string) para que substraiga el nombre del archivo
// y el directorio y genere el log correspondiente.
// Ejemplo: suma.txt, suma.log

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

        // Libreria -> #include <identificador.h>
        private void Libreria()
        {
            match("#");
            match("include");
            match("<");
            match(Token.clasificaciones.identificador);
            match(".");
            match("h");
            match(">");
        }

        // Main -> void main() { numero } 
        private void Main()
        {

        }
    }
}

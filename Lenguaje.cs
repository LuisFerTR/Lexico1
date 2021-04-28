using System;
using System.Collections.Generic;
using System.Text;

// ✅ Requerimiento 1: Separar el nombre del archivo y el directorio en el constructor Lexico(string)        
// ✅ Requerimiento 2: Validar en el constructor Lexico(string) que la extensión del archivo deba de ser    
//                     cpp y levantar una excepción en caso contrario.                                                  
// ❌ Requerimiento 3: Identificar errores sintácticos con línea y caracter y grabarlos en el log.          
// ✅ Requerimiento 4: Agregar el token flujo de entrada (<<) y flujo de salida (>>) en el análisis léxico. 
// ✅ Requerimiento 5: Implementar el if.

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
            if (getContenido() == "")
            {

            }
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

        // Main -> tipoDato main() BloqueInstrucciones 
        private void Main()
        {
            match(clasificaciones.tipoDato);
            match("main");
            match("(");
            match(")");

            BloqueInstrucciones();            
        }

        // BloqueInstrucciones -> { Instrucciones }
        private void BloqueInstrucciones()
        {
            match(clasificaciones.inicioBloque);

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

        // Variables -> tipoDato Lista_IDs; 
        private void Variables()
        {
            match(clasificaciones.tipoDato);
            Lista_IDs();
            match(clasificaciones.finSentencia);           
        }

        // Instruccion -> (If | cin | cout | const | Variables | asignacion) ;
        private void Instruccion()
        {
            if (getContenido() == "if")
            {
                If();
            }
            else if (getContenido() == "cin")
            {
                match("cin");
                match(">>");
                match(clasificaciones.identificador);
                match(clasificaciones.finSentencia);
            }
            else if (getContenido() == "cout")
            {
                match("cout");
                ListaFlujoSalida();
                match(clasificaciones.finSentencia);
            }
            else if (getContenido() == "const")
            {
                Constante();
            }
            else if (getClasificacion() == clasificaciones.tipoDato)
            {
                Variables();
            }            
            else
            {
                match(clasificaciones.identificador);
                match(clasificaciones.asignacion);

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

                match(clasificaciones.finSentencia);
            }

            
        }

        // Instrucciones -> Instruccion Instrucciones?
        private void Instrucciones()
        {
            Instruccion();

            if (getClasificacion() != clasificaciones.finBloque)
            {
                Instrucciones();
            }
        }

        // Constante -> const tipoDato identificador = numero | cadena;
        private void Constante()
        {
            match("const");
            match(clasificaciones.tipoDato);
            match(clasificaciones.identificador);
            match(clasificaciones.asignacion);

            if (getClasificacion() == clasificaciones.numero)
            {
                match(clasificaciones.numero);
            }
            else
            {
                match(clasificaciones.cadena);
            }
         
            match(clasificaciones.finSentencia);
        }

        // ListaFlujoSalida -> << cadena | identificador | numero (ListaFlujoSalida)?
        private void ListaFlujoSalida()
        {
            match("<<");

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

            if (getContenido() == "<<")
            {
                ListaFlujoSalida();
            }
        }

        // If -> if (Condicion) { BloqueInstrucciones } (else BloqueInstrucciones)?
        private void If()
        {
            match("if");
            match("(");
            Condicion();
            match(")");
            BloqueInstrucciones();

            if (getContenido() == "else")
            {
                match("else");
                BloqueInstrucciones();
            }
        }

        // Condicion -> identificador operadorRelacional identificador
        private void Condicion()
        {
            match(clasificaciones.identificador);
            match(clasificaciones.operadorRelacional);
            match(clasificaciones.identificador);
        }
    }
}

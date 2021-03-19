﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

// Requerimiento 1: Levantar la excepción en caso de error e identificar el tipo de error. ✓
// Requerimiento 2: Indicar en que línea y caracter se encuentra el error. ✓
// Requerimiento 3: Agregar el token "{" y "}" en la matriz trand6x. Nota: Agregar las columnas {,}. ✓
// Requerimiento 4: Agregar los comentarios de línea y multilínea. Nota: Agregar la columna #10. ✓
// Requerimiento 5: Grabar en el archivo log los errores léxicos. ✓
namespace Lexico3
{
    class Lexico: Token, IDisposable
    {
        private StreamReader archivo;
        private StreamWriter bitacora;
        int linea, caracter;
        const int F = -1;
        const int E = -2;
        int[,] trand6x = { // WS,EF, L, D, ., +, -, E, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?,La, {, },#10
                            {  0, F, 1, 2,29,17,18, 1, 8, 9,11,12,13,15,26,27,20,32,20,22,24,28,29,30,31, 0 },//0
                            {  F, F, 1, 1, F, F, F, 1, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//1
                            {  F, F, F, 2, 3, F, F, 5, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//2
                            {  E, E, E, 4, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },//3
                            {  F, F, F, 4, F, F, F, 5, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//4
                            {  E, E, E, 7, E, 6, 6, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },//5
                            {  E, E, E, 7, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },//6
                            {  F, F, F, 7, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//7
                            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//8
                            {  F, F, F, F, F, F, F, F,10, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//9
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//10
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//11
                            {  F, F, F, F, F, F, F, F, F, F, F,14, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//12
                            {  F, F, F, F, F, F, F, F, F, F, F, F,14, F, F, F, F, F, F, F, F, F, F, F, F, F },//13
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//14
                            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//15
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//16
                            {  F, F, F, F, F,19, F, F,19, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//17
                            {  F, F, F, F, F, F,19, F,19, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//18
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//19
                            {  F, F, F, F, F, F, F, F,21, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//20
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//21
                            { 22, E,22,22,22,22,22,22,22,22,22,22,22,22,22,22,22,22,22,23,22,22,22,22,22,22 },//22
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//23
                            { 24, E,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,25,24,24,24,24,24 },//24
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//25
                            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//26
                            {  F, F, F, F, F, F, F, F,16, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F },//27
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//28
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//29
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//30
                            {  F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F },//31
                            {  F, F, F, F, F, F, F, F,21, F, F, F, F, F, F, F,34,33, F, F, F, F, F, F, F, F },//32
                            { 33, 0,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33,33, 0 },//33
                            { 34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,35,34,34,34,34,34,34,34,34,34 },//34
                            { 34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,35, 0,34,34,34,34,34,34,34,34 },//35
                            // WS,EF, L, D, ., +, -, E, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?,La, {, },#10
                            };

        public Lexico()
        {
            linea = caracter = 1;

            Console.WriteLine("Compilando prueba.txt");

            if (File.Exists("C:\\Archivos\\prueba.txt"))
            {
                archivo = new StreamReader("C:\\Archivos\\prueba.txt");
                bitacora = new StreamWriter("C:\\Archivos\\prueba.log");
                bitacora.AutoFlush = true;

                bitacora.WriteLine("Archivo: prueba.txt");
                bitacora.WriteLine("Directorio: C:\\Archivos");
            }
            else
            {
                throw new Exception("El archivo prueba.txt no existe.");
            }

        }
        //~Lexico()
        public void Dispose()
        {
            Console.WriteLine("Finaliza compilacion de prueba.txt");
            CerrarArchivos();
        }

        private void CerrarArchivos()
        {
            archivo.Close();
            bitacora.Close();
        }

        public void NextToken()
        {
            char transicion;
            string palabra = "";
            int estado = 0;

            while (estado >= 0)
            {
                transicion = (char)archivo.Peek();

                estado = trand6x[estado, columna(transicion)];
                clasificar(estado);

                if (estado >= 0)
                {
                    archivo.Read();
                    caracter++;

                    if (transicion == 10)
                    {
                        linea++;
                        caracter = 1;
                    }

                    if (estado > 0 && estado < 33)
                    {
                        palabra += transicion;
                    }
                    else
                    {
                        palabra = "";
                    }
                }
            }

            if (estado == -2)
            {
                errorLexico(linea, caracter);
            }

            setContenido(palabra);
            switch (palabra)
            {
                case "char":
                case "int":
                case "float":
                    setClasificacion(clasificaciones.tipoDato);
                    break;

                case "private":
                case "public":
                case "protected":
                    setClasificacion(clasificaciones.zona);
                    break;

                case "if":
                case "else":
                case "switch":
                    setClasificacion(clasificaciones.condicion);
                    break;

                case "for":
                case "while":
                case "do":
                    setClasificacion(clasificaciones.ciclo);
                    break;
            }

            if (!string.IsNullOrWhiteSpace(palabra))
            {
                bitacora.WriteLine("Token = " + getContenido());
                bitacora.WriteLine("Clasificacion = " + getClasificacion());
            }
        }

        private void errorLexico(int linea, int caracter)
        {
            clasificaciones clasif = getClasificacion();
            string mensaje = "Error léxico linea " + linea + " caracter " + caracter + ": ";

            if (clasif == clasificaciones.cadena)
            {
                mensaje += "Se esperaban comillas (\") o comilla (') de cierre.";
                bitacora.WriteLine(mensaje);
                throw new Exception(mensaje);
            }
            else if (clasif == clasificaciones.numero)
            {
                mensaje += "Se esperaba un dígito.";
                bitacora.WriteLine(mensaje);
                throw new Exception(mensaje);
            }
        }

        private void clasificar(int estado)
        {
            switch(estado)
            {
                case 1:
                    setClasificacion(clasificaciones.identificador);
                    break;
                case 2:
                    setClasificacion(clasificaciones.numero);
                    break;
                case 8:
                    setClasificacion(clasificaciones.asignacion);
                    break;
                case 30:
                    setClasificacion(clasificaciones.inicioBloque);
                    break;
                case 31:
                    setClasificacion(clasificaciones.finBloque);
                    break;
                case 9:
                case 12:
                case 13:
                case 29:
                    setClasificacion(clasificaciones.caracter);
                    break;
                case 10:
                    setClasificacion(clasificaciones.inicializacion);
                    break;
                case 11:
                    setClasificacion(clasificaciones.finSentencia);
                    break;
                case 14:
                case 15:
                    setClasificacion(clasificaciones.operadorLogico);
                    break;
                case 16:
                case 26:
                case 27:
                    setClasificacion(clasificaciones.operadorRelacional);
                    break;
                case 17:
                case 18:
                    setClasificacion(clasificaciones.operadorTermino);
                    break;
                case 19:
                    setClasificacion(clasificaciones.incrementoTermino);
                    break;
                case 20:
                case 32:
                    setClasificacion(clasificaciones.operadorFactor);
                    break;
                case 21:
                    setClasificacion(clasificaciones.incrementoFactor);
                    break;
                case 22:
                case 24:
                    setClasificacion(clasificaciones.cadena);
                    break;
                case 28:
                    setClasificacion(clasificaciones.operadorTernario);
                    break;
            }
        }

        private int columna(char t)
        {
            // WS,EF, L, D, ., +, -, E, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?,La, {, },#10
            if (FinDeArchivo())
            {
                return 1;
            }
            else if (t == 10)
            {
                return 25;
            }
            else if (char.IsWhiteSpace(t))
            {
                return 0;
            }
            else if (char.ToLower(t) == 'e')
            {
                return 7;
            }
            else if (char.IsLetter(t))
            {
                return 2;
            }
            else if (char.IsDigit(t))
            {
                return 3;
            }
            else if (t == '.')
            {
                return 4;
            }
            else if (t == '+')
            {
                return 5;
            }
            else if (t == '-')
            {
                return 6;
            }
            else if (t == '=')
            {
                return 8;
            }
            else if (t == ':')
            {
                return 9;
            }
            else if (t == ';')
            {
                return 10;
            }
            else if (t == '&')
            {
                return 11;
            }
            else if (t == '|')
            {
                return 12;
            }
            else if (t == '!')
            {
                return 13;
            }
            else if (t == '>')
            {
                return 14;
            }
            else if (t == '<')
            {
                return 15;
            }
            else if (t == '*')
            {
                return 16;
            }
            else if (t == '/')
            {
                return 17;
            }
            else if (t == '%')
            {
                return 18;
            }
            else if (t == '"')
            {
                return 19;
            }
            else if (t == '\'')
            {
                return 20;
            }
            else if (t == '?')
            {
                return 21;
            }
            else if (t == '{')
            {
                return 23;
            }
            else if (t == '}')
            {
                return 24;
            }
            else 
            {
                return 22;
            }
            // WS,EF, L, D, ., +, -, E, =, :, ;, &, |, !, >, <, *, /, %, ", ', ?,La, {, },#10
        }

        public bool FinDeArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}

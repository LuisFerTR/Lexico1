using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lexico1
{
    class Lexico : Token, IDisposable 
    {
        private StreamReader archivo;
        private StreamWriter bitacora;

        public Lexico()
        {
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
            char c;
            string palabra = "";

            while (char.IsWhiteSpace(c = (char)archivo.Read()))
            {
                // Este ciclo busca el primer caracter válido
            }

            palabra += c;

            if (char.IsLetter(c))
            {
                setClasificacion(clasificaciones.identificador);
                // Encontró una letra
                while (char.IsLetterOrDigit(c = (char)archivo.Peek()))
                {
                    // Concatenar más letras para formar la palabra
                    palabra += c;
                    archivo.Read();
                }
            }
            else if (char.IsDigit(c))
            {
                setClasificacion(clasificaciones.numero);
                // Si no es letra es otro caracter
                while (char.IsDigit(c = (char)archivo.Peek()))
                {
                    // Concatenar más letras para formar la palabra
                    palabra += c;
                    archivo.Read();
                }
            }
            else if (c == '=')
            {
                setClasificacion(clasificaciones.asignacion);
            }
            else
            {
                setClasificacion(clasificaciones.caracter);
            }

            setContenido(palabra);
            bitacora.WriteLine("Token = " + getContenido());
            bitacora.WriteLine("Clasificacion = " + getClasificacion());  
        }

        public bool FinDeArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lexico3
{
    class Lexico: Token, IDisposable
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
            char transicion;
            string palabra = "";
            int estado = 0;
            const int F = -1;
            const int E = -2;

            while (estado >= 0)
            {
                transicion = (char)archivo.Peek();

                estado = maquinaTuring(transicion, estado);

                if (estado >= 0)
                {
                    archivo.Read();

                    if (estado > 0)
                    {
                        palabra += transicion;
                    }
                }
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

            bitacora.WriteLine("Token = " + getContenido());
            bitacora.WriteLine("Clasificacion = " + getClasificacion());
        }

        private int maquinaTuring(char t, int estado)
        {
            return estado;
        }

        public bool FinDeArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lexico2
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
            int estado = 0;
            const int F = -1;
            const int E = -2;

            while (estado >= 0)
            {
                c = (char)archivo.Peek();

                switch(estado)
                {
                    case 0: 
                        if (char.IsWhiteSpace(c))
                        {
                            estado = 0;
                        }
                        else if (char.IsLetter(c))
                        {
                            estado = 1;
                        }
                        else if (char.IsDigit(c))
                        {
                            estado = 2;
                        }
                        else
                        {
                            estado = 29;
                        }
                        break;
                    case 1:
                        setClasificacion(clasificaciones.identificador);
                        if (!char.IsLetterOrDigit(c))
                        {
                            estado = F;
                        }                   
                        break;
                    case 2:
                        setClasificacion(clasificaciones.numero);
                        if (char.IsDigit(c))
                        {
                            estado = 2;
                        }
                        else if (c == '.')
                        {
                            estado = 3;
                        }
                        else if (char.ToLower(c) == 'e')
                        {
                            estado = 5;
                        }
                        else
                        {
                            estado = F;
                        }
                        break;
                    case 3:
                        if (char.IsDigit(c))
                        {
                            estado = 4;
                        }
                        else
                        {
                            throw new Exception("Error lexico: Se espera un digito");
                        }
                        break;
                    case 4:
                        if (char.IsDigit(c))
                        {
                            estado = 4;
                        }                     
                        else if (char.ToLower(c) == 'e')
                        {
                            estado = 5;
                        }
                        else
                        {
                            estado = F;
                        }
                        break;
                    case 5:
                        if (c == '+' || c == '-')                            
                        {
                            estado = 6;
                        }
                        else if (char.IsDigit(c))
                        {
                            estado = 7;
                        }
                        else
                        {
                            throw new Exception("Error lexico: Se espera un digito");
                        }
                        break;
                    case 6:
                        if (char.IsDigit(c))
                        {
                            estado = 7;
                        }
                        else
                        {
                            throw new Exception("Error lexico: Se espera un digito");
                        }
                        break;
                    case 7:
                        if (!char.IsDigit(c))
                        {
                            estado = F;
                        }
                        break;
                    case 29:
                        setClasificacion(clasificaciones.caracter);
                        estado = F;
                        break;
                    

                }

                if (estado >= 0)
                {
                    archivo.Read();

                    if (estado > 0)
                    {
                        palabra += c;
                    }
                }                
            }

            setContenido(palabra);
            switch(palabra)
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

        public bool FinDeArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}

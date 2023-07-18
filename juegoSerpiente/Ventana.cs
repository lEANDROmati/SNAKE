using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace juegoSerpiente
{
    internal class Ventana
    {
        public string Titulo { get; set; } //Atributos
        public int Ancho { get; set; }
        public int Altura { get; set; }
        public ConsoleColor ColorFondo { get; set; }
        public ConsoleColor ColorLetra { get;set; }
        public Point LimiteSuperior { get; set; }
        public Point LimiteInferior { get; set; }
        public int Area { get; set; }   


        public  Ventana(string titulo , int ancho , int altura , 
            ConsoleColor colorfondo, ConsoleColor colorLetra,
            Point limiteSuperior, Point limiteInferior) //constructor
        {
            Titulo = titulo;
            Ancho = ancho;  
            Altura = altura;    
            ColorFondo = colorfondo;    
            ColorLetra = colorLetra;
            LimiteSuperior = limiteSuperior;
            LimiteInferior = limiteInferior;
            Area = ((limiteInferior.X - limiteSuperior.X)-1)*((limiteInferior.Y - limiteSuperior.Y) -1 );
            Init();
             
        }
        public void Init()
        {
            Console.SetWindowSize(Ancho, Altura);
            Console.Title = Titulo;
            Console.CursorVisible = false;
            Console.BackgroundColor = ColorFondo;
            Console.Clear();
            
        }
        public void DibujarMarco()
        {
            Console.ForegroundColor = ColorLetra;

            for(int i = LimiteSuperior.X; i < LimiteInferior.X; i++)
            {
                Console.SetCursorPosition(i, LimiteSuperior.Y);
                Console.Write("═");//alt 205
                Console.SetCursorPosition(i,LimiteInferior.Y);
                Console.Write('═');
            }

            for(int i = LimiteSuperior.Y; i < LimiteInferior.Y; i++)
            {
                Console.SetCursorPosition( LimiteSuperior.X, i);
                Console.Write("║");//alt 186
                Console.SetCursorPosition(LimiteInferior.X, i);
                Console.Write("║");
            }
            Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
            Console.Write("╔");// alt 201 200 187 188
            Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
            Console.Write("╚");
            Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
            Console.Write("╗");
            Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
            Console.Write("╝");
        }
    }
}

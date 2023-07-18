using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace juegoSerpiente
{
    internal class Snake
    {
        enum Direccion
        {
            Arriba, Abajo, Derecha, Izquierda
        }
        public bool Vivo { get; set; }
        public ConsoleColor ColorCabeza { get; set; }
        public ConsoleColor ColorCuerpo { get; set; }
        public Ventana VentanaC { get; set; }
        public List<Point> Cuerpo { get; set; }
        public Point Cabeza { get; set; }

        public Comida ComidaC { get; set; }

        private Direccion _direccion;
        private bool _comiendo;
        public Snake(Point posicion, ConsoleColor colorCabeza, ConsoleColor colorCuerpo, Ventana ventana, Comida comidaC)
        {
            ColorCabeza = colorCabeza;
            ColorCuerpo = colorCuerpo;
            VentanaC = ventana;
            Cabeza = posicion;
            Cuerpo = new List<Point>();

            _direccion = Direccion.Derecha;
            ComidaC = comidaC;
        }
        public void IniciarCuerpo(int numPart)
        {
            int x = Cabeza.X - 1;

            for (int i = 0; i < numPart; i++)
            {
                Console.SetCursorPosition(x, Cabeza.Y);
                Console.WriteLine("▓");//alt 178
                Cuerpo.Add(new Point(x, Cabeza.Y));
                x--;

            }
        }

        public void Mover()
        {
            Teclado();
            Point posAnteriorCabeza = Cabeza;
            MoverCabeza();
            MoverCuerpo(posAnteriorCabeza);
            ColisionesComida();
            if (ColicionesCuerpo())
            {
                Muerte();
                Environment.Exit(0);
            }
        }
        public void MoverCabeza()
        {
            Console.ForegroundColor = ColorCabeza;
            Console.SetCursorPosition(Cabeza.X, Cabeza.Y);
            Console.WriteLine(" ");
            switch (_direccion)
            {
                case Direccion.Derecha:
                    Cabeza = new Point(Cabeza.X + 1, Cabeza.Y);
                    break;
                case Direccion.Izquierda:
                    Cabeza = new Point(Cabeza.X - 1, Cabeza.Y);
                    break;
                case Direccion.Abajo:
                    Cabeza = new Point(Cabeza.X, Cabeza.Y + 1);
                    break;
                case Direccion.Arriba:
                    Cabeza = new Point(Cabeza.X, Cabeza.Y - 1);
                    break;
            }
            ColisionesMarco();
            Console.SetCursorPosition(Cabeza.X, Cabeza.Y);
            Console.WriteLine("█"); //alt 219

        }
        private void MoverCuerpo(Point posAnteriorCabeza)
        {
            Console.ForegroundColor = ColorCuerpo;
            Console.SetCursorPosition(posAnteriorCabeza.X, posAnteriorCabeza.Y);
            Console.WriteLine("▓"); // alt 178
            Cuerpo.Insert(0, posAnteriorCabeza);

            if (_comiendo)
            {
                _comiendo = false;
                return;
            }

            Console.SetCursorPosition(Cuerpo[Cuerpo.Count - 1].X, Cuerpo[Cuerpo.Count - 1].Y);
            Console.WriteLine(" ");
            Cuerpo.Remove(Cuerpo[Cuerpo.Count - 1]);
        }
        private void ColisionesComida()
        {
            if (Cabeza == ComidaC.Posicion)
            {
                if (!ComidaC.GenerarComida(this))
                {
                    Vivo = false;
                    Environment.Exit(0);
                }
                _comiendo = true;
            }
        }

        private void Teclado()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.RightArrow && (_direccion != Direccion.Izquierda))
                    _direccion = Direccion.Derecha;
                if (tecla.Key == ConsoleKey.LeftArrow && (_direccion != Direccion.Derecha))
                    _direccion = Direccion.Izquierda;
                if (tecla.Key == ConsoleKey.UpArrow && (_direccion != Direccion.Abajo))
                    _direccion = Direccion.Arriba;
                if (tecla.Key == ConsoleKey.DownArrow && (_direccion != Direccion.Arriba))
                    _direccion = Direccion.Abajo;

            }
        }

        private void ColisionesMarco()
        {
            if (Cabeza.X <= VentanaC.LimiteSuperior.X)
                Cabeza = new Point(VentanaC.LimiteInferior.X - 1, Cabeza.Y);
            if (Cabeza.X >= VentanaC.LimiteInferior.X)
                Cabeza = new Point(VentanaC.LimiteSuperior.X + 1, Cabeza.Y);
            if (Cabeza.Y <= VentanaC.LimiteSuperior.Y)
                Cabeza = new Point(Cabeza.X, VentanaC.LimiteInferior.Y - 1);
            if (Cabeza.Y >= VentanaC.LimiteInferior.Y)
                Cabeza = new Point(Cabeza.X, VentanaC.LimiteSuperior.Y + 1);
        }

        private bool ColicionesCuerpo()
        {
            foreach(Point item in Cuerpo)
            {
                if(Cabeza == item)
                {
                    Vivo = false;
                    return true;
                }
            }
            return false;
        }

        public void Muerte()
        {
            Console.ForegroundColor = ColorCuerpo;
            foreach (Point item in Cuerpo)
            {
                if (Cabeza == item)
                    continue;
                Console.SetCursorPosition(item.X, item.Y);
                Console.WriteLine("░");
                Thread.Sleep(120);
            }
        }

    }
}

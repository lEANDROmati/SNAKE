
using juegoSerpiente;
using System.Drawing;

Ventana ventana;
Snake snake;
Comida comida;
bool jugar = true;

void Iniciar()
{
    ventana = new Ventana("Snake", 65, 20, ConsoleColor.Black, ConsoleColor.White,
    new Point(5, 3), new Point(59, 18));
    ventana.DibujarMarco();
    comida = new Comida(ConsoleColor.DarkYellow, ventana);
    snake = new Snake(new Point(8, 4), ConsoleColor.Blue, ConsoleColor.Cyan, ventana, comida);
    snake.IniciarCuerpo(2);

    comida.GenerarComida(snake);

}

void Game()
{
    while (jugar)
    {
        snake.Mover();
        Thread.Sleep(100);
    }
}

Iniciar();
Game();
 
Console.ReadKey();
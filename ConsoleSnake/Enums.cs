using System;

namespace ConsoleSnake
{
    public enum Tiles
    {
        Null = ConsoleColor.Black,
        Grass = ConsoleColor.Green,
        GrassDark = ConsoleColor.DarkGreen,
        Snake = ConsoleColor.Blue,
        SnakeHead = ConsoleColor.DarkBlue,
        Apple = ConsoleColor.Red
    }

    public enum MoveStatus
    {
        Success,
        Fail,
        Apple
    }
}

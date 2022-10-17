using System;
using static ConsoleSnake.Core;

namespace ConsoleSnake
{
    public class Game
    {
        public Grid grid;
        public Grid origGrid;

        public const int width = 15;

        public Player player;

        public int highScore = 0;

        public void Start()
        {
            grid = new Grid(width, width);
            CreateGrid(grid);

            origGrid = new Grid(width, width);
            CreateGrid(origGrid);

            player = new Player(new Vector(width / 2, width / 2), 3, Vector.right, grid, origGrid);

            SpawnApple();
        }

        public void Update()
        {
            if (player.isAlive)
            {
                grid.PrintGrid();
                HandleInput();
                var mstatus = player.Move(player.direction);
                if (mstatus == MoveStatus.Apple) { player.length++; SpawnApple(); }
                if (mstatus == MoveStatus.Fail) player.Kill();
            }
            else
            {
                highScore = Math.Max(highScore, player.length - 3);

                Print(grid.height, "Game Over");
                Print(grid.height + 1, "Your score is", (player.length - 3), (highScore == player.length - 3 ? "(New high score!)" : ""));
                Print(grid.height + 2, "Your high score is", highScore);
                Print(grid.height + 3, "Press Enter to restart!");
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Enter) NewGame();
            }
        }


        void HandleInput()
        {
            Console.SetCursorPosition(width + 1, width + 1);
            Console.ForegroundColor = ConsoleColor.Black;
            if (!Console.KeyAvailable) return;
            var newDir = ChooseDirection(Console.ReadKey().Key);
            player.direction = (newDir != Vector.zero && newDir * -1 != player.direction) ? newDir : player.direction;
        }

        Vector ChooseDirection(ConsoleKey key)
        {
            switch (key)
            {
                case (ConsoleKey.W): return Vector.down;
                case (ConsoleKey.A): return Vector.left;
                case (ConsoleKey.S): return Vector.up;
                case (ConsoleKey.D): return Vector.right;
            }
            return Vector.zero;
        }


        void CreateGrid(Grid g)
        {
            bool alt = false;
            foreach (var v in g.AllPositionsWithin())
            {
                g.SetTile(v, alt ? Tiles.Grass : Tiles.GrassDark);
                alt = !alt;
            }
        }

        void SpawnApple() => grid.SetTile(GetRandomPosition(), Tiles.Apple);

        Vector GetRandomPosition()
        {
            var rnd = new Random();
            var v = new Vector(rnd.Next(0, grid.width), rnd.Next(0, grid.height));
            return grid.GetTile(v) == Tiles.Snake || grid.GetTile(v) == Tiles.SnakeHead ? GetRandomPosition() : v;
        }
    }
}

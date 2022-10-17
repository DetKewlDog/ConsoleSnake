using System.Collections.Generic;

namespace ConsoleSnake
{
    public class Player
    {
        public Vector headPos;
        public int length;
        public List<Vector> positions;
        public Vector direction;

        public Grid grid;
        public Grid origGrid;

        public bool isAlive;

        public Player(Vector pos, int length, Vector direction, Grid grid, Grid origGrid)
        {
            this.headPos = pos;
            this.length = length;
            this.positions = new List<Vector>();
            this.positions.Add(pos);
            this.direction = direction;

            this.grid = grid;
            this.origGrid = origGrid;

            this.isAlive = true;

            grid.SetTile(pos, Tiles.Snake);
        }

        public MoveStatus Move(Vector v)
        {
            MoveStatus r = MoveStatus.Fail;
            var lastPos = headPos;
            v += headPos;
            if (!grid.PosInRange(v) || grid.GetTile(v) == Tiles.Snake || grid.GetTile(v) == Tiles.SnakeHead) return r;
            if (grid.GetTile(v) == Tiles.Apple) r = MoveStatus.Apple;
            grid.SetTile(v, Tiles.SnakeHead);
            grid.SetTile(lastPos, Tiles.Snake);
            positions.Add(v);
            headPos = v;
            if (positions.Count > length)
            {
                grid.SetTile(positions[0], origGrid.GetTile(positions[0]));
                positions.RemoveAt(0);
            }
            r = r != MoveStatus.Apple ? MoveStatus.Success : MoveStatus.Apple;
            return r;
        }

        public void Kill() => this.isAlive = false;
    }
}

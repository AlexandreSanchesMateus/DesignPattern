using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TU_Challenge
{
    public class Pathfinding
    {
        private string map1;
        public char[,] Grid { get; internal set; }

        public Pathfinding(string map1)
        {
            this.map1 = map1;
        }

        public bool IsOutOfBound(Vector2 vector2)
        {
            throw new NotImplementedException();
        }

        public List<Path> GetNeighboors(Vector2 vector2)
        {
            throw new NotImplementedException();
        }

        public List<Path> GetNeighboors(Vector2 vector2, List<Vector2> exclude)
        {
            throw new NotImplementedException();
        }
        public Path BreadthFirstSearch(Vector2 start, Vector2 destination)
        {
            throw new NotImplementedException();
        }

        public char GetCoord(Vector2 el)
        {
            throw new NotImplementedException();
        }
    }

}

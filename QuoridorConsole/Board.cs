using System.Numerics;

namespace QuoridorConsole
{
    class Board
    {
        public int Size { get; }

        public Graph Graph { get; }

        public Board()
        {
            Size = 9;

            Graph = new Graph();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Graph.AddVertex(new Vector2(i, j));
                }
            }

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Graph.AddEdge(new Vector2(i, j), new Vector2(i, j - 1), 1);
                    Graph.AddEdge(new Vector2(i, j), new Vector2(i + 1, j), 1);
                    Graph.AddEdge(new Vector2(i, j), new Vector2(i, j + 1), 1);
                    Graph.AddEdge(new Vector2(i, j), new Vector2(i - 1, j), 1);
                }
            }
        }

        public bool HasPathBetweenPointAndLine(Vector2 point1, int line)
        {
            Dijkstra dijkstra = new Dijkstra(Graph);

            for (int i = 0; i < Size; i++)
            {
                if (dijkstra.HasPath(point1, new Vector2(i, line)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
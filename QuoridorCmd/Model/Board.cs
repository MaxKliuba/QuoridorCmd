namespace QuoridorCmd.Model
{
    class Board
    {
        public const int SIZE = 9;

        public Graph Graph { get; }

        public Board()
        {

            Graph = new Graph();

            for (int i = 1; i <= SIZE; i++)
            {
                for (int j = 1; j <= SIZE; j++)
                {
                    Graph.AddVertex(new Position(i, j));
                }
            }

            for (int i = 1; i <= SIZE; i++)
            {
                for (int j = 1; j <= SIZE; j++)
                {
                    Graph.AddEdge(new Position(i, j), new Position(i, j - 1), 1);
                    Graph.AddEdge(new Position(i, j), new Position(i + 1, j), 1);
                    Graph.AddEdge(new Position(i, j), new Position(i, j + 1), 1);
                    Graph.AddEdge(new Position(i, j), new Position(i - 1, j), 1);
                }
            }
        }

        public bool HasPathBetweenPositionAndLine(Position position, int line)
        {
            Dijkstra dijkstra = new Dijkstra(Graph);

            for (int i = 1; i <= SIZE; i++)
            {
                if (dijkstra.HasPath(position, new Position(i, line)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
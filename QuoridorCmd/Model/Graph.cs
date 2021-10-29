using System.Collections.Generic;

namespace QuoridorCmd.Model
{
    class Graph
    {
        public List<GraphVertex> Vertices { get; }

        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }

        public void AddVertex(Position position)
        {
            Vertices.Add(new GraphVertex(position));
        }

        public GraphVertex FindVertex(Position position)
        {
            foreach (var v in Vertices)
            {
                if (v.Position.Equals(position))
                {
                    return v;
                }
            }

            return null;
        }

        public void AddEdge(Position position1, Position position2, int weight)
        {
            var v1 = FindVertex(position1);
            var v2 = FindVertex(position2);

            if (v2 != null && v1 != null)
            {
                var e1 = v1.FindEdge(position2);
                var e2 = v2.FindEdge(position1);

                if (e2 == null && e1 == null)
                {
                    v1.AddEdge(v2, weight);
                    v2.AddEdge(v1, weight);
                }
            }
        }

        public void RemoveEdge(Position position1, Position position2)
        {
            var v1 = FindVertex(position1);
            var v2 = FindVertex(position2);

            if (v2 != null && v1 != null)
            {
                var e1 = v1.FindEdge(position2);
                var e2 = v2.FindEdge(position1);

                if (e2 != null && e1 != null)
                {
                    v1.RemoveEdge(e1);
                    v2.RemoveEdge(e2);
                }
            }
        }
    }
}

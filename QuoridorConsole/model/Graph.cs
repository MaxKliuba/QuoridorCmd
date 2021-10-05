using System.Collections.Generic;
using System.Numerics;

namespace QuoridorConsole
{
    public class Graph
    {
        public List<GraphVertex> Vertices { get; }

        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }

        public void AddVertex(Vector2 point)
        {
            Vertices.Add(new GraphVertex(point));
        }

        public GraphVertex FindVertex(Vector2 point)
        {
            foreach (var v in Vertices)
            {
                if (v.Point.Equals(point))
                {
                    return v;
                }
            }

            return null;
        }

        public void AddEdge(Vector2 point1, Vector2 point2, int weight)
        {
            var v1 = FindVertex(point1);
            var v2 = FindVertex(point2);

            if (v2 != null && v1 != null)
            {
                var e1 = v1.FindEdge(point2);
                var e2 = v2.FindEdge(point1);

                if (e2 == null && e1 == null)
                {
                    v1.AddEdge(v2, weight);
                    v2.AddEdge(v1, weight);
                }
            }
        }

        public void RemoveEdge(Vector2 point1, Vector2 point2)
        {
            var v1 = FindVertex(point1);
            var v2 = FindVertex(point2);

            if (v2 != null && v1 != null)
            {
                var e1 = v1.FindEdge(point2);
                var e2 = v2.FindEdge(point1);

                if (e2 != null && e1 != null)
                {
                    v1.RemoveEdge(e1);
                    v2.RemoveEdge(e2);
                }
            }
        }
    }
}

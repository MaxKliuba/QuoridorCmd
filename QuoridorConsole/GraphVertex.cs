using System.Collections.Generic;
using System.Numerics;

namespace QuoridorConsole
{
    public class GraphVertex
    {
        public Vector2 Point { get; }

        public int Y { get; }

        public List<GraphEdge> Edges { get; }

        public GraphVertex(Vector2 point)
        {
            Point = point;
            Edges = new List<GraphEdge>();
        }

        public void AddEdge(GraphEdge edge)
        {
            Edges.Add(edge);
        }

        public void AddEdge(GraphVertex vertex, int edgeWeight)
        {
            AddEdge(new GraphEdge(vertex, edgeWeight));
        }

        public GraphEdge FindEdge(Vector2 point)
        {
            foreach (var e in Edges)
            {
                if (e.ConnectedVertex.Point.Equals(point))
                {
                    return e;
                }
            }

            return null;
        }

        public void RemoveEdge(GraphEdge edge)
        {
            Edges.Remove(edge);
        }

        public override string ToString()
        {
            return $"[{Point.X};{Point.Y}], {Edges.Count} edges";
        }
    }
}

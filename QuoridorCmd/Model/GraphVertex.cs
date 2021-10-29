using System.Collections.Generic;

namespace QuoridorCmd.Model
{
    class GraphVertex
    {
        public Position Position { get; }

        public List<GraphEdge> Edges { get; }

        public GraphVertex(Position position)
        {
            Position = position;
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

        public GraphEdge FindEdge(Position position)
        {
            foreach (var e in Edges)
            {
                if (e.ConnectedVertex.Position.Equals(position))
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
            return $"{Position.Code}, {Edges.Count} edges";
        }
    }
}

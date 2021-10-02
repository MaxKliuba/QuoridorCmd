﻿namespace QuoridorConsole
{
    public class GraphEdge
    {
        public GraphVertex ConnectedVertex { get; }

        public int EdgeWeight { get; }

        public GraphEdge(GraphVertex connectedVertex, int weight)
        {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
        }

        public override string ToString()
        {
            return $"[{ConnectedVertex.Point.X};{ConnectedVertex.Point.Y}], Weight: {EdgeWeight}";
        }
    }
}

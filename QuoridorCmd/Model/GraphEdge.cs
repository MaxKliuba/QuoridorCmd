namespace QuoridorCmd.Model
{
    class GraphEdge
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
            return $"{ConnectedVertex.Position.Code}, Weight: {EdgeWeight}";
        }
    }
}

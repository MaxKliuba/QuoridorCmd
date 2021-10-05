using System.Collections.Generic;
using System.Numerics;

namespace QuoridorConsole
{
    class Dijkstra
    {
        private Graph graph;

        private List<GraphVertexInfo> infos;

        public Dijkstra(Graph graph)
        {
            this.graph = graph;
        }

        private void InitInfo()
        {
            infos = new List<GraphVertexInfo>();

            foreach (var v in graph.Vertices)
            {
                infos.Add(new GraphVertexInfo(v));
            }
        }

        private GraphVertexInfo GetVertexInfo(GraphVertex v)
        {
            foreach (var i in infos)
            {
                if (i.Vertex.Equals(v))
                {
                    return i;
                }
            }

            return null;
        }

        public GraphVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertexInfo minVertexInfo = null;

            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }

        public bool HasPath(Vector2 fromPoint, Vector2 toPoint)
        {
            return GetShortestPathLength(fromPoint, toPoint) > 0;
        }

        public List<Vector2> FindShortestPath(Vector2 fromPoint, Vector2 toPoint)
        {
            return FindShortestPath(graph.FindVertex(fromPoint), graph.FindVertex(toPoint));
        }

        public int GetShortestPathLength(Vector2 fromPoint, Vector2 toPoint)
        {
            var shortestPath = FindShortestPath(graph.FindVertex(fromPoint), graph.FindVertex(toPoint));

            return shortestPath != null ? shortestPath.Count : -1;
        }

        public List<Vector2> FindShortestPath(GraphVertex fromVertex, GraphVertex toVertex)
        {
            if (fromVertex == null || toVertex == null)
            {
                return null;
            }

            InitInfo();

            var first = GetVertexInfo(fromVertex);
            first.EdgesWeightSum = 0;

            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();

                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(fromVertex, toVertex);
        }

        private void SetSumToNextVertex(GraphVertexInfo info)
        {
            info.IsUnvisited = false;

            foreach (var e in info.Vertex.Edges)
            {
                var nextInfo = GetVertexInfo(e.ConnectedVertex);
                var sum = info.EdgesWeightSum + e.EdgeWeight;

                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }

        private List<Vector2> GetPath(GraphVertex fromVertex, GraphVertex toVertex)
        {
            var path = new List<Vector2>();

            path.Add(toVertex.Point);

            while (fromVertex != toVertex)
            {
                toVertex = GetVertexInfo(toVertex).PreviousVertex;

                if (toVertex == null)
                {
                    return null;
                }

                path.Add(toVertex.Point);
            }

            return path;
        }
    }
}

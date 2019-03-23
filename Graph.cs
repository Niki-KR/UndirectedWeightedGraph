using System.Collections.Generic;
using System.Text;
using Priority_Queue;

namespace UndirectedWeightedGraph {
    class Graph<T> {
        public List<Vertex<T>> Vertices { get; } = new List<Vertex<T>>();
        public List<Edge<T>> Edges { get; } = new List<Edge<T>>();

        public Graph(List<Vertex<T>> v, List<Edge<T>> e) {
            Vertices = v;
            Edges = e;
        }

        public void AddEdge(Edge<T> edge) {
            Edges.Add(edge);
        }

        public void AddVertex(Vertex<T> vertex) {
            Vertices.Add(vertex);
        }

        public override string ToString() {
            StringBuilder s = new StringBuilder();
            foreach (Vertex<T> vertex in Vertices) {
                s.AppendLine(vertex.ToString());
            }
            return s.ToString();
        }
        /// <summary>
        /// "Очистка" вершин графа для повторного поиска кратчайшего пути
        /// </summary>
        public void UnvisitAll() {
            foreach (Vertex<T> v in Vertices) {
                v.IsVisited = false;
            }
        }
        /// <summary>
        /// Алгоритм Дейкстры поиска кратчайшего пути в взвешенном неоринетированном графе
        /// </summary>
        /// <param Начальная вершина="start"></param>
        /// <param Конечная вершина="end"></param>
        /// <returns></returns>
        public List<Vertex<T>> Dijkstra(Vertex<T> start, Vertex<T> end) {
           
            UnvisitAll();

            // Ключ - целевая вершина, значение - вершина, через которую лежит кратчайший путь к целевой вершине
            Dictionary<Vertex<T>, Vertex<T>> Map = new Dictionary<Vertex<T>, Vertex<T>>();

            // Очередь с приоритетом, позволяющая итерировать по вершинам в порядке, зависящем от суммарного расстояния до них
            SimplePriorityQueue<Vertex<T>> Queue = new SimplePriorityQueue<Vertex<T>>();

            InitializeCosts(start);

            Queue.Enqueue(start, start.Cost);
            Vertex<T> Current;
            //Итерируем по вершинам в очереди с приоритетом
            while (Queue.Count > 0) {
                Current = Queue.Dequeue();
                if (!Current.IsVisited) {
                    Current.IsVisited = true;
                    // Завершение работы с очередью по приходу к искомой конечной вершине
                    if (Current.Equals(end)) {
                        break;
                    } 
                    // Итерируем по всем рёбрам итерируемой вершины
                    foreach (Edge<T> Edge in Current.Edges) {
                        Vertex<T> Neighbor = Edge.End;
                        int NewCost = Current.Cost + Edge.Weight;
                        int NeighborCost = Neighbor.Cost;
                        // Сравнение новой суммарной дистанции до соседней вершины с известной суммарной дистанцией до неё же
                        if (NewCost < NeighborCost) {
                            // При получении меньшей дистанции, заменяем старую дистанцию новой
                            Neighbor.Cost = NewCost;
                            // Записываем пару в словарь
                            // К соседней вершине быстрее всего можно добраться через итерируемую вершину
                            Map[Neighbor] = Current;
                            // Добавляем эту вершину в очередь с соответствующим приоритетом
                            Queue.Enqueue(Neighbor, NewCost);
                        }
                    }
                }
            }
            List<Vertex<T>> path = ReconstructPath(Map, start, end);
            return path;
        }

        // Делаем максимально большими начальные расстояния до вершин,
        // т.к. изначально мы не знаем фактических значений
        public void InitializeCosts(Vertex<T> start) {
            foreach (Vertex<T> vertex in Vertices) {
                vertex.Cost = int.MaxValue;
            }
            start.Cost = 0;
        }

        public List<Vertex<T>> ReconstructPath(Dictionary<Vertex<T>, Vertex<T>> map, Vertex<T> start, Vertex<T> end) {
            List<Vertex<T>> path = new List<Vertex<T>>();
            // Итерируем по словарю, начиная с конечной вершины
            Vertex<T> current = end;

            // В результате составления словаря в нём никогда
            // не будет содержаться ключ, являющийся стартовой вершиной,
            // а любая другая вершина будет связана ключом с предшествующей ей вершиной
            while (map.ContainsKey(current)) {
                // Добавляем текущую с конца вершину в путь
                path.Add(current);
                // Ищем в словаре новую вершину по соответствующему ключу
                current = map[current];
            }

            // Добавляем начальную вершину в путь
            path.Add(start);
            // И переворачиваем список в нужный нам порядок
            path.Reverse();

            return path;
        }
        
    }
}

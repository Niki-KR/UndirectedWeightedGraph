using System;
using System.Collections.Generic;
using System.Text;

namespace UndirectedWeightedGraph {
    class Vertex<T>: IEquatable<Vertex<T>> {
        /// <summary>
        /// Список соседних вершин. Наверняка можно обойтись и без него, интерполируя соседние вершины из соответствующих ребёр.
        /// </summary>
        public List<Vertex<T>> Neighbors { get; } = new List<Vertex<T>>();
        /// <summary>
        /// Список примыкающих ребёр
        /// </summary>
        public List<Edge<T>> Edges { get; } = new List<Edge<T>>();
        public T Value { get; set; }
        public bool IsVisited { get; set; }
        public int Cost { get; set; }

        public Vertex(T v) {
            IsVisited = false;
            Value = v;
        }
        /// <summary>
        /// Реализация интерфейса IEquatable
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool Equals (Vertex<T> v) {
            if (v == null) {
                return false;
            }
            if (Value.ToString() == v.Value.ToString()) {
                return true;
            }
            else {
                return false;
            }
        }

        public void AddEdge(Edge<T> edge) {
            Edges.Add(edge);
        }

        public void AddNeighbor(Vertex<T> neighbor) {
            Neighbors.Add(neighbor);
        }

        public override string ToString() {
            StringBuilder s = new StringBuilder();
            s.Append(Value);
            return s.ToString();
        }
    }
}

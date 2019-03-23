using System;
using System.Text;

namespace UndirectedWeightedGraph {
    class Edge<T> : IEquatable<Edge<T>> {
        
        public int Weight { get; }
        public Vertex<T> Start { get; }
        public Vertex<T> End { get; }

        public Edge(Vertex<T> s, Vertex<T> e, int w) {
            Start = s;
            End = e;
            Weight = w;
        }

        /// <summary>
        /// Возвращает ребро, равное по весу и противоположное по направлению
        /// </summary>
        /// <returns></returns>
        public Edge<T> Reversed() {
            return new Edge<T>(End, Start, Weight);
        }

        /// <summary>
        /// Реализация интерфейса IEquatable
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Equals(Edge<T> e) {
            if (e == null) {
                return false;
            }
            if (Start == e.Start && End == e.End && Weight == e.Weight) {
                return true;
            }
            else {
                return false;
            }
        }

        public override string ToString() {
            StringBuilder s = new StringBuilder();
            s.Append(Start);
            s.Append("-");
            s.Append(End);
            s.Append(":");
            s.AppendLine(Weight.ToString());
            return s.ToString();
        }
    }
}

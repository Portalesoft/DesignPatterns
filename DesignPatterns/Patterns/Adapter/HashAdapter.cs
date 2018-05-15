/*
 * Example using hashing to ensure we only perform a single conversion on the same object
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DesignPatterns.Patterns.Adapter {

    public class Point {

        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override bool Equals(object obj) {
            if (obj is null) return false;
            return ReferenceEquals(this, obj) && obj.GetType() == GetType() && Equals((Point) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString() {
            return $"({X}, {Y})";
        }

        protected bool Equals(Point other) {
            return X == other.X && Y == other.Y;
        }
    }

    public class Line {

        public Line(Point start, Point end) {
            Start = start;
            End = end;
        }

        public Point Start { get; }
        public Point End { get; }

        protected bool Equals(Line other) {
            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object obj) {
            if (obj is null) return false;
            return ReferenceEquals(this, obj) && obj.GetType() == GetType() && Equals((Line) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((Start != null ? Start.GetHashCode() : 0) * 397) ^ (End != null ? End.GetHashCode() : 0);
            }
        }
    }

    public abstract class VectorObject : Collection<Line> { }

    public class VectorRectangle : VectorObject {
        public VectorRectangle(int x, int y, int width, int height) {
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }

    public class LineToPointAdapter : IEnumerable<Point> {
        private static readonly Dictionary<int, List<Point>> Cache = new Dictionary<int, List<Point>>();
        private readonly int _hash;

        public LineToPointAdapter(Line line) {
            _hash = line.GetHashCode();
            if (Cache.ContainsKey(_hash)) return;

            var points = new List<Point>();
            var left = Math.Min(line.Start.X, line.End.X);
            var right = Math.Max(line.Start.X, line.End.X);
            var top = Math.Min(line.Start.Y, line.End.Y);
            var bottom = Math.Max(line.Start.Y, line.End.Y);
            var dx = right - left;
            var dy = line.End.Y - line.Start.Y;

            if (dx == 0)
                for (var y = top; y <= bottom; ++y)
                    points.Add(new Point(left, y));
            else if (dy == 0)
                for (var x = left; x <= right; ++x)
                    points.Add(new Point(x, top));

            Cache.Add(_hash, points);
        }

        public IEnumerator<Point> GetEnumerator() {
            return Cache[_hash].GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

}
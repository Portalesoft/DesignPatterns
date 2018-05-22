using System;
using System.Collections.Generic;

namespace DesignPatterns.SOLID.Solutions {

    public enum Color {
        Red, Green, Blue
    }

    public enum Size {
        Small, Medium, Large, Yuge
    }

    public class Product {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size) {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Color = color;
            Size = size;
        }
    }

    public interface ISpecification<in T> {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T> {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product> {
        private readonly Color _color;

        public ColorSpecification(Color color) {
            _color = color;
        }

        public bool IsSatisfied(Product p) {
            return p.Color == _color;
        }
    }

    public class SizeSpecification : ISpecification<Product> {
        private readonly Size _size;

        public SizeSpecification(Size size) {
            _size = size;
        }

        public bool IsSatisfied(Product p) {
            return p.Size == _size;
        }
    }

    public class AndSpecification<T> : ISpecification<T> {
        private readonly ISpecification<T> _first;
        private readonly ISpecification<T> _second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second) {
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public bool IsSatisfied(T t) {
            return _first.IsSatisfied(t) && _second.IsSatisfied(t);
        }

    }

    public class BetterFilter : IFilter<Product> {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec) {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }


}
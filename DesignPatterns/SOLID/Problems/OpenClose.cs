using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.SOLID.Problems {

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

    /// <summary>
    /// OCP = open for extension but closed for modification
    /// If we extend the combinations 3 criteria would require 7 methods
    /// </summary>
    public class ProductFilter {

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color) {
            return products.Where(p => p.Color == color);
        }

        public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size) {
            return products.Where(p => p.Size == size);
        }

        public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color) {
            return products.Where(p => p.Size == size && p.Color == color);
        } 

    }

}
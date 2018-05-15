/*
 * Getting the interface you want from the interface you have e.g.: same as with electrical devices, we need
 * a physical adapter in different countries, the mechanism is the same.
 * 
 * It's a construct which adapts an existing interface X to conform to the required interface Y. 
 * 
 */

namespace DesignPatterns.Patterns.Adapter {

    public class Square {
        public Square(int side) {
            Side = side;
        }
        public int Side { get; }
    }

    public interface IRectangle {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods {
        public static int Area(this IRectangle rc) {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle {
        public SquareToRectangleAdapter(Square square) {
            Width = square.Side;
        }

        public int Width { get; }
        public int Height => Width;
    }

}
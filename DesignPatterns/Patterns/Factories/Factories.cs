/*
 * A component responsible solely for the wholesale (not piecewise) creation of objects.
 * 
 * Object creation logic can become too convoluted and the constructor is not descriptive
 * as the name is mandated by the name of the containing type. It's also not possible to overload
 * the constructor with the same argument types.
 * 
 * With factories object creation can be outsourced (not piecewise like Builder):
 * 
 *      Separate function (Factory Method)
 *      Separate Class (Factory)
 *      Hierarchy of factories (Abstract Factory)
 * 
 */

using System;

namespace DesignPatterns.Patterns.Factories {

    public class Point {

        private readonly double _x, _y;
        private Point(double x, double y) {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Factory Method Implementation for Cartesian Points
        /// </summary>
        public static Point NewCartesianPoint(double x, double y) {
            return new Point(x, y);
        }

        /// <summary>
        /// Factory Method Implementation for Polar Points
        /// </summary>
        public static Point NewPolarPoint(double rho, double theta) {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        public override string ToString() {
            return $"{nameof(_x)}: {_x}, {nameof(_y)}: {_y}";
        }

        /// <summary>
        /// Inner Factory Class Implementation (Single Responsibility)
        /// </summary>
        public static class Factory {

            public static Point NewInnerCartesianPoint(double x, double y) {
                return new Point(x, y);
            }

            public static Point NewInnerPolarPoint(double rho, double theta) {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }

        }

    }

}
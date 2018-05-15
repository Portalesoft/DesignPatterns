/*
 * Connecting components together through abstractions
 *
 * Bridges prevent a 'Cartesian product' complexity explosion, through inheritance, for multiple scenarios.
 * It's a mechanism that decouples an interface (hierarchy) from an implementation (hierarchy).
 *
 * Decouples abstraction from implementation and is a stronger form of encapsulation.
 * This is also an example of DI through constructor injection
 *
 */

using System;

namespace DesignPatterns.Patterns.Bridge {

    public interface IRenderer {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer {
        public void RenderCircle(float radius) {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer {
        public void RenderCircle(float radius) {
            Console.WriteLine($"Drawing pixels for circle of radius {radius}");
        }
    }

    public abstract class Shape {

        protected IRenderer Renderer;

        // IRenderer is the Bridge between the shape that's being drawn and the component which actually draws it
        // The limitation of rendering the shape in vector form or raster form is not in the class itself
        protected Shape(IRenderer renderer) {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape {
        private float _radius;

        public Circle(IRenderer renderer, float radius) : base(renderer) {
            _radius = radius;
        }

        public override void Draw() {
            Renderer.RenderCircle(_radius);
        }

        public override void Resize(float factor) {
            _radius *= factor;
        }
    }
}
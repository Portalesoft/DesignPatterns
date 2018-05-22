namespace DesignPatterns.SOLID.Solutions {

    public class Document {}

    public interface IPrinter {
        void Print(Document d);
    }

    public interface IScanner {
        void Scan(Document d);
    }

    public class Printer : IPrinter {
        public void Print(Document d) {}
    }

    public class Photocopier : IPrinter, IScanner {
        public void Print(Document d) {}
        public void Scan(Document d) {}
    }

}
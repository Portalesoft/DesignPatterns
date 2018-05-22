namespace DesignPatterns.SOLID.Problems {

    public class Document {}

    public interface IMachine {
        void Print(Document d);
        void Fax(Document d);
        void Scan(Document d);
    }

    // Ok if we support all the methods of the interface ...
    public class MultiFunctionPrinter : IMachine {
        public void Print(Document d) {}
        public void Fax(Document d) { }
        public void Scan(Document d) {}
    }

    // Not so good when we don't ...
    public class OldFashionedPrinter : IMachine {
        public void Print(Document d) {}
        public void Fax(Document d) {
            throw new System.NotImplementedException();
        }
        public void Scan(Document d) {
            throw new System.NotImplementedException();
        }
    }

}
/*
 * Typically a tool for structure traversal rather than anything else.
 * It allows for adding functionality when the class hiearchy is fixed.
 *
 * A pattern where a component (visitor) is allowed to traverse the entire
 * inheritance hierarchy. Implemented by propagating a single visit() method
 * throughout the entire hierarchy.
 *
 * Need to define a new operation on an entire class hierarchy
 *
 *   E.g.: make a document model printable to HTML/Markdown
 *
 * Do not want to keep modifying every class in the hierarchy.
 *
 * Utilises double dispatch: which depends on the name of the request and type
 * of two receivers i.e.: type of visitor and type of element being visited.
 *
 */

using System;
using System.Text;

namespace DesignPatterns.Patterns.Visitor {

    public abstract class Expression {
        public abstract void Accept(IExpressionVisitor visitor);
    }

    public class DoubleExpression : Expression {
        public double Value;

        public DoubleExpression(double value) {
            Value = value;
        }

        public override void Accept(IExpressionVisitor visitor) {
            visitor.Visit(this);
        }
    }

    public class AdditionExpression : Expression {
        public Expression Left;
        public Expression Right;

        public AdditionExpression(Expression left, Expression right) {
            Left = left ?? throw new ArgumentNullException(paramName: nameof(left));
            Right = right ?? throw new ArgumentNullException(paramName: nameof(right));
        }

        public override void Accept(IExpressionVisitor visitor) {
            visitor.Visit(this);
        }
    }

    public interface IExpressionVisitor {
        void Visit(DoubleExpression de);
        void Visit(AdditionExpression ae);
    }

    public class ExpressionPrinter : IExpressionVisitor {

        private readonly StringBuilder _sb = new StringBuilder();

        public void Visit(DoubleExpression de) {
            _sb.Append(de.Value);
        }

        public void Visit(AdditionExpression ae) {
            _sb.Append("(");
            ae.Left.Accept(this);
            _sb.Append("+");
            ae.Right.Accept(this);
            _sb.Append(")");
        }

        public override string ToString() => _sb.ToString();
    }

    public class ExpressionCalculator : IExpressionVisitor {

        public double Result;

        public void Visit(DoubleExpression de) {
            Result = de.Value;
        }

        public void Visit(AdditionExpression ae) {
            ae.Left.Accept(this);
            var a = Result;
            ae.Right.Accept(this);
            var b = Result;
            Result = a + b;
        }

    }

}
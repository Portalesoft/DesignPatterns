/*
 *  Some objects are simple and can be created in a single constructor call,
 *  however others are more complex and having an object with multiple constructor
 *  parameters is not constructive.
 *
 *  Builders can be constructed using a constructor or returned via a static function.
 *
 *  Builder provides an API for constructing an object step by step.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Patterns.Builder {

    /// <summary>
    /// Html Element to use in our builder
    /// </summary>
    public class HtmlElement  {

        private const int IndentSize = 2;
        private readonly string _name, _text;
        private readonly List<HtmlElement> _elements = new List<HtmlElement>();

        public HtmlElement(string name) {
            _name = name ?? throw new ArgumentNullException(paramName: nameof(name));
        }

        public HtmlElement(string name, string text) {
            _name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            _text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        public void AddChild(HtmlElement child) {
            _elements.Add(child);
        }

        public override string ToString() {
            return ToStringImpl(0);
        }

        private string ToStringImpl(int indent) {
            var sb = new StringBuilder();
            var i = new string(' ', IndentSize * indent);

            // Open the html tag
            sb.AppendLine($"{i}<{_name}>");
            if (!string.IsNullOrWhiteSpace(_text)) {
                sb.Append(new string(' ', IndentSize * (indent + 1)));
                sb.AppendLine(_text);
            }

            // Append the children
            foreach (var e in this._elements) {
                sb.Append(e.ToStringImpl(indent + 1));
            }

            // Close the html tag and exit
            sb.AppendLine($"{i}</{_name}>");
            return sb.ToString();
        }
    }

    // Builder class
    public class HtmlBuilder {

        private readonly string _rootName;
        private HtmlElement _root;

        public HtmlBuilder(string rootName) {
            _rootName = rootName;
            _root = new HtmlElement(rootName);
        }

        public HtmlBuilder AddChild(string childName, string childText) {
            var e = new HtmlElement(childName, childText);
            _root.AddChild(e);
            return this;
        }

        public override string ToString() {
            return _root.ToString();
        }

        public void Clear() {
            _root = new HtmlElement(_rootName);
        }
    }
}
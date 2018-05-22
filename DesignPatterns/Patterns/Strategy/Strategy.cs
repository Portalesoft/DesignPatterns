/*
 * System behaviour partially defined at runtime. Enables the exact behaviour of a system to be selected
 * either at runtime (dynamic) or compile time (static). Also known as policy (C++)
 *
 * Many algorithms can be decomposed into higher and lower level parts e.g.: making tea
 *
 *   Process of making a hot drink (boil water, pour into cup)
 *   Tea specific things (teabag in water, add milk, lemon)
 *
 * The high level algorithm can then be resused for making coffee or hot chocolate
 *
 *   Supported by beverage-specific strategies
 *
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Patterns.Strategy {

    public enum OutputFormat {
        Markdown,
        Html
    }

    public interface IListStrategy {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);
    }

    public class MarkdownListStrategy : IListStrategy {

        /// <summary>
        /// Markdown doesn't require a list preamble
        /// </summary>
        /// <param name="sb"></param>
        public void Start(StringBuilder sb) {}
        public void End(StringBuilder sb) {}
        public void AddListItem(StringBuilder sb, string item) {
            sb.AppendLine($" * {item}");
        }
    }

    public class HtmlListStrategy : IListStrategy {
        public void Start(StringBuilder sb) {
            sb.AppendLine("<ul>");
        }
        public void End(StringBuilder sb) {
            sb.AppendLine("</ul>");
        }
        public void AddListItem(StringBuilder sb, string item) {
            sb.AppendLine($"  <li>{item}</li>");
        }
    }

    public class TextProcessor {

        private readonly StringBuilder _sb = new StringBuilder();
        private IListStrategy _listStrategy;

        public void SetOutputFormat(OutputFormat format) {
            switch (format) {
                case OutputFormat.Markdown:
                    _listStrategy = new MarkdownListStrategy();
                    break;
                case OutputFormat.Html:
                    _listStrategy = new HtmlListStrategy();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }

        public void AppendList(IEnumerable<string> items) {
            _listStrategy.Start(_sb);
            foreach (var item in items)
                _listStrategy.AddListItem(_sb, item);
            _listStrategy.End(_sb);
        }

        public StringBuilder Clear() {
            return _sb.Clear();
        }

        public override string ToString() {
            return _sb.ToString();
        }
    }

}
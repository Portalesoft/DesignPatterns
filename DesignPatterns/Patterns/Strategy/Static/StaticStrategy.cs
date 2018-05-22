using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Patterns.Strategy.Static {

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

    /// <summary>
    /// Defined at compile time i.e.: policy
    /// </summary>
    /// <typeparam name="TLs"></typeparam>
    public class TextProcessor<TLs> where TLs : IListStrategy, new() {
        private readonly StringBuilder _sb = new StringBuilder();
        private readonly IListStrategy _listStrategy = new TLs();

        public void AppendList(IEnumerable<string> items) {
            _listStrategy.Start(_sb);
            foreach (var item in items)
                _listStrategy.AddListItem(_sb, item);
            _listStrategy.End(_sb);
        }

        public override string ToString() {
            return _sb.ToString();
        }
    }
}

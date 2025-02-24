using System.Text;

namespace DesignPatterns
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;
        public HtmlElement() 
        { 
        }
        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }
        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");

            if(!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }
            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>"); //closing tag
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class HtmlBuilder
    {

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            Console.WriteLine(sb);

            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat($"<li>{word}</li>");
            }
            sb.Append("</ul>");
            Console.WriteLine(sb);

        }
    }
}

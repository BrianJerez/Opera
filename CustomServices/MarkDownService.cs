using Markdig;

namespace Opera.CustomServices
{
    public class MarkDownService
    {
        public string StringToHtml(string Text)
        {
            return Markdown.ToHtml(Text);
        }
    }
}
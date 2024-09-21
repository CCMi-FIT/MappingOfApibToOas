namespace NJ.SharedModel
{
    public abstract class Description
    {
        public abstract string Text { get; init; }
    }

    public class PlainTextDescription : Description
    {
        public override string Text { get; init; }
    }

    // TODO: Better ?
    public class MarkdownDescription : Description
    {
        public override string Text { get; init; }
    }
}

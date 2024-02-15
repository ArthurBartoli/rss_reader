namespace rss_reader.toolbox
{
    public class NullException : Exception
    {
        public NullException()
            : base("This object is null and it shouldn't be at this point of the process.")
        {
        }

        public NullException(string message)
            : base(message)
        {
        }

        public NullException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
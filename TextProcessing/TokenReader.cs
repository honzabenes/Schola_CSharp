namespace TextProcessing
{
    public abstract class TokenReader
    {
        protected TextReader _reader;
        protected char[] _whiteSpaces { get; init; }

        public TokenReader(TextReader reader, params char[] whiteSpaces)
        {
            _reader = reader;

            if (whiteSpaces.Length == 0)
            {
                throw new ArgumentException("At least one separator must be provided.");
            }

            _whiteSpaces = whiteSpaces;
        }

        public abstract Token ReadToken();
    }
}

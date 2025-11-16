namespace TextProcessing
{
    public enum TokenType
    { 
        Word,
        Space,
        EoI,
        EoL,
        EoP
    }

    /// <summary>
    /// Represents a lexical token.
    /// </summary>
    public readonly struct Token
    {
        public TokenType Type { get; init; }
        public string? Word { get; init; }

        public Token(TokenType type)
        {
            if (type == TokenType.Word)
            {
                throw new InvalidOperationException("Use Token(string word) constructor instead.");
            }

            Type = type;
            Word = null;
        }

        public Token(string word)
        {
            Type = TokenType.Word;
            Word = word;
        }

        public override string ToString()
        {
            return $"Type: {Type}, Word: {Word}";
        }
    }
}

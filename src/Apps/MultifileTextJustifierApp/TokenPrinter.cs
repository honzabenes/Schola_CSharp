using TokenProcessingFramework;

namespace MultifileTextJustifierApp
{
    /// <summary>
    /// Prints text based on tokens coming from the given input stream to the specified output.
    /// </summary>
    public class TokenPrinter
    {
        private ITokenReader _reader;
        private TextWriter _writer;
        private bool _isNewParagraph = false;

        public TokenPrinter(ITokenReader reader, TextWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }


        public void PrintAllTokens(bool highlightWhiteSpaces = false)
        {
            Token token;

            while ((token = _reader.ReadToken()) is not { Type: TokenType.EoI })
            {
                PrintToken(token, highlightWhiteSpaces);
            }

            PrintToken(token, highlightWhiteSpaces);
        }


        private void PrintToken(Token token, bool higlightWhiteSpaces)
        {
            string newLineChar = higlightWhiteSpaces ? "<-\n" : "\n";
            string spaceChar = higlightWhiteSpaces ? "." : " ";

            switch (token.Type)
            {
                case TokenType.Space:
                    _writer.Write(spaceChar);
                    break;

                case TokenType.EoL:
                    _writer.Write(newLineChar);
                    break;

                case TokenType.EoP:
                    _isNewParagraph = true;
                    _writer.Write(newLineChar);
                    break;

                case TokenType.Word:
                    if (_isNewParagraph)
                    {
                        _writer.Write(newLineChar);
                        _isNewParagraph = false;
                    }
                    _writer.Write(token.Word);
                    break;

                default:
                    break;
            }
        }
    }
}

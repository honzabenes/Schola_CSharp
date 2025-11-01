namespace TextProcessing
{
    /// <summary>
    /// Wraps an existing <see cref="ITokenReader"/> for text justification implementation based
    /// on the given maxLineWidth. This class assumes justified End of Line tokens on the input.
    /// </summary>
    public class SpaceAddingTokenReaderWrapper : ITokenReader
    {
        private ITokenReader _reader { get; set; }
        private Queue<Token> _wordBuffer { get; set; } = new Queue<Token>();
        private int _maxLineWidth { get; init; }

        public SpaceAddingTokenReaderWrapper(ITokenReader reader, int maxLineWidth)
        {
            _reader = reader;
            _maxLineWidth = maxLineWidth;
        }


        public Token ReadToken()
        {
            if (_wordBuffer.Count == 0)
            {
                LoadNextLine();
            }

            return _wordBuffer.Dequeue();
        }


        private void LoadNextLine()
        {
            List<Token> wordsInLine = new List<Token>();
            Token lineEndingToken;

            Token token;


            while ((token = _reader.ReadToken()) is { Type: TypeToken.Word })
            {
                wordsInLine.Add(token);
            }

            lineEndingToken = token;

            // Reader didn't load any word token
            if (wordsInLine.Count == 0)
            {
                _wordBuffer.Enqueue(lineEndingToken);
                return;
            }

            // One word line, justify to the left
            if (wordsInLine.Count == 1)
            {
                _wordBuffer.Enqueue(wordsInLine[0]);
                _wordBuffer.Enqueue(lineEndingToken);
                return;
            }


            bool justified = lineEndingToken.Type == TypeToken.EoL;

            int spacesCount = wordsInLine.Count - 1;

            int totalWordsLength = 0;
            foreach (Token word in wordsInLine)
            {
                totalWordsLength += word.Word!.Length;
            }

            int totalSpacesWidth = _maxLineWidth - totalWordsLength;

            int baseSpaceWidth;
            int spacesRemainder;

            if (justified)
            {
                baseSpaceWidth = totalSpacesWidth / spacesCount;
                spacesRemainder = totalSpacesWidth % spacesCount;
            }
            else
            {
                baseSpaceWidth = 1;
                spacesRemainder = 0;
            }


            for (int i = 0; i < wordsInLine.Count; i++)
            {
                _wordBuffer.Enqueue(wordsInLine[i]);

                // If the word isn't the last on the line, add spaces
                if (i < spacesCount)
                {
                    for (int j = 0; j < baseSpaceWidth; j++)
                    {
                        _wordBuffer.Enqueue(new Token(TypeToken.Space));
                    }

                    if (spacesRemainder > 0)
                    {
                        _wordBuffer.Enqueue(new Token(TypeToken.Space));
                        spacesRemainder--;
                    }
                }
            }

            _wordBuffer.Enqueue(lineEndingToken);
        }
    }
}

namespace TextProcessing_Tests
{
    public class WordCounter_Tests
    {
        public char[] WHITE_SPACES = { '\t', ' ', '\r', '\n' };


        [Fact]
        public void NoWord()
        {
            // Arrange.
            string input = """
                     
                   
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("0", output);
        }


        [Fact]
        public void OneWordNoWhiteSpaces()
        {
            // Arrange.
            string input = "hello";

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("1", output);
        }



        [Fact]
        public void OneWordSomeWhiteSpaces()
        {
            // Arrange.
            string input = """
                    
                  Hello.
                    
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("1", output);
        }



        [Fact]
        public void MoreWordSomeWhiteSpaces()
        {
            // Arrange.
            string input = """
                    
                  Hello.    World

                What a  nice day.
                    
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("6", output);
        }
    }
}

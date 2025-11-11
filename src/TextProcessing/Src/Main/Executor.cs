namespace TextProcessing
{
    /// <summary>
    /// Reads all tokens from the given <see cref="ITokenReader"/>, processes them using the specified
    /// <see cref="ITokenProcessor"/> and writes the output to the provided <see cref="TextWriter"/>
    /// </summary>
    public static class Executor
    {
        public const string InvalidFileFormatErrorMessage = "Invalid File Format";
        public const string InvalidIntegerValueErrorMessage = "Invalid Integer Value";
        public const string NonExistentColumnNameErrorMessage = "Non-existent Column Name";

        public static void ProcessAllWords(ITokenReader reader, ITokenProcessor processor, TextWriter writer, TextWriter errWriter)
        {
            try
            {
                Token token;

                while ((token = reader.ReadToken()) is not { Type: TokenType.EoI })
                {
                    processor.ProcessToken(token);
                }

                processor.ProcessToken(token);
            }
            catch (InvalidInputFormatException)
            {
                errWriter.WriteLine(InvalidFileFormatErrorMessage);
                return;
            }
            catch (NotParsableByIntException)
            {
                errWriter.WriteLine(InvalidIntegerValueErrorMessage);
                return;
            }
            catch (NonExistenColumnNameInTableException)
            {
                errWriter.WriteLine(NonExistentColumnNameErrorMessage);
                return;
            }

            processor.WriteOut(writer);
        }
    }
}

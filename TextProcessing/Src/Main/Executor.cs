namespace TextProcessing
{
    /// <summary>
    /// Reads all tokens from the given <see cref="ITokenReader"/>, processes them using the specified
    /// <see cref="ITokenProcessor"/> and writes the output to the provided <see cref="TextWriter"/>
    /// </summary>
    public static class Executor
    {
        public static void ProcessAllWords(ITokenReader reader, ITokenProcessor processor, TextWriter writer, TextWriter errWriter)
        {
            try
            {
                Token token;

                while ((token = reader.ReadToken()) is not { Type: TypeToken.EoI })
                {
                    processor.ProcessToken(token);
                }

                processor.ProcessToken(token);
            }
            catch (InvalidInputFormatException)
            {
                errWriter.WriteLine("Invalid File Format");
                return;
            }
            catch (NotParsableByIntException)
            {
                errWriter.WriteLine("Invalid Integer Value");
                return;
            }
            catch (NonExistenColumnNameInTableException)
            {
                errWriter.WriteLine("Non-existent Column Name");
                return;
            }

            processor.WriteOut(writer);
        }
    }
}

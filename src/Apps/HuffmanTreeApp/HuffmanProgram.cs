using ByteProcessingFramework;
using FileProcessingConsoleAppFramework;
using HuffmanTreeFramework;
using TokenProcessingFramework;
using WordFrequencyCounterApp;

namespace Huffman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new AppErrorHandler(Console.Out);

            app.Execute(new HuffmanProgramCore(), args);
        }

        public class HuffmanProgramCore : IProgramCore
        {
            public void Run(string[] args)
            {
                var IOState = new InputOutputState(args);

                try
                {
                    IOState.CheckArgumentsCount(1);
                    IOState.OpenInputByteStream(0);

                    IByteReader fileByteReader = new FileByteReader(IOState.InputByteStream!);
                    var fileByteReaderToTokenReaderAdapter = new ByteReaderToTokenReaderAdapter(fileByteReader);

                    var wordFrequencyCounter = new WordFrequencyCounter();

                    TokenProcessing.ProcessTokensUntilEndOfInput(fileByteReaderToTokenReaderAdapter, wordFrequencyCounter);

                    var huffmanTreeBuilder = new HuffmanTreeBuilder(wordFrequencyCounter.Words);

                    Node root = huffmanTreeBuilder.Build();
                    huffmanTreeBuilder.WriteOut(root);
                }
                finally
                {
                    IOState.Dispose();
                }
            }
        }
    }
}

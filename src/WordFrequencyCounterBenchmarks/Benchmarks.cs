using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


namespace WordFrequencyCounterBenchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<IncrementCountInDictionary_WordNotFound>();
            BenchmarkRunner.Run<IncrementValueInDictionary_WordFound>();
        }
    }


    public class IncrementCountInDictionary_WordNotFound
    {
        private IDictionary<string, int> _wordFrequencies = new Dictionary<string, int>();
        private string[] _words;
        private int _wordIndex;

        private const int _operationsPerIteration = 100_000;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _words = new string[_operationsPerIteration];

            for (int i = 0; i < _operationsPerIteration; i++)
            {
                _words[i] = "word" + i;
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _wordFrequencies.Clear();
            _wordIndex = 0;
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_V1()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string word = _words[_wordIndex++];

                try
                {
                    _wordFrequencies[word]++;
                }
                catch (KeyNotFoundException)
                {
                    _wordFrequencies[word] = 1;
                }
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_V2()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string word = _words[_wordIndex++];

                if (_wordFrequencies.ContainsKey(word))
                {
                    _wordFrequencies[word]++;
                }
                else
                {
                    _wordFrequencies[word] = 1;
                }
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_V3()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string word = _words[_wordIndex++];

                _ = _wordFrequencies.TryGetValue(word, out int value);     // If not found, value == default(int) == 0
                value++;
                _wordFrequencies[word] = value;
            }
        }
    }


    public class IncrementValueInDictionary_WordFound
    {
        private IDictionary<string, int> _wordFrequencies = new Dictionary<string, int>();
        private string word = "test";

        [Benchmark]
        public void IncrementWordCount_V1()
        {
            try
            {
                _wordFrequencies[word]++;
            }
            catch (KeyNotFoundException)
            {
                _wordFrequencies[word] = 1;
            }
        }


        [Benchmark]
        public void IncrementWordCount_V2()
        {
            if (_wordFrequencies.ContainsKey(word))
            {
                _wordFrequencies[word]++;
            }
            else
            {
                _wordFrequencies[word] = 1;
            }
        }


        [Benchmark]
        public void IncrementWordCount_V3()
        {
            _ = _wordFrequencies.TryGetValue(word, out int value);     // If not found, value == default(int) == 0
            value++;
            _wordFrequencies[word] = value;
        }
    }
}

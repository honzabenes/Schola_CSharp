using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace WordFrequencyOptimalDataStructureBenchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<IncrementCountInDataStructure_KeyNotFound>();
            BenchmarkRunner.Run<IncrementCountInDataStructure_KeyFound>();
        }
    }


    public class IncrementCountInDataStructure_KeyNotFound
    {
        private SortedList<string, int> _sortedList = new SortedList<string, int>();
        private SortedDictionary<string, int> _sortedDictionary = new SortedDictionary<string, int>();
        private Dictionary<string, int> _dictionary = new Dictionary<string, int>();

        private string[] _keys;

        private const int _operationsPerIteration = 100_000;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _keys = new string[_operationsPerIteration];

            for (int i = 0; i < _operationsPerIteration; i++)
            {
                _keys[i] = "key" + i;
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _dictionary.Clear();
            _sortedDictionary.Clear();
            _sortedList.Clear();
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_SortedList()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                try
                {
                    _dictionary[key]++;
                }
                catch (KeyNotFoundException)
                {
                    _dictionary[key] = 1;
                }
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_SortedDictionary()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                if (_dictionary.ContainsKey(key))
                {
                    _dictionary[key]++;
                }
                else
                {
                    _dictionary[key] = 1;
                }
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_Dictionary()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                _ = _dictionary.TryGetValue(key, out int value);
                value++;
                _dictionary[key] = value;
            }
        }
    }


    public class IncrementCountInDataStructure_KeyFound
    {
        private SortedList<string, int> _sortedList = new SortedList<string, int>();
        private SortedDictionary<string, int> _sortedDictionary = new SortedDictionary<string, int>();
        private Dictionary<string, int> _dictionary = new Dictionary<string, int>();

        private string[] _keys;

        private const int _operationsPerIteration = 100_000;
        private const int _keysCountInDataStructure = 10_000;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _keys = new string[_operationsPerIteration];

            for (int i = 0; i < _operationsPerIteration; i++)
            {
                _keys[i] = "key" + (i % _keysCountInDataStructure);
            }

            for (int i = 0; i < _keysCountInDataStructure; i++)
            {
                _sortedList.Add(_keys[i], 1);
                _sortedDictionary.Add(_keys[i], 1);
                _dictionary.Add(_keys[i], 1);
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _sortedList.Clear();
            _sortedDictionary.Clear();
            _dictionary.Clear();
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_SortedList()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                _ = _sortedList.TryGetValue(key, out int value);
                value++;
                _sortedList[key] = value;
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_SortedDictionary()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                _ = _sortedDictionary.TryGetValue(key, out int value);
                value++;
                _sortedDictionary[key] = value;
            }
        }


        [Benchmark(OperationsPerInvoke = _operationsPerIteration)]
        public void IncrementWordCount_Dictionary()
        {
            for (int i = 0; i < _operationsPerIteration; i++)
            {
                string key = _keys[i];

                _ = _dictionary.TryGetValue(key, out int value);
                value++;
                _dictionary[key] = value;
            }
        }
    }
}

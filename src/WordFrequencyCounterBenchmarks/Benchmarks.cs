using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

// Při psaní benchmarků pro případ, že se slovo, u kterého chceme zvyšovat počet výskytů, ještě ve slovníku nenachází,
// jsem narazil na problém, jak zaručit, aby se v každé operaci při běhu benchmarku přidávané slovo opravdu
// ve slovníku nenacházelo. Ńejprve jsem sám problém vyřešil tím, že jsem v každé ze tří funkcí nejprve slovník
// vyprázdnil. To mi ale přijde jako špatné řešení, jelikož do implementace funkcí bychom asi ideálně neměli vůbec
// sahat. Pak jsem našel v dokumentaci BenchmarkDotNet, že lze pomocí Atributu IterationSetup vyprázdnit slovník
// před každou iterací. Jenže to, jak jsem zjistil donutí benchmark, aby v každé iteraci prováděl jen jednu operaci.
// Z toho, si myslím, by nevycházely dobré výsledky vzhledem k malému počtu spouštění funkce. Poté jsem se AI zeptal,
// jak by to jinak šlo vyřešit a ta mi nabídla způsob, podle kterého jsem nakonec benchmarky implementoval.
// https://aistudio.google.com/app/prompts?state=%7B%22ids%22:%5B%221QEquw94qHFVfXt3HABZWROdfSx-TH5sh%22%5D,%22action%22:%22open%22,%22userId%22:%22111991003136347724951%22,%22resourceKeys%22:%7B%7D%7D&usp=sharing

namespace WordFrequencyCounterBenchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<IncrementCountInDictionary_WordNotFound>();
            BenchmarkRunner.Run<IncrementCountInDictionary_WordFound>();
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


    public class IncrementCountInDictionary_WordFound
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

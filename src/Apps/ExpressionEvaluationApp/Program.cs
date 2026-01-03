using ExpressionEvaluationFramework;

namespace ExpressionEvaluationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string expression = Console.ReadLine();

            try
            {
                Node? root = ExpressionTreeBuilder.Build(expression);

                int result = ExpressionTreeEvaluator.Evaluate(root);
                Console.WriteLine(result);
            }
            catch (FormatException)
            {
                Console.WriteLine("Format Error");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Overflow Error");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Divide Error");
            }
        }
    }
}

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
                ExpressionTreeNode? root = ExpressionTreeBuilder.Build(expression);
                int result = root.Evaluate();

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

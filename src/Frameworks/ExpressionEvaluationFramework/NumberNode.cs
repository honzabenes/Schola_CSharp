namespace ExpressionEvaluationFramework
{
    public class NumberNode : Node
    {
        public int Value { get; init; }

        public NumberNode(int value) : base(null, null)
        {
            Value = value;
        }
    }
}

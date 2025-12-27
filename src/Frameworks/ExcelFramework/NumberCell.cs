namespace ExcelFramework
{
    /// <summary>
    /// Represents a cell in the sheet that holds some constant integer value.
    /// </summary>
    public record NumberCell : Cell
    {
        public NumberCell(int value) : base(value, CellState.Calculated) { }


        public override string GetOutputString()
        {
            return $"{Value}";
        }


        public override EvaluationResult GetValue(Sheet sheet)
        {
            return new EvaluationResult((int)Value!);
        }
    }
}

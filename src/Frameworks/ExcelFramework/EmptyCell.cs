namespace ExcelFramework
{
    /// <summary>
    /// Represents a cell in the sheet that is explicitly empty.
    /// </summary>
    public sealed record EmptyCell : Cell
    {
        public EmptyCell() : base(0, CellState.Calculated) { }


        public override string GetOutputString()
        {
            return "[]";
        }


        public override EvaluationResult GetValue(Sheet sheet)
        {
            return new EvaluationResult(0);
        }
    }
}

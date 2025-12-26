namespace ExcelFramework
{
    public record ErrorCell : Cell
    {
        public string ErrorMessage;

        public ErrorCell(string errorMessage) : base(0, CellState.Error)
        {
            ErrorMessage = errorMessage;
        }


        public override string GetOutputString()
        {
            return ErrorMessage;
        }


        public override EvaluationResult GetValue(Sheet sheet)
        {
            return new EvaluationResult(ErrorMessage);
        }
    }
}

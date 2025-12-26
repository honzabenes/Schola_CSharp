namespace ExcelFramework
{
    public readonly struct EvaluationResult
    {
        public bool IsSucces { get; }
        public int Value { get; }
        public string ErrorMessage { get; }
        public Cell? CycleInitiatior { get; }

        public EvaluationResult(int value)
        {
            IsSucces = true;
            Value = value;
            ErrorMessage = string.Empty;
            CycleInitiatior = null;
        }

        public EvaluationResult(string errorMessage, Cell? cycleInitiator = null)
        {
            IsSucces = false;
            Value = 0;
            ErrorMessage = errorMessage;
            CycleInitiatior = cycleInitiator;
        }
    }
}

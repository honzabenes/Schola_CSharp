namespace ExcelFramework
{
    /// <summary>
    /// Represents the base contract for a cell in the sheet.
    /// </summary>
    public abstract record Cell
    {
        public int? Value { get; protected set; }
        public CellState State { get; protected set; }

        public Cell(int? value, CellState state)
        {
            Value = value;
            State = state;
        }


        /// <summary>
        /// Returns a value that should be printed in the particular cell in the output sheet.
        /// </summary>
        public abstract string GetOutputString();


        /// <summary>
        /// Returns the result of the evaluation of the particular cell.
        /// </summary>
        public abstract EvaluationResult GetValue(Sheet sheet);
    }
}

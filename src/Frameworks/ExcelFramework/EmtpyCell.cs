namespace ExcelFramework
{
    public record EmtpyCell : Cell
    {
        public EmtpyCell() : base(0, CellState.Calculated) { }


        public override string GetOutputString()
        {
            return "[]";
        }


        public override int GetValue(Sheet sheet)
        {
            return 0;
        }
    }
}

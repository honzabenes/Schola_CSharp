namespace ExcelFramework
{
    public record FormulaCell : Cell
    {
        private string ErrorMessage = "";
        private char Operator;
        private CellAddress FirstOperandAddress;
        private CellAddress SecondOperandAddress;

        public FormulaCell(char @operator, CellAddress firstOperand, CellAddress secondOperand)
            : base(null, CellState.Uncalculated)
        {
            Operator = @operator;
            FirstOperandAddress = firstOperand;
            SecondOperandAddress = secondOperand;
        }


        public override string GetOutputString()
        {
            if (State == CellState.Error)
            {
                return ErrorMessage;
            }

            return $"{Value}";
        }


        public override EvaluationResult GetValue(Sheet sheet)
        {
            if (State == CellState.Calculated)
            {
                return new EvaluationResult((int)Value!);
            }

            if (State == CellState.Error)
            {
                return new EvaluationResult(ErrorMessage);
            }

            if (State == CellState.Calculating)
            {
                return new EvaluationResult(ErrorMessages.Cycle, this);
            }

            State = CellState.Calculating;

            EvaluationResult val1 = sheet.GetCellValue(FirstOperandAddress);
            EvaluationResult val2 = sheet.GetCellValue(SecondOperandAddress);

             // TODO
        }

        
        private int CalculateResult(int val1, int val2)
        {
            switch (Operator)
            {
                case '+': return val1 + val2;
                case '-': return val1 - val2;
                case '*': return val1 * val2;
                case '/':
                    if (val2 == 0)
                    {
                        throw new DivideByZeroApplicationException();
                    }
                    else
                    {
                        return val1 / val2;
                    }
                default: return 0;
            }
        }


        private EvaluationResult HandleError(EvaluationResult error)
        {
            // TODO
        }

        private EvaluationResult SetErrorState(string message)
        {
            ErrorMessage = message;
            State = CellState.Error;
            return new EvaluationResult(message);
        }
    }
}

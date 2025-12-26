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


        public override int GetValue(Sheet sheet)
        {
            if (State == CellState.Calculated)
            {
                return (int)Value!;
            }

            if (State == CellState.Error)
            {
                throw new TryingToGetValueFromErrorCellApplicationException();
            }

            if (State == CellState.Calculating)
            {
                HandleError(ErrorMessages.Cycle, new CycleDetectedApplicationException());
            }

            State = CellState.Calculating;

            try
            {
                int val1 = sheet.GetCellValue(FirstOperandAddress);
                int val2 = sheet.GetCellValue(SecondOperandAddress);

                Value = CalculateResult(val1, val2);

                State = CellState.Calculated;
                return (int)Value!;
            }
            catch (CycleDetectedApplicationException)
            {
                HandleError(ErrorMessages.Cycle, new CycleDetectedApplicationException());
            }
            catch (DivideByZeroApplicationException)
            {
                HandleError(ErrorMessages.DivZero, new TryingToGetValueFromErrorCellApplicationException());
            }
            catch (TryingToGetValueFromErrorCellApplicationException)
            {
                HandleError(ErrorMessages.Error, new TryingToGetValueFromErrorCellApplicationException());
            }

            return 0;
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


        private void HandleError(string message, ApplicationException exceptionToThrow)
        {
            ErrorMessage = message;
            State = CellState.Error;
            throw exceptionToThrow;
        }
    }
}

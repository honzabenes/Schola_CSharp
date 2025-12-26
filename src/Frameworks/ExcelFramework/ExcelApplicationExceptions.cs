namespace ExcelFramework
{
    public class InvalidCellAddressLabelApplicationException : ApplicationException { }
    public class CycleDetectedApplicationException : ApplicationException { }
    public class DivideByZeroApplicationException : ApplicationException { }
    public class TryingToGetValueFromErrorCellApplicationException : ApplicationException { }
}

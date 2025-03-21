namespace NPU.Data.Exceptions;

public class DalException: Exception
{
    public DalException(string message) : base(message) { }
    public DalException(string message, Exception innerException) : base(message, innerException) { }
}
namespace UserMicroservice.Business.Exceptions;

public abstract class SampleException : Exception
{
    public SampleException(string message)
        : base(message)
    { }

    public abstract int ErrorCode { get; }
}


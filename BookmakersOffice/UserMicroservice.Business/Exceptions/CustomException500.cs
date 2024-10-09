namespace UserMicroservice.Business.Exceptions;

public class CustomeException500 : SampleException
{
    public CustomeException500()
        : base($"Unknown Error")
    { }

    public override int ErrorCode =>500;
}
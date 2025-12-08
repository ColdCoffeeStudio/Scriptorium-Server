namespace Domain.Shared;

public class Error(string code, string message)
{
    public string Code { get; } = code;
    public string Message { get; } = message;

    public static Error Empty()
    {
        return new Error("", "");
    }
    
    public override bool Equals(object? other)
    {
        bool areEqual = false;

        if (other is not Error otherError) return areEqual;
        areEqual =  Code == otherError.Code 
                    && Message == otherError.Message;

        return areEqual;
    }
    public override int GetHashCode() => HashCode.Combine(Code, Message);
}
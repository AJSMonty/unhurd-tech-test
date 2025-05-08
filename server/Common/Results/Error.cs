namespace Unhurd.Infrastructure.Common;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.Null", "A null value was encountered.");
}

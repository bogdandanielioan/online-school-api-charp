namespace online_school_api.Books.ValueObjects;

public record ISBN
{
    public string Value { get; }
    private ISBN() // EF Core needs this sometimes
    {
        Value = string.Empty;
    }
    public ISBN(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 13 || !value.All(char.IsDigit))
            throw new ArgumentException("Invalid ISBN format. ISBN must be 13 digits.");
        Value = value;
    }

    public override string ToString() => Value;
}

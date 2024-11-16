using System.Diagnostics;
using System.Text.Json;

namespace UniSyncApi.Exceptions;

public class ExceptionDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
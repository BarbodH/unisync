namespace UniSyncApi.Exceptions;

public class InvalidFieldException(string resource, string field)
    : Exception($"Invalid field '{field}' for resource '{resource}'.");
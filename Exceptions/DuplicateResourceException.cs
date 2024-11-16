namespace UniSyncApi.Exceptions;

public class DuplicateResourceException(string resource, string field)
    : Exception($"Duplicate field '{field}' for resource '{resource}'.");
namespace UniSyncApi.Exceptions;

public class ResourceNotFoundException(string name) : Exception($"Unable to find resource '{name}'.");
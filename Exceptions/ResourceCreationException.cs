namespace UniSyncApi.Exceptions;

public class ResourceCreationException(string name) : Exception($"Failed to create resource '{name}'.");
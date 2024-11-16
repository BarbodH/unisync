namespace UniSyncApi.Exceptions;

public class AuthenticationException(string field) : Exception($"Authentication failed due to invalid '{field}'.");
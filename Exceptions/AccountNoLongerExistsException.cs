namespace UniSyncApi.Exceptions;

public class AccountNoLongerExistsException(string accountId)
    : Exception($"The account associated with user ID '{accountId}' no longer exists.");
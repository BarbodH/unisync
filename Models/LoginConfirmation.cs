namespace UniSyncApi.Models;

public class LoginConfirmation
{
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
}
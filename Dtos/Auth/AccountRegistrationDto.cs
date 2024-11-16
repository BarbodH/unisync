namespace UniSyncApi.Dtos.Auth;

public class AccountRegistrationDto
{
    public int Role { get; set; } // 0: Admin, 1: Instructor, 2: Student
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string PasswordConfirm { get; set; } = "";
}
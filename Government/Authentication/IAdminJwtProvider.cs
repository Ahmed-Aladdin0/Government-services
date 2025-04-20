namespace Government.Authentication
{
    public interface IAdminJwtProvider
    {
        (string token, int expiresIn) GenerateToken(AppUser user, IEnumerable<string> roles, IEnumerable<string> permissions);


    }
}

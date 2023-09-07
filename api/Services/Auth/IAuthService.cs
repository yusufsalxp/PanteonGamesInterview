using api.Entities;

public interface IAuthService
{
    public Task<string> Login(UserLoginDto dto);
    public Task<User> Register(UserRegisterDto dto);
    public Task<User?> GetById(Guid id);
}
using shega_meetups_api.Entities;

namespace shega_meetups_api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
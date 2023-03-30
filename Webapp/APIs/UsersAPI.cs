using Entities;
using Infrastructure.Exceptions;

namespace Webapp.APIs;
public class UsersAPI : IAPI
{
    public UsersAPI(IConfiguration configuration) 
        : base(configuration["WebapiHost"]) { }

    public void create(User user)
    {
        var response = Post("/users", user).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}

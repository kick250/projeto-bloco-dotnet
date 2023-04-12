using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Webapp.APIs;

public class UserInfoAPI : IAPI
{
    public UserInfoAPI(IConfiguration configuration)
       : base(configuration["WebapiHost"]) { }


    public User GetMyInfo()
    {
        var response = Get($"/UserInfo").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        User? user = JsonConvert.DeserializeObject<User>(jsonResult);

        if (user == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return user;
    }

    public void Update(User user)
    {
        var response = Put($"/UserInfo", user).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }

    public void DeleteMyUser()
    {
        var response = Delete($"/UserInfo", new {}).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}

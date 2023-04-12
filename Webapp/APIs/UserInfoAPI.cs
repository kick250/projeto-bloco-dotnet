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
}

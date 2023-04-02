using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;
using Webapp.Models;


namespace Webapp.APIs;
public class FriendsAPI : IAPI
{
    public FriendsAPI(IConfiguration configuration) 
        : base(configuration["WebapiHost"]) { }

    public List<User> GetFriendsOf(Account account)
    {
        AddToken(account.GetToken());

        var response = Get($"/friends").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        var friends = JsonConvert.DeserializeObject<List<User>>(jsonResult);

        if (friends == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return friends;
    }

    public void AddFriend(Account account, string friendEmail)
    {
        AddToken(account.GetToken());

        var response = Post($"/friends/{friendEmail}", new {}).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }

    public void RemoveFriend(Account account, string friendEmail) 
    {
        AddToken(account.GetToken());

        var response = Delete($"/friends/{friendEmail}", new {}).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}

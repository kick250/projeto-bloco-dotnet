using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;
using Webapp.Models;

namespace Webapp.APIs;

public class PostsAPI : IAPI
{
    public PostsAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public Post GetById(Account account, int id)
    {
        AddToken(account.Token ?? "");

        var response = Get($"/Posts/{id}").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        Post? post = JsonConvert.DeserializeObject<Post>(jsonResult);

        if (post == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return post;
    }
}

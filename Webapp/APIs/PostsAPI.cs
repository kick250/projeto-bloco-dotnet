using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Webapp.APIs;

public class PostsAPI : IAPI
{
    public PostsAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }


    public List<Post> GetAll()
    {
        var response = Get($"/Posts").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        List<Post>? posts = JsonConvert.DeserializeObject<List<Post>>(jsonResult);

        if (posts == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return posts;
    }

    public Post GetById(int id)
    {
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

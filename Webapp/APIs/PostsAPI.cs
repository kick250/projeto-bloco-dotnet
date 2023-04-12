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

    public void Create(Post post)
    {
        var response = Post($"/Posts", post).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }

    public void Update(Post post)
    {
        var response = Put($"/Posts/{post.Id}", post).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }

    public void DeleteById(int id)
    {
        var response = Delete($"/Posts/{id}", new {}).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}

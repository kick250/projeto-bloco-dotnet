using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;
using Webapp.Models;

namespace Webapp.APIs;

public class CommentsAPI : IAPI
{
    public CommentsAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public Comment GetById(int id)
    {
        var response = Get($"/Comments/{id}").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        Comment? comment = JsonConvert.DeserializeObject<Comment>(jsonResult);

        if (comment == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return comment;
    }

    public void Create(Comment comment, int postId)
    {
        var commentParams = new
        {
            Comment = comment,
            PostId = postId
        };

        var response = Post("/Comments", commentParams).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}

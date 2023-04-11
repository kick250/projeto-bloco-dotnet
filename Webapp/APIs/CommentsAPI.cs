using Entities;
using Infrastructure.Exceptions;

namespace Webapp.APIs;

public class CommentsAPI : IAPI
{
    public CommentsAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

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

    public void DeleteById(int id)
    {
        var response = Delete($"/Comments/{id}", new {}).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}

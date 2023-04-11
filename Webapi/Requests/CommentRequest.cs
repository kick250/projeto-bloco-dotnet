using Entities;
using Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Webapi.Requests;

public class CommentRequest
{
    [Required(ErrorMessage = "É necessário o dado no comentário nessa requisição")]
    public Comment? Comment { get; set; }
    [Required(ErrorMessage = "É necessário post do comentário nessa requisição")]
    public int? PostId { get; set; }

    public Comment GetComment()
    {
        if (Comment == null)
            throw new RequiredParameterNotPresent("Comment");

        return Comment;
    }

    public int GetPostId()
    {
        if (PostId == null)
            throw new RequiredParameterNotPresent("PostId");

        return int.Parse($"{PostId}");
    }

}

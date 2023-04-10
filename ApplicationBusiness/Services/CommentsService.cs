using Repository;
using Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ApplicationBusiness.Services;
public class CommentsService
{
    private HomeRepairContext Context { get; set; }
    private IEnumerable<Comment> Comments { get; set; }

    public CommentsService(HomeRepairContext context)
    {
        Context = context;
        Comments = context.Comments
            .Include(post => post.Owner)
            .Include(post => post.Content);
    }

    public Comment GetById(int id)
    {
        Comment? comment = Comments.FirstOrDefault(comment => comment.Id == id);

        if (comment == null)
            throw new PostNotFoundException();

        return comment;
    }
    
}

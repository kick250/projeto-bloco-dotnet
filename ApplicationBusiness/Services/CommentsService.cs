using Repository;
using Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApplicationBusiness.Services;
public class CommentsService
{
    private HomeRepairContext Context { get; set; }
    private IEnumerable<Comment> Comments { get; set; }

    public CommentsService(HomeRepairContext context)
    {
        Context = context;
        Comments = context.Comments
                    .Include(comment => comment.Owner)
                    .Include(comment => comment.Post);
    }

    public Comment GetById(int id)
    {
        Comment? comment = Comments.FirstOrDefault(comment => comment.Id == id);

        if (comment == null)
            throw new CommentNotFoundException();

        return comment;
    }

    public void Create(Comment comment)
    {
        Context.Comments.Add(comment);
        Context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        Comment comment = GetById(id);

        Context.Comments.Remove(comment);
        Context.SaveChanges();
    }
}

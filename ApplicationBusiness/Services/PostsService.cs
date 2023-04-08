using Repository;
using Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApplicationBusiness.Services;
public class PostsService
{
    private HomeRepairContext Context { get; set; }
    private IEnumerable<Post> Posts { get; set; }

    public PostsService(HomeRepairContext context)
    {
        Context = context;
        Posts = context.Posts
            .Include(post => post.Owner)
            .Include(post => post.Comments).ThenInclude(comment => comment.Owner);
    }

    public Post GetById(int id)
    {
        Post? post = Posts.FirstOrDefault(post => post.Id == id);

        if (post == null)
            throw new PostNotFoundException();

        return post;
    }
    
}

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
            .Include(post => post.Comments).ThenInclude(comment => comment.Owner)
            .Include(post => post.Owner);
    }

    public IEnumerable<Post> GetPostsFor(User user)
    {
        List<int?> usersToGetPost = user.GetFriendIds();
        usersToGetPost.Add(user.Id);

        return Posts.Where(x => x.Owner != null && usersToGetPost.Contains(x.Owner.Id));
    } 

    public Post GetById(int id)
    {
        Post? post = Posts.FirstOrDefault(post => post.Id == id);

        if (post == null)
            throw new PostNotFoundException();

        return post;
    }
    
    public void Create(Post post)
    {
        Context.Posts.Add(post);
        Context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        Post post = GetById(id);

        foreach (Comment comment in post.Comments)
            Context.Comments.Remove(comment);

        Context.Posts.Remove(post);
        Context.SaveChanges();
    }


}

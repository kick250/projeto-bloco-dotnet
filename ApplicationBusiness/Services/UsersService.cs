using Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Text;

namespace ApplicationBusiness.Services;
public class UsersService
{
    private HomeRepairContext Context { get; set; }
    private IEnumerable<User> Users { get; set; }

    public UsersService(HomeRepairContext context)
    {
        Context = context;
        Users = context.Users
            .Include(user => user.Friends)
            .Include(user => user.Posts).ThenInclude(post => post.Comments);
    }

    public User GetById(int id)
    {
        User? user = Users.FirstOrDefault(user => user.Id == id);

        if (user == null)
            throw new UserNotFoundException();

        return user;
    } 

    public User GetByEmail(string email) 
    {
        User? user = Users.FirstOrDefault(user => user.Username == email);

        if (user == null)
            throw new UserNotFoundException();

        return user;
    }

    public void Create(User user)
    {
        if (Exists(user))
            throw new UsernameInUseException();

        if (user.Password != null)
            user.SetPassword(user.Password);

        Context.Users.Add(user);
        Context.SaveChanges();
    }

    public void Update(User userUpdated)
    {
        User user = GetById(int.Parse($"{userUpdated.Id}"));

        user.UpdateFrom(userUpdated);

        Context.Update(user);
        Context.SaveChanges();
    }

    public void Delete(User userToDelete)
    {
        try
        {
            User user = GetById(int.Parse($"{userToDelete.Id}"));

            DeleteUserFriends(user);
            DeleteUserPosts(user);
            DeleteUserComments(user);

            Context.Users.Remove(user);
            Context.SaveChanges();
        } catch (UserNotFoundException) { }
    }

    public void AddFriend(User user, User Friend)
    {
        user.AddFriend(Friend);
        Update(user);
    }

    public void RemoveFriend(User user, User Friend)
    {
        user.RemoveFriend(Friend);
        Update(user);
    }

    public User? Authenticate(string username, string password)
    {
        return Users.FirstOrDefault(user =>
            user.Username == username &&
            user.Password == EncodePassword(password)
        );
    }

    #region private

    private void DeleteUserFriends(User user)
    {
        user.Friends.Clear();
        Update(user);
    }

    private void DeleteUserPosts(User user)
    {
        foreach (Post post in user.Posts)
        {
            foreach (Comment comment in post.Comments)
            {
                Context.Comments.Remove(comment);
            }
            Context.SaveChanges();
            Context.Posts.Remove(post);
        }
        Context.SaveChanges();
    }

    private void DeleteUserComments(User user)
    {
        IEnumerable<Comment> userComments = Context.Comments.Where(comment => comment.Owner == user);
        
        foreach (Comment comment in userComments)
            Context.Comments.Remove(comment);
        Context.SaveChanges();
    }

    private bool Exists(User user)
    {
        if (Users.FirstOrDefault(existingUser => existingUser.Username == user.Username) == null)
            return false;

        return true;
    }
    private string EncodePassword(string value)
    {
        return Convert.ToBase64String(Encoding.Default.GetBytes(value));
    }

    #endregion
}


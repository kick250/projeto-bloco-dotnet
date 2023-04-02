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
            .Include(user => user.Posts);
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

    private void Update(User user)
    {
        Context.Update(user);
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


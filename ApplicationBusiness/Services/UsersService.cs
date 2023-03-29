using Entities;
using Infrastructure;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repository;

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

    public void Create(User user)
    {
        if (Exists(user))
            throw new UsernameInUseException();

        if (user.Password != null)
            user.SetPassword(user.Password);

        Context.Users.Add(user);
        Context.SaveChanges();
    }

    #region private

    private bool Exists(User user)
    {
        if (Users.FirstOrDefault(existingUser => existingUser.Username == user.Username) == null)
            return false;

        return true;
    }

    #endregion
}


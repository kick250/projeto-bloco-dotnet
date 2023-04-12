using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Entities;
public class User
{
	public const string USER_TYPE = "profile";

	public int? Id { get; set; }
	[Required(ErrorMessage = "O email do usuário é necessário."),
	 EmailAddress(ErrorMessage = "O email deve estar no formato válido."),
     JsonPropertyName("username")]
	public string? Username { get; set; }
	[JsonPropertyName("password")]
	public string? Password { get; set; }
	[Required(ErrorMessage = "O nome do usuário é necessário."),
     JsonPropertyName("name")]
	public string? Name { get; set; }
	[Required(ErrorMessage = "O sobrenome do usuário é necessário."),
     JsonPropertyName("lastName")]
	public string? LastName { get; set; }
	[JsonPropertyName("profileImage")]
	public string? ProfileImage { get; set; } = "";
    public string? Type { get; set; } = USER_TYPE;
    public List<Post> Posts { get; set; } = new List<Post>();
	public List<User> Friends { get; set; } = new List<User>();

    public void SetPassword(string password)
	{
		Password = Convert.ToBase64String(Encoding.Default.GetBytes(password));
	}

	public string GetName()
	{
		return Name ?? "";
	}

	public string GetEmail()
	{
		return Username ?? "";
	}

	public string GetPassword()
	{
		return Password ?? "";
	}

	public string GetProfileImage()
	{
		return ProfileImage ?? "";
	}

	public string GetFullName()
	{
		return $"{Name} {LastName}";
	}

	public List<int?> GetFriendIds()
	{
		if (Friends == null || Friends.Count == 0) return new List<int?>();

		return Friends.Select(x => x.Id).ToList();
	}

	public void AddFriend(User friend)
	{
        if (IsFriendOf(friend) || Id == friend.Id) return;

        Friends.Add(friend);
		friend.AddFriend(this);
	}

    public void RemoveFriend(User friend)
    {
        if (!this.IsFriendOf(friend)) return;

        Friends.Remove(friend);
        friend.RemoveFriend(this);
    }

    public bool IsFriendOf(User user)
	{
		return Friends.Contains(user);
	}

	public void UpdateFrom(User user)
	{
		Name = user.Name;
		LastName = user.LastName;
		ProfileImage = user.ProfileImage;
		if (!String.IsNullOrEmpty(user.Password))
			SetPassword(user.Password);
	}

}


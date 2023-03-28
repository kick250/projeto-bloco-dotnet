namespace Entities;


public class User
{
	public const string USER_TYPE = "profile";

	public int? Id { get; set; }
	public string? Username { get; set; }
	public string? Password { get; set; }
	public string? Name { get; set; }
	public string? LastName { get; set; }
	public string? ProfileImage { get; set; }
	public string type { get; set; } = USER_TYPE;
	public List<Post> Posts { get; set; } = new List<Post>();
	public List<User> Friends { get; set; } = new List<User>();
}


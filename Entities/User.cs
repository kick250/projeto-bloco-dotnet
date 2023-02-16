namespace Entities;


public class User
{
	public const string USER_TYPE = "user";

	public int? Id { get; set; }
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public string? ProfileImage { get; set; }
	public List<ServicePost> ServicePosts { get; set; } = new List<ServicePost>();
	public string type { get; set; } = USER_TYPE;
}


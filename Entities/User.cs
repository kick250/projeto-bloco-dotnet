using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities;
public class User
{
	public const string USER_TYPE = "profile";

	public int? Id { get; set; }
	[Required(ErrorMessage = "O email do usuário é necessário."),
	 EmailAddress(ErrorMessage = "O email deve estar no formato válido."),
     JsonPropertyName("username")]
	public string? Username { get; set; }
	[Required(ErrorMessage = "A senha é necessária."),
	 JsonPropertyName("password"),
     JsonIgnore(Condition = JsonIgnoreCondition.Always)]
	public string? Password { get; set; }
	[Required(ErrorMessage = "O nome do usuário é necessário."),
     JsonPropertyName("name")]
	public string? Name { get; set; }
	[Required(ErrorMessage = "O sobrenome do usuário é necessário."),
     JsonPropertyName("lastName")]
	public string? LastName { get; set; }
	[Required(ErrorMessage = "Uma imagem de perfil é necessária."),
     JsonPropertyName("profileImage")]
    public string? ProfileImage { get; set; }
	[JsonIgnore]
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
}


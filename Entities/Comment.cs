using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Comment
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "O comentário não pode ficar vazio.")]
    public string? Content { get; set; }
    public User? Owner { get; set; }
    public Post? Post { get; set; }

    public string GetOwnerName()
    {
        if (Owner == null) return "";

        return Owner.GetFullName();
    }
}

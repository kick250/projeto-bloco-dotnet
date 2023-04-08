namespace Entities;

public class Comment
{
    public int? Id { get; set; }
    public string? Content { get; set; }
    public User? Owner { get; set; }
    public Post? Post { get; set; }
}

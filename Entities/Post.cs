namespace Entities;
public class Post
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
    public User? Owner { get; set; }    
    public List<Comment>? Comments { get; set; }
}


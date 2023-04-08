namespace Entities;
public class Post
{
    public int? Id { get; set; }
    public User? Owner { get; set; }    
    public List<Comment>? Comments { get; set; }
}


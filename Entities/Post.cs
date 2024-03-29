﻿using System.ComponentModel.DataAnnotations;

namespace Entities;
public class Post
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Um post precisa de titulo")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Um post precisa de conteudo")]
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
    public User? Owner { get; set; }    
    public List<Comment> Comments { get; set; } = new List<Comment>();

    public string GetOwnerName()
    {
        if (Owner == null) return "";

        return Owner.GetFullName();
    }

    public string GetOwnerEmail()
    {
        if (Owner == null) return "";

        return Owner.GetEmail();
    }

    public bool Isfrom(string userEmail)
    {
        if (Owner == null || Owner.Username == null || string.IsNullOrEmpty(userEmail)) 
            return false;

        return Owner.Username == userEmail;
    }

    public void UpdateFrom(Post post)
    {
        Title = post.Title;
        Content = post.Content;
        ImageUrl = post.ImageUrl;
    }
}


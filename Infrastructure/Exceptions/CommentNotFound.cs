namespace Infrastructure.Exceptions;
public class CommentNotFoundException : Exception
{
    public string GetMessage()
    {
        return "Esse comentário não foi encontrado.";
    }
}


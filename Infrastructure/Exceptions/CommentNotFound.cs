namespace Infrastructure.Exceptions;
public class CommentNotFoundException : Exception
{
    public override string Message => "Esse comentário não foi encontrado.";
}


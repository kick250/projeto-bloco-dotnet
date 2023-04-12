namespace Infrastructure.Exceptions;

public class PostNotFoundException : Exception
{
    public override string Message => "Esse post não foi encontrado";
}

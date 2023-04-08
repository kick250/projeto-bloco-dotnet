namespace Infrastructure.Exceptions;

public class PostNotFoundException : Exception
{
    public string GetMessage()
    {
        return "Esse post não foi encontrado";
    }
}

namespace Infrastructure.Exceptions;
public class UserNotFoundException : Exception
{
    public string GetMessage()
    {
        return "Esse usuário não foi encontrado.";
    }
}

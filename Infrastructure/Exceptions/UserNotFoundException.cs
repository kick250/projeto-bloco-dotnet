namespace Infrastructure.Exceptions;
public class UserNotFoundException : Exception
{
    public override string Message => "Esse usuário não foi encontrado.";

}

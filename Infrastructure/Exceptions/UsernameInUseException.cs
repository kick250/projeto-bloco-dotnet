namespace Infrastructure.Exceptions;

public class UsernameInUseException : Exception
{
    public override string Message => "Esse email já está em uso.";
}

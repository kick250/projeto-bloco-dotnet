namespace Infrastructure;

public class UsernameInUseException : Exception
{
    public string GetMessage()
    {
        return "Esse email já está em uso.";
    }
}

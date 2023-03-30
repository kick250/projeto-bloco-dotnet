namespace Infrastructure.Exceptions;

public class RequiredParameterNotPresent : Exception
{
    private string ParameterName { get; set; }

    public RequiredParameterNotPresent(string parameterName) : base()
    {
        ParameterName = parameterName;
    }

    public string GetErrorMessage()
    {
        return $"O parametro {ParameterName} é necessário.";
    }
}

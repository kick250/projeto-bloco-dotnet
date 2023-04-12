namespace Infrastructure.Exceptions;

public class RequiredParameterNotPresent : Exception
{
    private string ParameterName { get; set; }

    public RequiredParameterNotPresent(string parameterName) : base()
    {
        ParameterName = parameterName;
    }

    public override string Message => $"O parametro {ParameterName} é necessário.";
}

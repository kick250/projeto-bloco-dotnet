using Newtonsoft.Json;

namespace Infrastructure.Exceptions;
public class APIErrorException : Exception
{
    private string APIError = "Ocorreu um erro ao realizar essa requisição.";

    public APIErrorException(HttpResponseMessage response) : base() 
    {
        string jsonResult = response.Content.ReadAsStringAsync().Result;
        var definition = new { Error = "" };
        var result = JsonConvert.DeserializeAnonymousType(jsonResult, definition);

        if (result != null && result.Error != null)
            APIError = result.Error;
    }

    public string GetMessage()
    {
        return APIError;
    }
}

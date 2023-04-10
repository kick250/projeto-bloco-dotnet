using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json;
using Webapp.Models;

namespace Webapp.APIs;

public class CommentsAPI : IAPI
{
    public CommentsAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public Comment GetById(Account account, int id)
    {
        AddToken(account.Token ?? "");

        var response = Get($"/Comments/{id}").Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        Comment? comment = JsonConvert.DeserializeObject<Comment>(jsonResult);

        if (comment == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return comment;
    }
}

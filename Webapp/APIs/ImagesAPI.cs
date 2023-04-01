using Infrastructure.Exceptions;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;

namespace Webapp.APIs;

public class ImagesAPI : IAPI
{
    private const string PATH = "/Images";

    public ImagesAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public string UploadImage(IFormFile file)
    {
        var response = Client.PostAsync(BuildUrl(PATH), CreateContent(file)).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        var definition = new { ImageUrl = "" };

        var result = JsonConvert.DeserializeAnonymousType(jsonResult, definition);

        if (result == null || result.ImageUrl == null)
            throw new Exception("Ocorreu um erro desconhecido");

        return result.ImageUrl;
    }

    private MultipartFormDataContent CreateContent(IFormFile file)
    {
        var content = new MultipartFormDataContent();
        StreamContent image = new StreamContent(file.OpenReadStream());
        image.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
        content.Add(image, "image", file.FileName);

        return content;
    }
}

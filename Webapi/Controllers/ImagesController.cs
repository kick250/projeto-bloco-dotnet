using Microsoft.AspNetCore.Mvc;
using ApplicationBusiness.Services;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    ImagesService ImagesService { get; set; }

    public ImagesController(ImagesService imagesService)
    {
        ImagesService = imagesService;
    }

    public IActionResult Create(IFormFile image)
    {
        string imageUrl = ImagesService.UploadImage(image);

        return Ok(new { ImageUrl = imageUrl });
    }
}

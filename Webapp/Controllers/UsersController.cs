using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;
using Infrastructure.Exceptions;
using Webapp.Repositories;

namespace Webapp.Controllers;

public class UsersController : Controller
{
    private UsersAPI UsersAPI { get; set; }
    private ImagesAPI ImagesAPI { get; set; }

    public UsersController(UsersAPI usersApi, ImagesAPI imagesAPI)
    {
        UsersAPI = usersApi;
        ImagesAPI = imagesAPI;
    }

    public IActionResult New()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(User user, [FromForm] IFormFile? ProfileImage)
    {
        if (!ModelState.IsValid) return View("New", user);
        if (ProfileImage == null)
        {
            ViewBag.Error = "Uma imagem de perfil deve ser adicionada";
            return View("New", user);
        }

        try
        {
            string imageUrl = ImagesAPI.UploadImage(ProfileImage);
            user.ProfileImage = imageUrl;
            UsersAPI.create(user);
        }
        catch (APIErrorException ex)
        {
            ViewBag.Error = ex.Message;
            return View("New", user);
        }

        return Redirect("/Login/new");
    }
}

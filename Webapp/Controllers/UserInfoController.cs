using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;

namespace Webapp.Controllers;

[Authorize]
public class UserInfoController : AuthorizedController
{
    private UserInfoAPI UserInfoAPI { get; set; }
    private ImagesAPI ImagesAPI { get; set; }

    public UserInfoController(UserInfoAPI userInfoAPI, ImagesAPI imagesAPI)
    {
        UserInfoAPI = userInfoAPI;
        ImagesAPI = imagesAPI;
    }

    protected override void SetAPIToken()
    {
        UserInfoAPI.AddToken(SessionToken);
    }

    public ActionResult Details()
    {
        User user = UserInfoAPI.GetMyInfo();

        return View(user);
    }

    public ActionResult Edit()
    {
        User user = UserInfoAPI.GetMyInfo();
        user.Password = string.Empty;
        return View(user);
    }

    [HttpPost]
    public ActionResult Update(User user, IFormFile? ProfileImageFile)
    {
        if (!ModelState.IsValid) return View(nameof(Edit), user);

        try 
        {
            if (ProfileImageFile != null)
            {
                string imageUrl = ImagesAPI.UploadImage(ProfileImageFile);
                user.ProfileImage = imageUrl;
            }

            UserInfoAPI.Update(user);

            return RedirectToAction(nameof(Details));
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(nameof(Edit), user);
        }
    }

    public ActionResult Delete(int id)
    {
        User user = UserInfoAPI.GetMyInfo();

        return View(user);
    }

    [HttpPost]
    public ActionResult Destroy()
    {
        UserInfoAPI.DeleteMyUser();

        return Redirect("/Login/Logout");
    }
}

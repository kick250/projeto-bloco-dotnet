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

    //public ActionResult Edit(int id)
    //{
    //    return View();
    //}

    //[HttpPost]
    //public ActionResult Edit(int id, IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}

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

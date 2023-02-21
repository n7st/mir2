using Microsoft.AspNetCore.Mvc;
using Server.MirDatabase;

namespace Server.Web.Controllers;

public class AccountController : BaseController
{
    public IActionResult Index()
    {
        ViewData["Accounts"] = Envir.AccountList;

        return View();
    }
}
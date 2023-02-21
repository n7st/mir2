using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.MirEnvir;

namespace Server.Web.Controllers;

/**
 * <summary>
 * BaseController is an abstract controller used as the base for every controller in the application, with the exception
 * of ones which handle user registration and authentication.
 *
 * It ensures that every other page requires an authenticated user.
 * </summary>
 */
[Authorize]
public abstract class BaseController : Controller
{
    /**
     * <summary>
     * Envir provides access to the server's environment.
     * </summary>
     */
    public static Envir Envir => Envir.Main;
}
using AdsBoard.Domain.Entities.Identity;
using AdsBoard.Domain.Infrastructure;
using AdsBoard.WebUI.IdentityInf;
using AdsBoard.WebUI.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdsBoard.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, PhoneNumber=model.PhoneNumber, Email = model.Email };
                IdentityResult result =
                    await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("List","Ad");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Користувача не знайдено" });
            }
        }
        [Authorize]
        public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(string id, string password, CreateUserViewModel model)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.PhoneNumber = model.PhoneNumber;
                IdentityResult validPhoneNumber
                    = await UserManager.UserValidator.ValidateAsync(user);

                if (!validPhoneNumber.Succeeded)
                {
                    AddErrorsFromResult(validPhoneNumber);
                }
                user.Email = model.Email;
                IdentityResult validEmail
                    = await UserManager.UserValidator.ValidateAsync(user);

                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass
                        = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                            UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validPhoneNumber.Succeeded && validEmail.Succeeded &&  validPass == null) ||
                        (validPhoneNumber.Succeeded && validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Користувача не знайдено");
            }
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}
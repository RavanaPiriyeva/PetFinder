using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFinder.DAL;
using PetFinder.Models;
using PetFinder.Utils;
using PetFinder.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, AppDbContext context, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        //public void show(MemberRegisterViewModel memberVM)
        //{
        //    var password = MemberRegisterViewModel.Attributes.OfType<DataTypeAttribute>().FirstOrDefault(); ;
        //    if(password.data)
        //}
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByNameAsync(memberVM.RegisterUserName);

            if (user != null)
            {
                ModelState.AddModelError("UserName", "UserName already exist");
                return View();
            }

            if (_context.Users.Any(x => x.NormalizedEmail == memberVM.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "Email already exist");
                return View();
            }

            user = new AppUser
            {
                Email = memberVM.Email,
                UserName = memberVM.RegisterUserName,
                FullName = memberVM.FullName
            };

            var result = await _userManager.CreateAsync(user, memberVM.RegisterPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("index", "home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberLoginViewModel memberVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser member = await _userManager.Users.FirstOrDefaultAsync(x => !x.IsAdmin && x.NormalizedUserName == memberVM.LoginUserName.ToUpper());

            if (member == null)
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(member, memberVM.LoginPassword, memberVM.IsPersistent, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View();
            }

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Profile()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            ProfileViewModel profileVM = new ProfileViewModel
            {
                Member = new MemberUpdateViewModel
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    UserName = user.UserName
                },
               // Orders = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Book).Where(x => x.AppUserId == user.Id).ToList()
            };
            return View(profileVM);
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(MemberUpdateViewModel memberVM)
        {
            AppUser member = await _userManager.FindByNameAsync(User.Identity.Name);

            ProfileViewModel profileVM = new ProfileViewModel
            {
                Member = memberVM
            };

            if (!ModelState.IsValid)
            {
                return View(profileVM);
            }

            if (member.Email != memberVM.Email && _userManager.Users.Any(x => x.NormalizedEmail == memberVM.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "This email has already been taken");
                return View(profileVM);
            }

            if (member.UserName != memberVM.UserName && _userManager.Users.Any(x => x.NormalizedUserName == memberVM.UserName.ToUpper()))
            {
                ModelState.AddModelError("UserName", "This username has already been taken");
                return View(profileVM);
            }

            member.Email = memberVM.Email;
            member.FullName = memberVM.FullName;
            member.UserName = memberVM.UserName;

            var result = await _userManager.UpdateAsync(member);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(profileVM);
            }

            if (!string.IsNullOrWhiteSpace(memberVM.Password) && !string.IsNullOrWhiteSpace(memberVM.RepeatPassword))
            {
                if (memberVM.Password != memberVM.RepeatPassword)
                {
                    return View(profileVM);
                }

                if (!await _userManager.CheckPasswordAsync(member, memberVM.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "CurrentPassword is not correct");
                    return View(profileVM);
                }

                var passwordResult = await _userManager.ChangePasswordAsync(member, memberVM.CurrentPassword, memberVM.Password);

                if (!passwordResult.Succeeded)
                {
                    foreach (var item in passwordResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(profileVM);
                }
            }
            _context.SaveChanges();

            await _signInManager.SignInAsync(member, true);

            return View(profileVM);
        }

        public async Task<IActionResult> Forgot()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Forgot(ForgotPasswordViewModel forgotVM)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser user = await _userManager.FindByEmailAsync(forgotVM.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("resetpassword", "account", new { email = user.Email, token = token },protocol:HttpContext.Request.Scheme);
             var message = $"<a href= '{url}'>Şifrəni yenilə <a> ";
            await EmailUtil.SendEmailAsync(forgotVM.Email, "Şifrəni yenilə", message);
            return RedirectToAction("login");

        }

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordVM)
        {
            AppUser user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
            if (user == null || !(await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetPasswordVM.Token)))
                return RedirectToAction("login");


            //return View(resetPasswordVM);
            //if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(token))
            //    return BadRequest();

            //var user = await _userManager.FindByIdAsync(id);
            //if (user == null)
            //    return NotFound();

            //return View();
            return View(resetPasswordVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ResetPasswordViewModel resetPasswordVM)
        {
            if (string.IsNullOrWhiteSpace(resetPasswordVM.Password) || resetPasswordVM.Password.Length > 25)
                ModelState.AddModelError("Password", "Password is required and must be less than 26 character");

            if (!ModelState.IsValid) return View("ResetPassword", resetPasswordVM);

            AppUser user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
            if (user == null) return RedirectToAction("login");

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordVM.Token, resetPasswordVM.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View("ResetPassword", resetPasswordVM);
            }

            TempData["Success"] = "Sifreniz ugurla yenilendi!";

            return RedirectToAction("login");
        }


        public IActionResult Chat()
        {
            var users = _context.Users.Where(x => !x.IsAdmin).ToList();
            return View(users);
        }
    }
}

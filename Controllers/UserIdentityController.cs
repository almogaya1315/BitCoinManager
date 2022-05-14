using BitCoinManager.Models;
using BitCoinManager.Services;
using BitCoinManagerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BitCoinManager.Controllers
{
    public class UserIdentityController : ControllerBase
    {
        public UserIdentityController(GlobalizationHandler global, ILogger<HomeController> logger, BitCoinRepository repository, SessionHandler session)
            : base(global, logger, repository, session) { }

        public IActionResult Login(UserViewModel userVm)
        {
            var user = userVm.Model;
            var loginAttempValid = false;

            if (!userVm.Init)
            {
                if (!string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.Password))
                {
                    loginAttempValid = _repository.ValidateLogin(user);
                    if (!loginAttempValid)
                    {
                        ViewData.Add("Mesasge_Login", "Login attemp failed. EmaiL OR Password INCORRECT.");
                    }
                }
                else
                {
                    ViewData.Add("Mesasge_Login", "Login attemp failed. EmaiL OR Password EMPTY.");
                }
            }

            if (!loginAttempValid)
            {
                return View(userVm);
            }

            return RedirectToAction("MainMenu", "Orders", userVm);
        }

        public IActionResult CreateUser(UserViewModel userVm)
        {
            var user = userVm.Model;
            var creationAttempValid = false;

            if (!userVm.Init)
            {
                if (!string.IsNullOrWhiteSpace(user.Email))
                {
                    if (!Regex.IsMatch(user.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                        ViewData.Add("Message_Email", "Email not in correct format");
                }
                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    var passChars = user.Password.ToList();
                    var hasAtleastOneNumber = false;
                    var hasAtleastOneLetter = false;
                    var hasAtleastOneSymbol = false;
                    var hasAtleastEightChars = passChars.Count >= 8;
                    foreach (var c in passChars)
                    {
                        if (Char.IsNumber(c))
                        {
                            hasAtleastOneNumber = true;
                            continue;
                        }
                        if (Char.IsLetter(c))
                        {
                            hasAtleastOneLetter = true;
                            continue;
                        }
                        if (Char.IsSymbol(c))
                        {
                            hasAtleastOneSymbol = true;
                            continue;
                        }
                    }

                    if (!hasAtleastOneNumber || !hasAtleastOneLetter || !hasAtleastOneSymbol || !hasAtleastEightChars)
                    {
                        ViewData.Add("Message_Password", "Password must have atleast 8 charcters, which contain numbers, letters & symbols.");
                    }
                }

                creationAttempValid = string.IsNullOrWhiteSpace((string)ViewData["Message_Email"]) && 
                                      string.IsNullOrWhiteSpace((string)ViewData["Message_Password"]);
            }

            if (creationAttempValid)
                return RedirectToAction("MainMenu", "Orders", userVm);
            else return View(userVm);
        }
    }
}

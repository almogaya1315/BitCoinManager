using BitCoinManager.Models;
using BitCoinManager.Services;
using BitCoinManagerModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BitCoinManager.Controllers
{
    public class UserIdentityController : ControllerBase
    {
        public UserIdentityController(ILogger<HomeController> logger, BitCoinRepository repository, SessionHandler session)
            : base(logger, repository, session) { }

        public IActionResult Login(UserViewModel userVm)
        {
            try
            {
                var user = userVm.Model;
                var loginAttempValid = false;

                if (!userVm.Init)
                {
                    if (!string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.Password))
                    {
                        loginAttempValid = _repository.ValidateLogin(ref user);
                        userVm.Model.Id = user.Id;

                        if (!loginAttempValid)
                        {
                            ViewData.Add("Mesasge_Login", "Login attemp failed. EmaiL OR Password INCORRECT.");
                        }
                        else
                        {
                            _session.SetUserInCookies(JsonConvert.SerializeObject(user));
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
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in 'Login'. {e.Message}");
                ViewData.Add("Mesasge_Login", "Error in user login.");
                return View(userVm);
            }

            return RedirectToAction("MainMenu", "Orders", userVm);
        }

        public IActionResult LogOut()
        {
            try
            {
                _session.GetUserFromCookies(out UserViewModel userVm);
                return View("Login", userVm);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in 'Login'. {e.Message}");
                return RedirectToAction("Error", "Home", new { Message = e.Message });
            }
        }

        public IActionResult CreateUser(UserViewModel userVm)
        {
            try
            {
                var user = userVm.Model;
                var creationAttempValid = false;

                if (!userVm.Init)
                {
                    //if (!string.IsNullOrWhiteSpace(user.Email))
                    //{
                    //    if (!Regex.IsMatch(user.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                    //        ViewData.Add("Message_Email", "Email not in correct format");
                    //}
                    if (!string.IsNullOrWhiteSpace(user.Password))
                    {
                        var passChars = user.Password.ToList();
                        var hasAtleastOneNumber = false;
                        var hasAtleastOneLetter = false;
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
                        }

                        if (!hasAtleastOneNumber || !hasAtleastOneLetter || !hasAtleastEightChars)
                        {
                            ViewData.Add("Message_Password", "Password must have atleast 8 charcters, which contain numbers & letters.");
                        }
                    }

                    creationAttempValid = string.IsNullOrWhiteSpace((string)ViewData["Message_Email"]) &&
                                          string.IsNullOrWhiteSpace((string)ViewData["Message_Password"]);

                    if (creationAttempValid)
                        user.Id = _repository.CreateUser(user);
                }

                if (creationAttempValid)
                    return RedirectToAction("MainMenu", "Orders", userVm);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in 'CreateUser'. {e.Message}");
                ViewData.Add("Mesasge_Login", "Error in user creation.");
            }

            return View(userVm);
        }
    }
}

using MVC_SHOP.Common;
using MVC_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SHOP.Controllers
{
    public class AccountController : Controller
    {
        private LearnContext db = new LearnContext();
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(LoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (!isExist(userLogin.UserName,userLogin.Password))
                {
                    ViewBag.Error = "Tài khoản không tồn tại trong hệ thống.";
                    return View();
                }else
                {
                    Session[Notice.UserSession] = userLogin;
                    var user = (LoginViewModel)Session[Notice.UserSession];
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (isAccountExist(register.UserName)){
                    ViewBag.Error = "Tài khoản đã tồn tại!";
                    return View(register);
                }
                else
                {
                    var User = new User
                    {
                        Account = register.UserName,
                        Password = Encryptor.MD5Hash(register.Password),
                        ConfirmPassword=Encryptor.MD5Hash(register.ConfirmPassword),
                        Email=register.Email,
                        PhoneNo=register.PhoneNo,
                        Createdby=Notice.Customer,
                        CreatedDate=DateTime.Now
                    }; 

                    try
                    {
                        db.Users.Add(User);
                        db.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        var error = ex.ToString();
                    }

                    ViewBag.Error = "Xin chúc mừng bạn đã đăng ký tài khoản thành công! <a href=\"/account/login\" style=\"color:blue;text-decoration: underline;\">Click tại đây</a> để đăng nhập";
                    return View();
                }
            }
        }

        public ActionResult LogOut()
        {
            Session[Notice.UserSession] = null;
            return RedirectToAction("Index","Home");

        }

        private bool isAccountExist(string userName)
        { 
            return (db.Users.Where(x => x.Account == userName).ToList().Count() == 0)?false:true;
        }

        private bool isExist(string userName,string password)
        {
            string hashPass = Encryptor.MD5Hash(password);
            return db.Users.Where(x => x.Account == userName && x.Password == hashPass).Count() > 0 ? true : false;
        }
    }
}
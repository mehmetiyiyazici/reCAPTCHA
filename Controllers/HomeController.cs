using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DenemePostMan.Models;
using RestSharp;

namespace DenemePostMan.Controllers;

public class HomeController : Controller
{
     public IActionResult Index()
        {
            var client = new RestClient("https://dummyjson.com/");
            var request = new RestRequest("posts");
            var response = client.Get<PostsList>(request);
            return View(response);
        }
        
        public IActionResult AddPost()
        {
            return View(new Post());
        }
        [HttpPost]
        public IActionResult AddPost(Post model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var captchaToken = Request.Form["g-recaptcha-response"];

            if (!VerifyCaptcha(captchaToken))
            {
                ViewBag.CaptchaError = true;
                return View(model);
            }

            var client = new RestClient("https://dummyjson.com/");
            var request = new RestRequest("posts/add", Method.Post);
            request.AddJsonBody(model);
            var response = client.Post<Post>(request);
            return Json(response);
        }

        public IActionResult Editor()
        {
            var client = new RestClient("https://dummyjson.com/");
            var request = new RestRequest($"posts");
            var response = client.Get<PostsList>(request);
            return View(response);
        }
       
        public IActionResult DeletePost(int id)
        {
            var client = new RestClient("https://dummyjson.com/");
            var request = new RestRequest($"posts/{id}");
            var response = client.Delete<PostsList>(request);
            return Ok(new
            {
                msg="Basariyla silindi",
            }) ;
        }

        public bool VerifyCaptcha(string captchaToken)
        {
            //CaptchaResponse
            var client = new RestClient("https://www.google.com/recaptcha");
            var request = new RestRequest("api/siteverify", Method.Post);
            request.AddParameter("secret", "6LdQhTgqAAAAAMKweEV-LGbXjfn6SVZihuS3Jm5B");
            request.AddParameter("response", captchaToken);

            var response = client.Execute<CaptchaResponse>(request);

            if (response.Data.Success)
            {
                return true;
            }
            return false;
        }
}
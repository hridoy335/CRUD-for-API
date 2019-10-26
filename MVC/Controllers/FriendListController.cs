using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC.Models;


namespace MVC.Controllers
{
    public class FriendListController : Controller
    {
        // GET: FriendList
        public ActionResult Index()
        {
            IEnumerable<MVCFriendModel> friendlist;
            HttpResponseMessage response = GlobalVariable.webapiclient.GetAsync("Web").Result;
            friendlist = response.Content.ReadAsAsync<IEnumerable<MVCFriendModel>>().Result;
            return View(friendlist);
        }

        [HttpGet]
        public ActionResult AddFriend(int id=0)
        {
            if (id == 0)
            {
                return View(new MVCFriendModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariable.webapiclient.GetAsync("Web/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<MVCFriendModel>().Result);
            }
            
        }

        [HttpPost]
        public ActionResult AddFriend(MVCFriendModel fri)
        {
            if (fri.InfoID == 0)
            {
                HttpResponseMessage response = GlobalVariable.webapiclient.PostAsJsonAsync("Web", fri).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariable.webapiclient.PutAsJsonAsync("Web/"+fri.InfoID, fri).Result;
            }
            

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteFriend(int?id)
        {
            HttpResponseMessage response = GlobalVariable.webapiclient.GetAsync("Web/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<MVCFriendModel>().Result);
        }

        [HttpPost]
        public ActionResult DeleteFriend(int id)
        {
            HttpResponseMessage response = GlobalVariable.webapiclient.DeleteAsync("Web/" +id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}
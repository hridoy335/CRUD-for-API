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
    }
}
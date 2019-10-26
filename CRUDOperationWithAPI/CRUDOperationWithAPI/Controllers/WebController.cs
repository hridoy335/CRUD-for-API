using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CRUDOperationWithAPI.Models;

namespace CRUDOperationWithAPI.Controllers
{
    public class WebController : ApiController
    {
        private FriendsInfoEntities db = new FriendsInfoEntities();

        // GET: api/Web
        public IQueryable<FriendInfo> GetFriendInfoes()
        {
            return db.FriendInfoes;
        }

        // GET: api/Web/5
        [ResponseType(typeof(FriendInfo))]
        public IHttpActionResult GetFriendInfo(int id)
        {
            FriendInfo friendInfo = db.FriendInfoes.Find(id);
            if (friendInfo == null)
            {
                return NotFound();
            }

            return Ok(friendInfo);
        }

        // PUT: api/Web/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFriendInfo(int id, FriendInfo friendInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friendInfo.InfoID)
            {
                return BadRequest();
            }

            db.Entry(friendInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Web
        [ResponseType(typeof(FriendInfo))]
        public IHttpActionResult PostFriendInfo(FriendInfo friendInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FriendInfoes.Add(friendInfo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = friendInfo.InfoID }, friendInfo);
        }

        // DELETE: api/Web/5
        [ResponseType(typeof(FriendInfo))]
        public IHttpActionResult DeleteFriendInfo(int id)
        {
            FriendInfo friendInfo = db.FriendInfoes.Find(id);
            if (friendInfo == null)
            {
                return NotFound();
            }

            db.FriendInfoes.Remove(friendInfo);
            db.SaveChanges();

            return Ok(friendInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FriendInfoExists(int id)
        {
            return db.FriendInfoes.Count(e => e.InfoID == id) > 0;
        }
    }
}
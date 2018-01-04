using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRAWaitList.DAL;
using PRAWaitList.Models;
using System.Net.Mail;

namespace PRAWaitList.Controllers
{
    [Authorize]
    public class EmailControlModelsController : Controller
    {
        private PRAWaitListContext db = new PRAWaitListContext();

        // GET: EmailControlModels
        public ActionResult Index()
        {
            return View(db.EmailControls.ToList());
        }

        // GET: EmailControlModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailControlModel emailControlModel = db.EmailControls.Find(id);
            if (emailControlModel == null)
            {
                return HttpNotFound();
            }
            return View(emailControlModel);
        }

        // GET: EmailControlModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailControlModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FromAddress,SMTPDeliveryMethod,SMTPHost,SMTPPort,SMTPUser,SMTPPassword,SMTPEnableSSL,SMTPSendLimit,SMTPisActive")] EmailControlModel emailControlModel)
        {
            if (ModelState.IsValid)
            {
                db.EmailControls.Add(emailControlModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emailControlModel);
        }

        // GET: EmailControlModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailControlModel emailControlModel = db.EmailControls.Find(id);
            if (emailControlModel == null)
            {
                return HttpNotFound();
            }
            return View(emailControlModel);
        }

        // POST: EmailControlModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FromAddress,SMTPDeliveryMethod,SMTPHost,SMTPPort,SMTPUser,SMTPPassword,SMTPEnableSSL,SMTPSendLimit,SMTPisActive")] EmailControlModel emailControlModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailControlModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emailControlModel);
        }

        // GET: EmailControlModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailControlModel emailControlModel = db.EmailControls.Find(id);
            if (emailControlModel == null)
            {
                return HttpNotFound();
            }
            return View(emailControlModel);
        }

        // POST: EmailControlModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailControlModel emailControlModel = db.EmailControls.Find(id);
            db.EmailControls.Remove(emailControlModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: EmailControlModels/EmailTest
        public ActionResult EmailTest()
        {
            try
            {
                var body = "This is a test email.";
                var message = new MailMessage();
                message.To.Add(new MailAddress("engel.j@gmail.com")); //replace with valid value
                message.Subject = "Test Email.";
                message.Body = body;
                message.IsBodyHtml = true;
                Utility.SendMail(message);
                TempData["alertMessage"] = "Email Queued!";
            }
            catch (Exception e)
            {
                TempData["alertMessage"] = "Error Queueing Email: " + e.Message;
            }
            return RedirectToAction("index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

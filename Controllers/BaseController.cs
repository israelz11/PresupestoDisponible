using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresupDisponible.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult ThrowJSONError(Exception e)
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            Response.StatusCode = 0;
            return Json(new { Message = e.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}
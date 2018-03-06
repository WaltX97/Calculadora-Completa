using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

		// GET: Home
		[HttpPost]
		public ActionResult Index(string bt)
		{
		//algoritmo da calculadora
			return View();
		}

	}
}
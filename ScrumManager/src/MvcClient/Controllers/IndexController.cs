using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcClient.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        /// <summary>
        /// Index which redirects uers to right index by role
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Developer's index
        /// </summary>
        /// <returns></returns>
        public IActionResult Developer()
        {
            return View();
        }

        /// <summary>
        /// Scrum master's index
        /// </summary>
        /// <returns></returns>
        public IActionResult ScrumMaster()
        {
            return View();
        }

        /// <summary>
        /// Product owner's index
        /// </summary>
        /// <returns></returns>
        public IActionResult ProductOwner()
        {
            return View();
        }

    }
}

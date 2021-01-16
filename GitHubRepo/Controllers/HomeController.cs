using GitHubRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubRepo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult RemoveBM(string Name)
        {
            int repNum = Int32.Parse(Name);
            List<Repo> repoListBM = HttpContext.Session.GetComplexData<List<Repo>>("repoListBM");
            Repo repoItem = repoListBM.FirstOrDefault(itm => itm.RepoId == repNum);
            if (repoItem != null)
            {
                repoListBM.Remove(repoItem);
                HttpContext.Session.SetComplexData("repoListBM", repoListBM);
            }
            return View("BookMarks", repoListBM);
        }
        public IActionResult Index()
        {
            return View();
        }

        


        public IActionResult Results()
        {
            List<Repo> repoList = HttpContext.Session.GetComplexData<List<Repo>>("repoList");
            return View("~/Views/Repo/GitHubResult.cshtml", repoList);
              
        }

        public IActionResult BookMarks()
        {
            List<Repo> repoListBM = HttpContext.Session.GetComplexData<List<Repo>>("repoListBM");
            return View(repoListBM);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

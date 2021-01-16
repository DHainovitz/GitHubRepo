using GitHubRepo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubRepo.Controllers
{
    public class RepoController : Controller
    {

        List<Repo> repoList = new List<Repo>();
        public IActionResult SaveBM(string Name)
        {

            //save this repo (by id to session)
            List<Repo> repoList = HttpContext.Session.GetComplexData<List<Repo>>("repoList");
            List<Repo> repoListBM = HttpContext.Session.GetComplexData<List<Repo>>("repoListBM");

            int repNum = Int32.Parse(Name);
            //bool has = repoList.Any(rep => rep.RepoId == Int32.Parse(Name));

            //var addressList = addr.Where(item => ids.Contains(item.Id)).Select(a => a).ToList();

            // var newlist = repoList.Where(item => repoList.Contains(item.RepoId==55)).Select(a => a).ToList();
            Repo repoItem = repoList.FirstOrDefault(itm => itm.RepoId == repNum);
            if (repoItem != null)
            {
                //check also that item is not already bookmarked
                if(repoListBM.FirstOrDefault(itm => itm.RepoId == repNum)==null)
                {
                    repoListBM.Add(repoItem);
                    HttpContext.Session.SetComplexData("repoListBM", repoListBM);
                }
            }


            return new EmptyResult();
        }

            //  /Repo/GitHubResult/{searchPhrase}
            [Route("Repo/GitHubResult/{searchPhrase?}")]
        public async Task<IActionResult> GitHubResult(string searchPhrase)
        {
            List<Item> repoOrigList = new List<Item>();
          
           

            using (var httpClient = new HttpClient())
            {
                //
               // User - Agent: Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 87.0.4280.88 Safari / 537.36
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                using (var response = await httpClient.GetAsync("https://api.github.com/search/repositories?q=" + searchPhrase))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    // jsonObject ourlisting = JsonConvert.DeserializeObject<jsonObject>(strJSON);
                    Rootobject ourlisting = JsonConvert.DeserializeObject<Rootobject>(apiResponse);
                    repoOrigList =  ourlisting.items.ToList();


                    foreach (var item in repoOrigList)
                    {
                        Repo rep = new Repo();
                        rep.RepoId = item.id;
                        rep.RepoName = item.name;
                        rep.AvatarURL = item.owner.avatar_url;
                        rep.IsBookMarked = false;

                        repoList.Add(rep);

                        //var id = item.
                    }

                    //save repoList to Session
                    HttpContext.Session.SetComplexData("repoList", repoList);
                    //repoListBMs only need to be initiated  once!
                    if (HttpContext.Session.GetComplexData<List<Repo>>("repoListBM") == null)
                    {
                        HttpContext.Session.SetComplexData("repoListBM", new List<Item>());
                    }

                }
            }

            return View(repoList);
        }

        public IActionResult GetResult(string searchPhrase)
        {
            //LoggedUserVM user = GetUserDataById(model.userId);
            //rech out ti github and get api


            //Create Session with complex object   
            HttpContext.Session.SetComplexData("loggerUser", "user");

            return Json("");  //LIST OF RESULT          _   gitHubRepository.List()

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

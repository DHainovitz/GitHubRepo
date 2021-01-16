using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubRepo.Models
{
    public class Repo
    {
        public int RepoId { get; set; }
        public string RepoName { get; set; }
        public string AvatarURL { get; set; }
        public bool IsBookMarked { get; set; }
       
    }
}


using System.Collections.Generic;

namespace CodeMash.Model
{
    public class SpeakerModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }      
        public string GravatarUrl { get; set; }
     
    }

    public class SpeakerDetailModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string GravatarUrl { get; set; }
        public string TwitterLink { get; set; }
        public string GitHubLink { get; set; }
        public string LinkedInProfile { get; set; }
        public string BlogUrl { get; set; }
        public string[] SessionIds { get; set; } 

    }

}

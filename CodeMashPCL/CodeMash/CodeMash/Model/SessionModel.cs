
using System.Collections.Generic;
using System.Linq;


namespace CodeMash.Model
{
    public class SessionModel
    {
        public int Id { get; set; }
        public string SessionTime { get; set; }
        public string SessionStartTime { get; set; }
        public string SessionEndTime { get; set; }
        public string Room { get; set; }
        public string[] Rooms { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string SessionType { get; set; }
        public string[] Tags { get; set; }
        public string Category { get; set; }
        public List<SpeakerModel> Speakers { get; set; }
        public string SpeakerName => Speakers.FirstOrDefault()?.FirstName + "-" + Speakers.FirstOrDefault()?.LastName;
        public bool isFavorite { get; set; }
        public string FavText => (isFavorite) ? "- Fav" : "+ Fav";
        public string FavImage => (isFavorite) ? "goldstar_medium" : "goldstar_gray_medium.png";
        public string StartTimeFormated => string.IsNullOrEmpty(SessionStartTime)
            ? "" : $"{SessionStartTime:M/dd h:mm tt, ddd}";
    }

    public class FavSessionModel
    {      
          public int SessionId { get; set; }
    }
}

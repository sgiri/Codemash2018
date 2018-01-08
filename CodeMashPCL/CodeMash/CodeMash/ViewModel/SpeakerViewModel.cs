using CodeMash.Model;
using System.Collections.ObjectModel;


namespace CodeMash.ViewModel
{
    public class SpeakerViewModel
    {
        public ObservableCollection<SpeakerModel> Speakers { get; set; } =
               new ObservableCollection<SpeakerModel>();

        public SpeakerViewModel()
        {
            var Items = new ObservableCollection<SpeakerModel>
             {
                 new SpeakerModel { Id = "1", FirstName = "Jon",LastName="Doe", GravatarUrl= "github.com"},
                 new SpeakerModel { Id = "1", FirstName = "Mary",LastName="lee", GravatarUrl= "linkedin.com"},
                 new SpeakerModel { Id = "1", FirstName = "Josh",LastName="Kin", GravatarUrl= "overflow.com"}           
            };
            Speakers = Items;

        }
    }
}

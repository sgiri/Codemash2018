using CodeMash.Model;
using System.Collections.ObjectModel;


namespace CodeMash.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<MainPageModel> MainItems { get; set; } =
               new ObservableCollection<MainPageModel>();

        public MainPageViewModel()
        {
            var Items = new ObservableCollection<MainPageModel>
             {
                 new MainPageModel {Display = "Sessions", Id = 1},
                 new MainPageModel {Display = "Speakers", Id = 2},
                 new MainPageModel {Display = "Twitter SignIn", Id = 3},
                 new MainPageModel {Display = "Favorites", Id = 4},
                new MainPageModel {Display = "Google SignIn", Id = 5},
                new MainPageModel {Display = "Event Map", Id = 6}
                 
            };

            MainItems = Items;

        }
    }
}

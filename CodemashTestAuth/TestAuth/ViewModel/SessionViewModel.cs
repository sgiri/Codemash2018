using TestAuth.Model;
using System.Collections.ObjectModel;


namespace TestAuth.ViewModel
{
    public class SessionViewModel
    {
        public ObservableCollection<SessionModel> SessionList { get; set; } =
               new ObservableCollection<SessionModel>();

        public SessionViewModel()
        {
           

        }
    }
}

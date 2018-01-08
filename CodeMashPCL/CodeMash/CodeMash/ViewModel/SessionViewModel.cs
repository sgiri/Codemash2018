using CodeMash.Model;
using System.Collections.ObjectModel;


namespace CodeMash.ViewModel
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


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAuth.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionTab : TabbedPage
    {
        public SessionTab()
        {
            InitializeComponent();
            Children.Add(new SessionsList(App.precompilerSessionList, "Pre-Compiler"));
            Children.Add(new SessionsList(App.generalSessionList, "General Session"));
            Children.Add(new SessionsList(App.kidsSessionList, "Kidz Mash"));
        }
    }
}


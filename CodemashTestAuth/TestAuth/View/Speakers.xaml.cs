using TestAuth.Model;
using TestAuth.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAuth.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Speakers : ContentPage
    {
        public ObservableCollection<SpeakerDetailModel> speakerList { get; set; }
        public Speakers()
        {
            try
            {
                InitializeComponent();
                speakerList = App.allspeakerDetailList;
                lvSpeakers.ItemsSource = speakerList;
            }
            catch (Exception ex)
            {
                string h = ex.Message;
            }
        }

        private void lvSpeakers_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            var speaker = (SpeakerDetailModel)e.SelectedItem;
            Navigation.PushAsync(new NavigationPage(new TestAuth.View.SpeakerDetail(speaker)));
            lvSpeakers.SelectedItem = null;
        }
    }
}

using CodeMash.Model;
using CodeMash.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CodeMash
{ 
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = new MainPageViewModel();
            }
            catch(Exception ex)
            {
                string h = ex.Message;
            }
        }

        private void LvMain_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            var main = (MainPageModel)e.SelectedItem;
            MainPageModel p = e.SelectedItem as MainPageModel;
            //DisplayAlert("Selected", p.Display, "OK");

            switch (main.Id)
            {
                case 1:
                    Navigation.PushAsync(new CodeMash.View.SessionTab());
                    break;
                case 2:
                    Navigation.PushAsync(new CodeMash.View.Speakers());
                    break;
                case 3:
                   // Twitter sign in
                    break;
                case 4:
                    Navigation.PushAsync(new CodeMash.View.FavoritesTab());
                    break;
                case 5:
                   //    
                    break;
                default:
                    break;//do nothing for now
            }

            lvMain.SelectedItem = null;
        }
    }
}

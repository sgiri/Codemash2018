using TestAuth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAuth.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionDetail : ContentPage
    {
        private SessionModel Session;
        public SessionDetail(SessionModel session)
        {
            InitializeComponent();
            Session = session;
            this.Title = session.Title;
            lblSpeaker.Text = session.Speakers.FirstOrDefault()?.FirstName + ' ' +
                              session.Speakers.FirstOrDefault()?.LastName;
            lblTechnology.Text = session.Category;
            lblSessionType.Text = session.SessionType;
            lblDateTime.Text = session.StartTimeFormated;
            lblRoom.Text = session.Room;
            lblDescription.Text = session.Abstract;
        }

        private void LblSpeaker_OnFocused(object sender, FocusEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}


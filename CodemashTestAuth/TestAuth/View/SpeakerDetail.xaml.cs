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
    public partial class SpeakerDetail : ContentPage
    {
        private SpeakerDetailModel Speaker { get; set; }
        public SpeakerDetail(SpeakerDetailModel speaker)
        {
            InitializeComponent();

            Speaker = speaker;

            lblSpeaker.Text = speaker.FirstName + ' ' +
                              speaker.LastName;
            lblGravatarUrl.Text = speaker.GravatarUrl;
            lblTwitterLink.Text = speaker.TwitterLink;
            lblGitHubLink.Text = speaker.GitHubLink;
            lblLinkedInProfile.Text = speaker.LinkedInProfile;
            lblBiography.Text = speaker.Biography;
        }
    }
}
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
    public partial class TwitterFeed : ContentPage
    {
        public TwitterFeed()
        {
            InitializeComponent();

            //Device.OpenUri(new Uri("https://twitter.com/#codemash"));
            Device.OpenUri(new Uri("https://twitter.com"));
        }
    }
}

﻿using System.Collections.ObjectModel;
namespace TestAuth.ViewModel
{
    public class SpeakerDetailViewModel
    {
        public ObservableCollection<SpeakerDetailViewModel> Speakers { get; set; } =
               new ObservableCollection<SpeakerDetailViewModel>();

        public SpeakerDetailViewModel()
        {
            var Items = new ObservableCollection<SpeakerDetailViewModel>
            {
                 
            };
            Speakers = Items;

        }
    }
}

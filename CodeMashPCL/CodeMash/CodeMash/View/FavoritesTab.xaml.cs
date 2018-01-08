﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodeMash.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesTab : TabbedPage
    {
        public FavoritesTab()
        {
            InitializeComponent();
            Children.Add(new FavoritesFuture());
            Children.Add(new FavoritesPast());
        }
    }
}

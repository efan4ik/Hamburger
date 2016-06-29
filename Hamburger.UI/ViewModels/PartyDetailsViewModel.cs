using Hamburger.UI.Model;
using Hamburger.UI.View;
using Hamburger.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Hamburger.UI.ViewModels
{
    internal class PartyDetailsViewModel
    {
        public PartyDetailsViewModel()
        {
        }
        Dictionary<string, PartyImageView> nameToPartyImageView = new Dictionary<string, PartyImageView>();
        internal PartyImageView DisplayPics(string partyName)
        {
            if (!nameToPartyImageView.ContainsKey(partyName))
            {
                PartyImageView partyImageView = new PartyImageView();
                ((PartyImageViewModel) partyImageView.DataContext).partyImageModel = new PartyImageModel(partyName);
                nameToPartyImageView.Add(partyName, partyImageView);
            }
            return nameToPartyImageView[partyName];
        }
    }
}
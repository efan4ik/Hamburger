using Hamburger.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hamburger.UI.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PartyImageView : Page
    {
        PartyImageViewModel partyImageViewModel = new PartyImageViewModel();
        bool alreadyLoaded = false;
        public PartyImageView ()
        {
            this.InitializeComponent();
            this.DataContext = partyImageViewModel;
        }
        private void SetFirstPic(object sender, RoutedEventArgs e)
        {
            if (!alreadyLoaded)
            {
                ((PartyImageViewModel)this.DataContext).SetFirstPic((FlipView)sender);
            }         
        }
        private void LoadLocationBar(object sender, RoutedEventArgs e)
        {
            if (!alreadyLoaded)
            {
                ((PartyImageViewModel)this.DataContext).LoadLocationBar((StackPanel)sender);
                alreadyLoaded = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
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
using Hamburger.UI.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hamburger.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PartyDetailsView : Page
    {
        public PartyDetailsView()
        {
            this.InitializeComponent();
            this.DataContext = new PartyDetailsViewModel();
        }

        public void DisplayPics(string partyName)
        {
            this.Content = ((PartyDetailsViewModel)DataContext).DisplayPics(partyName);//TODO switch to command directly to viewModel
        }
    }
}

using PartySelectionPanel.ViewModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace PartySelectionPanel.View
{
    public sealed partial class PartyPanelView : Page
    {
        public PartyPanelViewModel Vm
        {
            get
            {
                return (PartyPanelViewModel)DataContext;
            }
        }
        public PartyPanelView()
        {
            this.DataContext = new PartyPanelViewModel();
            this.InitializeComponent();
        }

        private void ShowPartyOnMap(object sender, RoutedEventArgs e)
        {
            Vm.ShowPartyOnMap(sender, e);//TODO - switch to command directly to ViewModel
        }
    }
}

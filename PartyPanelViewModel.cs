using PartySelectionPanel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace PartySelectionPanel.ViewModel
{
    public class PartyPanelViewModel
    {
        private int margin = 1;
        private ObservableCollection<PartyModel> _partyModels = new ObservableCollection<PartyModel>();
        public ObservableCollection<PartyModel> partyModels
        {
            get
            {
                return _partyModels;
            }
            set
            {
                _partyModels = value;
            }
        }
        private Dictionary<string, bool> _isPartySelected = new Dictionary<string, bool>();
        public Dictionary<string, bool> isPartySelected
        {
            get
            {
                return _isPartySelected;
            }
            set
            {
                _isPartySelected = value;
            }
        }
        Dictionary<string, PartyModel> partyNameToModel = new Dictionary<string, PartyModel>();
        public PartyPanelViewModel()
        {
            LoadPartyModels();
        }

        public void ShowPartyOnMap(object sender, RoutedEventArgs e)
        {
            isPartySelected[((Button)sender).Tag.ToString()] = !isPartySelected[((Button)sender).Tag.ToString()];
            ToggleButtonBackground((Button)sender);
            var minX = 180;
            var maxX = -180;
            var minY = 90;
            var maxY = -90;
            foreach(PartyModel party in partyModels)
            {
                if (!isPartySelected[party.name])
                {
                    continue;
                }
                if (party.x < minX)
                {
                    minX = party.x;
                }
                if(party.x > maxX)
                {
                    maxX = party.x;
                }
                if(party.y < minY)
                {
                    minY = party.y;
                }
                if(party.y > maxY)
                {
                    maxY = party.y;
                }
            }
            Debug.WriteLine((minX - margin).ToString() + "," + (minY - margin).ToString() + "," + (maxX + margin).ToString() + "," + (maxY + margin).ToString());
        }
        
        private void ToggleButtonBackground(Button button)
        {
            if (isPartySelected[button.Tag.ToString()])
            {
                button.Background = new SolidColorBrush(Colors.Blue);
            }
            else
            {
                button.Background = new SolidColorBrush(Colors.Gray);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void LoadPartyModels()
        {
            PartyModel debug = new PartyModel("Party1", 31, 32);
            partyModels.Add(new PartyModel("Party1", 31, 32 ));
            partyModels.Add(new PartyModel("Party2", 32, 33));
            partyModels.Add(new PartyModel("Party3", 33, 32));
            partyModels.Add(new PartyModel("Party4", 32, 31));
            foreach(PartyModel party in partyModels)
            {
                partyNameToModel.Add(party.name, party);
                isPartySelected.Add(party.name, false);
            }
        }
    }
}

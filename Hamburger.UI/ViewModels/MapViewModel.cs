using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.UI.ViewModels
{
    public class MapViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        private bool _isToolboxVisible = false;
        public bool IsToolboxVisible
        {
            get
            {
                return _isToolboxVisible;
            }
            set
            {
                _isToolboxVisible = value;
                RaisePropertyChanged("IsToolboxVisible");
            }
        }

        public CommandHandler MyCommand
        {
            get;
            private set;
        }

        public MapViewModel()
        {
            MyCommand = new CommandHandler(ExecuteMyCommand);

        }

        private void ExecuteMyCommand()
        {
            IsToolboxVisible = !IsToolboxVisible;
        }
    }
}

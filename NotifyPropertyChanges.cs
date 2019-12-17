using System;
using System.ComponentModel;

namespace Polaris_Charactergenerator
{
    public abstract class NotifyPropertyChanges : INotifyPropertyChanged
    {
        public Action NotifyCallback { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

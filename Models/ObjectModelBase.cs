using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_MVVM_App_Shell.Models
{
    public class ObjectModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

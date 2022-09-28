using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.ViewModels
{
    public partial  class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy=false;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;
    }
}

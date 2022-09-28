using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Helpers
{
    internal class MyFocusEventArgs:EventArgs
    {
        public bool IsFocused { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MaUI.Helpers
{
    public class KeyBoardAppearEventArgs:EventArgs
    {
        public float KeyboardSize { get; set; }

        public KeyBoardAppearEventArgs()
        {
        }
    }
}

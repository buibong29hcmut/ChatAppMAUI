﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client.Helperss
{
    public class ScrollToItemEventArgs<T> : EventArgs
    {
        public object Item { get; set; }
        public T? Index { get; set; }
    }
    public class ScrollToMessageEventArges: ScrollToItemEventArgs<Guid>
    {

    }
}
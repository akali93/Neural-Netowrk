﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    internal interface Receiver : Identifiable
    {
        void Receive(Transmitter t);
    }
}

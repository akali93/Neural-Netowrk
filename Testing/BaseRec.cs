using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NN;

namespace Testing
{
    class BaseRec : Output
    {
        public override void Receive()
        {
            Console.WriteLine(this.Value);
        }
    }
}

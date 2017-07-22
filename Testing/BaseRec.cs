using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    class BaseRec : Output
    {
        public override void Receive()
        {
            Console.WriteLine(this.Value);
        }

        public override string ToString()
        {
            return "ID: " + ID;
        }
    }
}

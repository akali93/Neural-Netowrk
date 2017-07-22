using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    public class LearningSet
    {
        private Dictionary<int, double> GivenInputs { get; set; }
        private Dictionary<int, double> ExpectedOutputs { get; set; }

        public LearningSet(ICollection<Transmitter> inputs, ICollection<Receiver> outputs)
        {
            foreach (Transmitter input in inputs)
                GivenInputs.Add(input.ID, 0);
            foreach (Receiver output in outputs)
                ExpectedOutputs.Add(output.ID, 0);
        }

        public bool GiveInput(Transmitter input, double value)
        {
            if (!GivenInputs.ContainsKey(input.ID))
                return false;
            GivenInputs[input.ID] = value;
            return true;
        }

        public bool ExpectOutput(Receiver output, double value)
        {
            if (!ExpectedOutputs.ContainsKey(output.ID))
                return false;
            ExpectedOutputs[output.ID] = value;
            return true;
        }
    }
}

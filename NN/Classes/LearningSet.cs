using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    public class LearningSet
    {
        internal Dictionary<int, double> GivenInputs { get; set; }
        internal Dictionary<int, double> ExpectedOutputs { get; set; }

        internal LearningSet(ICollection<Input> inputs, ICollection<Output> outputs)
        {
            GivenInputs = new Dictionary<int, double>();
            ExpectedOutputs = new Dictionary<int, double>();
            foreach (Input input in inputs)
                GivenInputs.Add(input.ID, 0);
            foreach (Output output in outputs)
                ExpectedOutputs.Add(output.ID, 0);
        }

        public bool GiveInput(Input input, double value)
        {
            if (!GivenInputs.ContainsKey(input.ID))
                return false;
            GivenInputs[input.ID] = value;
            return true;
        }

        public bool ExpectOutput(Output output, double value)
        {
            if (!ExpectedOutputs.ContainsKey(output.ID))
                return false;
            ExpectedOutputs[output.ID] = value;
            return true;
        }

    }
}

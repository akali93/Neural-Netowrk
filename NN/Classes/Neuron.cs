using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    class Neuron : Identifiable, ITransmitter, IReceiver
    {
        /// <summary>
        /// The current value of this neuron.
        /// </summary>
        public double Value { get; private set; }
        /// <summary>
        /// The list of transmitters this neuron receives values from.
        /// </summary>
        private List<ITransmitter> Inputs { get; set; }
        /// <summary>
        /// The list of receivers this neuron sends values to.
        /// </summary>
        private List<IReceiver> Outputs { get; set; }
        /// <summary>
        /// Used to calculate and som the data received from the inputs.
        /// </summary>
        private Dictionary<int, double> Weights { get; set; }
        /// <summary>
        /// Used to count how many of the inputs already sent values.
        /// </summary>
        private int RecieveCounter;

        public Neuron()
        {
            RecieveCounter = 0;
            Inputs = new List<ITransmitter>();
            Outputs = new List<IReceiver>();
            Weights = new Dictionary<int, double>();
        }

        public bool AddInput(ITransmitter input, double weight)
        {
            if (InputExists(input))
                return false;
            Inputs.Add(input);
            return UpdateWeight(input, weight);
        }

        public bool AddOutput(IReceiver output)
        {
            if (OutputExists(output))
                return false;
            Outputs.Add(output);
            return true;
        }

        public bool UpdateWeight(ITransmitter input, double newWeight)
        {
            if (!InputExists(input))
                return false;
            Weights[input.ID] = newWeight;
            return true;
        }

        public void AutoRecieve()
        {
            foreach (ITransmitter input in Inputs)
                Receive(input);
        }

        public void Receive(ITransmitter t)
        {
            RecieveCounter++;
            Value += t.Value * Weights[t.ID];
            if (RecieveCounter >= Inputs.Count)
                Transmit();
        }

        public void Transmit()
        {
            RecieveCounter = 0;
            foreach (IReceiver output in Outputs)
                output.Receive(this);
            this.Value = 0;
        }

        public bool InputExists(ITransmitter input)
        {
            return Inputs.Contains(input);
        }

        public bool OutputExists(IReceiver output)
        {
            return Outputs.Contains(output);
        }

    }
}

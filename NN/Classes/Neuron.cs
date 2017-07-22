using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    class Neuron : Transmitter, Receiver
    {
        public int ID { get; set; }
        public double Value { get; set; }
        
        private List<Transmitter> Inputs { get; set; }
        private List<Receiver> Outputs { get; set; }
        private Dictionary<int, double> Weights { get; set; }
        private int RecieveCounter;

        public Neuron(int id)
        {
            ID = id;
            RecieveCounter = 0;
            Inputs = new List<Transmitter>();
            Outputs = new List<Receiver>();
            Weights = new Dictionary<int, double>();
        }

        public bool AddInput(Transmitter input, double weight)
        {
            if (InputExists(input))
                return false;
            Inputs.Add(input);
            return UpdateWeight(input, weight);
        }

        public bool AddOutput(Receiver output)
        {
            if (OutputExists(output))
                return false;
            Outputs.Add(output);
            return true;
        }

        public bool UpdateWeight(Transmitter input, double newWeight)
        {
            if (!InputExists(input))
                return false;
            Weights[input.ID] = newWeight;
            return true;
        }

        public void AutoRecieve()
        {
            foreach (Transmitter input in Inputs)
                Receive(input);
        }

        public void Receive(Transmitter t)
        {
            RecieveCounter++;
            Value += t.Value * Weights[t.ID];
            if (RecieveCounter >= Inputs.Count)
                Transmit();
        }

        public void Transmit()
        {
            RecieveCounter = 0;
            foreach (Receiver output in Outputs)
                output.Receive(this);
            this.Value = 0;
        }

        public bool InputExists(Transmitter input)
        {
            return Inputs.Contains(input);
        }

        public bool OutputExists(Receiver output)
        {
            return Outputs.Contains(output);
        }

        public override bool Equals(object obj)
        {
            if (obj is Identifiable)
                return ID == ((Identifiable)obj).ID;
            return false;
        }

        public override string ToString()
        {
            return "ID: " + ID + ", Value: " + Value;
        }
    }
}

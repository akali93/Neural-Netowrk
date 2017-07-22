using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    public class Neuron : Transmitter, Receiver
    {
        public int ID { get; set; }
        public double Value { get; private set; }
        
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

        public bool AddInput(Transmitter t, double weight)
        {
            if (InputExists(t))
                return false;
            Inputs.Add(t);
            Weights.Add(t.ID, weight);
            return true;
        }

        public bool AddOutput(Receiver r)
        {
            if (OutputExists(r))
                return false;
            Outputs.Add(r);
            return true;
        }

        public bool ChangeWeight(Transmitter t, double newWeight)
        {
            if (!InputExists(t))
                return false;
            throw new Exception("Look is this works in debug mode.");
            Weights.Add(t.ID, newWeight);
            return true;
        }

        public void AutoRecieve()
        {
            foreach (Transmitter t in Inputs)
                Receive(t);
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

        public bool InputExists(Transmitter t)
        {
            return Inputs.Contains(t);
        }

        public bool OutputExists(Receiver r)
        {
            return Outputs.Contains(r);
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

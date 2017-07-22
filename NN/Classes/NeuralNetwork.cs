using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN
{
    public class NeuralNetwork
    {
        Random rnd;
        private List<Transmitter> Inputs { get; set; }
        private List<Receiver> Outputs { get; set; }
        private List<List<Neuron>> Neurons { get; set; }
        private int idCounter;
        private readonly int reduction;
        private bool isBuilt;

        public NeuralNetwork()
        {
            idCounter = 1;
            reduction = 4;
            isBuilt = false;
            Neurons = new List<List<Neuron>>();
            Inputs = new List<Transmitter>();
            Outputs = new List<Receiver>();
            rnd = new Random();
        }

        public void AddInputs(ICollection<Transmitter> inputs)
        {
            foreach (Transmitter input in inputs)
                AddInput(input);
        }

        public void AddOutputs(ICollection<Receiver> outputs)
        {
            foreach (Receiver output in outputs)
                AddOutput(output);
        }

        public void AddInput(Transmitter input)
        {
            input.ID = GetID();
            Inputs.Add(input);
        }

        public void AddOutput(Receiver output)
        {
            output.ID = GetID();
            Outputs.Add(output);
        }

        public void Build()
        {
            if (Inputs.Count < 1)
                return;
            if (Outputs.Count < 1)
                return;
            isBuilt = true;
            Neurons.Clear();
            CreateLevel(Inputs, 0);
        }

        private void CreateLevel(List<Transmitter> inputs, int levelIndex)
        {
            bool lastRun = false; // Indicates if this is the last level building iteration
            int actualReduction = this.reduction;
            // Calculate number of neurons for this level
            int neurCount = (int)Math.Ceiling((double)inputs.Count / actualReduction);
            if (neurCount <= Outputs.Count)
            {
                // If neuron counter is less then output number, add neurons
                while (neurCount < Outputs.Count)
                    neurCount++;
                actualReduction = (int)Math.Floor((double)inputs.Count / neurCount);
                lastRun = true;
            }
            List<Neuron> level = new List<Neuron>();
            int inputIndex = 0;
            // For each neuron
            for (int i = 0; i < neurCount; i++)
            {
                Neuron neuron = new Neuron(GetID());
                // Connect inputs
                for (int j = 0; (j < actualReduction) && (inputIndex < inputs.Count); j++)
                    Connect(neuron, inputs[inputIndex++]);
                level.Add(neuron);
            }
            // This will run on last level building. TODO: Check it again.
            for (; inputIndex < inputs.Count;)
                Connect(level[0], inputs[inputIndex++]);
            // Save level
            Neurons.Add(level);
            if (lastRun)
            {
                // Connect last level neurons to outputs
                for (int i = 0; i < level.Count; i++)
                    level[i].AddOutput(Outputs[i]);
                return;
            }
            // Create next level, when this level is the input
            CreateLevel(level.ToList<Transmitter>(), levelIndex + 1);
        }

        private void Connect(Neuron n, Transmitter input)
        {
            n.AddInput(input, rnd.NextDouble());
            if (input is Neuron)
                ((Neuron)input).AddOutput(n);
        }

        public void Activate()
        {
            if (!isBuilt)
                return;
            foreach (Neuron n in Neurons[0])
                n.AutoRecieve();
        }

        public LearningSet GenerateLearningSet()
        {
            if (!isBuilt)
                return null;
            return new LearningSet(Inputs, Outputs);
        }

        public static double Normalize(double x)
        {
            return 1 / (1 + Math.Pow(Math.E, -x));
        }

        /*
        public void Connect()
        {
            Neuron n1 = GetRandomNeuron();
            Neuron n2 = GetRandomNeuron();
            Connect(n1, n2);
        }
        
        private Neuron GetRandomNeuron()
        {
            int index = rnd.Next(Neurons.Count);
            return Neurons[index];
        }*/

        private int GetID()
        {
            return idCounter++;
        }
    }
}
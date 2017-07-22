using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NN
{
    public class NeuralNetwork
    {
        Random rnd;
        private List<Input> Inputs { get; set; }
        private List<Output> Outputs { get; set; }
        private List<List<Neuron>> Neurons { get; set; }
        private readonly int reduction;
        private bool isBuilt;

        public NeuralNetwork()
        {
            reduction = 4;
            isBuilt = false;
            Neurons = new List<List<Neuron>>();
            Inputs = new List<Input>();
            Outputs = new List<Output>();
            rnd = new Random();
        }

        public void AddInputs(ICollection<Input> inputs)
        {
            foreach (Input input in inputs)
                AddInput(input);
        }

        public void AddOutputs(ICollection<Output> outputs)
        {
            foreach (Output output in outputs)
                AddOutput(output);
        }

        public bool AddInput(Input input)
        {
            if (InputExists(input))
                return false;
            Inputs.Add(input);
            return true;
        }

        public bool AddOutput(Output output)
        {
            if (OutputExists(output))
                return false;
            Outputs.Add(output);
            return true;
        }

        public void Build()
        {
            if (Inputs.Count < 1)
                return;
            if (Outputs.Count < 1)
                return;
            isBuilt = true;
            Neurons.Clear();
            CreateLevel(Inputs.ToList<ITransmitter>(), 0);
        }

        private void CreateLevel(List<ITransmitter> inputs, int levelIndex)
        {
            bool lastLevel = false; // Indicates if this is the last level building iteration
            int actualReduction = reduction;
            // Calculate number of neurons for this level
            int neurCount = (int)Math.Ceiling((double)inputs.Count / actualReduction);
            if (neurCount <= Outputs.Count)
            {
                // If neuron counter is less then output number, add neurons
                while (neurCount < Outputs.Count)
                    neurCount++;
                actualReduction = (int)Math.Floor((double)inputs.Count / neurCount);
                lastLevel = true;
            }
            List<Neuron> level = new List<Neuron>();
            int inputIndex = 0;
            // For each neuron
            for (int i = 0; i < neurCount; i++)
            {
                Neuron neuron = new Neuron();
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
            if (lastLevel)
            {
                // Connect last level neurons to outputs
                for (int i = 0; i < level.Count; i++)
                    level[i].AddOutput(Outputs[i]);
                return;
            }
            // Create next level, when this level is the input
            CreateLevel(level.ToList<ITransmitter>(), levelIndex + 1);
        }

        private void Connect(Neuron n, ITransmitter input)
        {
            n.AddInput(input, rnd.NextDouble());
            if (input is Neuron)
                ((Neuron)input).AddOutput(n);
        }

        public void Activate()
        {
            BuiltCheck();
            foreach (Neuron n in Neurons[0])
                n.AutoRecieve();
        }

        public LearningSet GenerateLearningSet()
        {
            BuiltCheck();
            return new LearningSet(Inputs, Outputs);
        }

        public void Practice(LearningSet ls)
        {
            BuiltCheck();
            foreach (Input input in Inputs)
                input.Value = ls.GivenInputs[input.ID];
            Activate();
        }

        public bool InputExists(Input input)
        {
            return Inputs.Contains(input);
        }

        public bool OutputExists(Output output)
        {
            return Outputs.Contains(output);
        }

        private void BuiltCheck()
        {
            if (isBuilt)
                return;
            StackTrace trance = new StackTrace();
            string method = trance.GetFrame(1).GetMethod().Name;
            throw new MethodAccessException("The method '" + method + "' can be accessed only after the 'Build' method was called.");
        }

        public static double Normalize(double x)
        {
            return 1 / (1 + Math.Pow(Math.E, -x));
        }
    }
}
using Handy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using NN;

namespace Testing
{
    class Program
    {
        const int width = 95;
        const int height = 20;

        [STAThread]
        static void Main(string[] args)
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height + 500);
            Design.Headline(Application.ProductName.Replace('_', ' '));

            int inputSize = 10;
            BaseTrans[] inputs = new BaseTrans[inputSize];
            NeuralNetwork nn = new NeuralNetwork();
            Random r = new Random();
            for (int i = 0; i < inputSize; i++)
            {
                inputs[i] = new BaseTrans();
                nn.AddInput(inputs[i]);
            }
            BaseRec r1 = new BaseRec();
            //BaseReciever r2 = new BaseReciever();

            nn.AddOutput(r1);
            //nn.AddOutput(r2);


            int iterationNum = 10000;
            double sum = 0;
            for (int i = 0; i < iterationNum; i++)
            {
                nn.Build();
                foreach (BaseTrans input in inputs)
                {
                    input.Value = r.Next(100);
                }
                nn.Activate();
                sum += r1.Value;
                //Console.WriteLine(r1.Value);
            }

            Console.WriteLine(sum / iterationNum);

            //Design.End();
        }
    }
}

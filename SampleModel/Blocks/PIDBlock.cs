using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModel.Blocks
{
    public class PIDBlock : BaseBlock
    {
        private GainBlock Gain;
        private IntBlock Int;
        private DiffBlock Diff;

        private double ki = 0.000001;

        public double K { get { return Gain.Gain; } set { Gain.Gain = value; } }
        public double Ti { 
            get { return 1 / ki; } 
            set { if (value == 0) ki = double.MaxValue;
                  else ki = 1 / value; 
            }  
        }
        public double Ki { get { return ki; } set { ki = value; }  }
        public double Td { get; set; }

        public PIDBlock(double dt) {
            Gain = new GainBlock(1);
            Int = new IntBlock(dt);
            Diff = new DiffBlock(dt);
        }

        public override double Calc(double x) {
            var u = Gain.Calc(x) + ki * Int.Calc(x) + Td * Diff.Calc(x);
            return u;
        }
    }
}

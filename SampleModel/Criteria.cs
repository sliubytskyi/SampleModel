using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModel
{
    public class Criteria
    {
        public static double dt = 0.1;
        public static double maxTime = 10;
        public static double eps = 0.1;
        public static double I2Criteria(double[] vars) {
            double sum = 0;
            ControlSystem sys = new ControlSystem(dt);
            sys.PID.K = vars[0];
            sys.PID.Ti = vars[1];
            sys.PID.Td = vars[2];
            sys.SetPoint = 5;
            var stepCnt = (int)(maxTime / dt);

            for (int i = 0; i < stepCnt; i++) {
                sys.Calc();
                sum += sys.E * sys.E * dt;
            }

            return sum;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo3
{
    class ReportData
    {
        public int Iteration { get; set; }
        public double Reg { get; set; }
        public int D { get; set; }
        public double ObjectiveFunc { get; set; }
        public double AverageError{ get; set; }

        public ReportData(int iteration, double reg, int d, double objFun)
        {
            Iteration = iteration;
            Reg = reg;
            D = d;
            ObjectiveFunc = objFun;
        }
    }
}

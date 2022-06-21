using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModel
{
    internal class Optimization
    {
        public static string PointToString(double[] point, String caption = "") {
            string s="";
            if (!String.IsNullOrEmpty(caption)) {
                s += caption + " ";
            }
            for (int i = 0; i < point.Length; i++) {
                s += $"u[{i}]={point[i]} ";
            }
            return s;
        }
        public static void PrintPoint(double[] point, String caption = "") {
            if (!String.IsNullOrEmpty(caption)) {
                Console.Write(caption + " ");
            }
            for (int i = 0; i < point.Length; i++) {
                Console.Write($"u[{i}]={point[i]} ");
            }
            Console.WriteLine();
        }
        public static double GoldenSection(Func<double[], double> f, double[] point, int varIndex, double a, double b, double eps = 0.01, int maxStep = 10000) {
            double x1, x2, y1, y2;
            double fi = (1 + Math.Sqrt(5)) / 2;
            int steps = 0;
            do {
                x1 = b - (b - a) / fi;
                x2 = a + (b - a) / fi;
                point[varIndex] = x1;
                y1 = f(point);
                point[varIndex] = x2;
                y2 = f(point);
                if (y1 >= y2) a = x1;
                else b = x2;
                steps++;
                if (steps > maxStep) break;
            }
            while (Math.Abs(b - a) > eps);
            point[varIndex] = (b + a) / 2;
            return point[varIndex];
        }

        public static double findMin(Func<double[], double> f, ref double[] p, int i, double h = 10, double eps = 0.01) {
            var l = p[i];
            double[] p1 = new double[p.Length];
            double f0, f1, f2;
            do {
                f1 = f(p);
                p.CopyTo(p1, 0);
                p1[i] = p[i] + h;
                f2 = f(p1);
                bool fwd = (f1 > f2);
                if (!fwd) {
                    p1[i] = p[i] - h;
                    f2 = f(p1);
                }
                while (f1 > f2) {
                    f1 = f2;
                    if (fwd) { p[i] = p[i] + h; }
                    else { p[i] = p[i] - h; }
                    f2 = f(p);
                    PrintPoint(p, "L: ");
                }
                h /= 10;
            } while (h > eps);
            return f(p);
        }
        public static int CoordinateDescent2(Func<double[], double> f, ref double[] point, double h = 0.1, double eps = 0.01, int maxStep = 1000) {
            int k = 0;
            double[] prev = new double[point.Length];
            do {
                double lenSum = 0;
                point.CopyTo(prev, 0);
                for (int i = 0; i < point.Length; i++) {
                    findMin(f, ref point, i);
                    PrintPoint(point);
                    lenSum += Math.Pow((prev[i] - point[i]), 2);
                }
                var len = Math.Sqrt(lenSum);
                if (len <= eps) {
                    break;
                }
                k++;
            } while (k < maxStep);
            return k;
        }



        public static int HookeJeeves(Func<double[], double> f,ref double[] point,  double h=10, double eps = 0.01, int maxStep = 1000) {
            int step = 0; ;
            double[] xb = (double[])point.Clone(), x1, xob;
            double fb = f(xb), f0, f1 = 0;

            do {
                f0 = fb;
                point = (double[])xb.Clone();
                x1 = (double[])point.Clone();
                for (int i = 0; i < point.Length; i++) {
                    x1[i] = point[i] + h;
                    f1 = f(x1);
                    if (f1 < f0) {
                        point = (double[])x1.Clone();
                        f0 = f1;
                    }
                    else {
                        x1[i] = point[i] - h;
                        f1 = f(x1);
                        if (f1 < f0) {
                            point = (double[])x1.Clone();
                            f0 = f1;
                        }
                    }
                }
                if (f0 < fb) {  // пошук за зразком
                    xob = (double[])xb.Clone();
                    for (int i = 0; i < point.Length; i++) {
                        xb[i] = xob[i] + 2 * (point[i] - xob[i]);
                    }
                    fb = f(xb);
                    if(f0 < fb) h /= 10; // значення в новій базисній виявилося гірше
                }
                else {
                    h /= 10;
                }
                step++;
            } while (h > eps && step < maxStep);
            point = (double[])xb.Clone();
            return step;
        }

        public static int CoordinateDescent(Func<double[], double> f, double[] point, double a, double b, double eps = 0.01, int maxStep = 1000) {
            double y1 = f(point);
            double y2 = y1 + 100;
            int step = 0;
            do {
                y2 = y1;
                for (int i = 0; i < point.Length; i++) {
                    Optimization.GoldenSection(f, point, i, a, b);
                    y1 = f(point);
                }
                step++;
            } while (Math.Abs(y2 - y1) < eps);
            return step;
        }
       
    }
}

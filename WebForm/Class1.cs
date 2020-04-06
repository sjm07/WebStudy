using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm
{
    public class Class1
    {
        public List<double> Xs { get; private set; }

        public Class1(List<double> Xs)
        {
            this.Xs = Xs;

            Average = Xs.Average();
            Variance = GetVariance(Xs);

            if (Variance == 0) throw new Exception("方差为0");//此时不需要统计 因为每个样本数据都相同，可以在界面做相应提示

            StandardVariance = Math.Sqrt(Variance);
        }

        /// <summary>
        /// 方差/标准方差的平方
        /// </summary>
        public double Variance { get; private set; }

        /// <summary>
        /// 标准方差
        /// </summary>
        public double StandardVariance { get; private set; }

        /// <summary>
        /// 算数平均值/数学期望
        /// </summary>
        public double Average { get; private set; }

        /// <summary>
        /// 1/ (2π的平方根)的值
        /// </summary>
        public static double InverseSqrt2PI = 1 / Math.Sqrt(2 * Math.PI);

        /// <summary>
        /// 获取指定X值的Y值  计算正太分布的公式
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double GetGaussianDistributionY(double x)
        {
            double PowOfE = -(Math.Pow(Math.Abs(x - Average), 2) / (2 * Variance));

            double result = (Class1.InverseSqrt2PI / StandardVariance) * Math.Pow(Math.E, PowOfE);

            return result;
        }

        /// <summary>
        /// 获取正太分布的坐标<x,y>
        /// </summary>
        /// <returns></returns>
        public List<Tuple<double, double>> GetGaussianDistributionYs()
        {
            List<Tuple<double, double>> XYs = new List<Tuple<double, double>>();

            Tuple<double, double> xy = null;

            foreach (double x in Xs)
            {
                xy = new Tuple<double, double>(x, GetGaussianDistributionY(x));
                XYs.Add(xy);
            }



            return XYs;
        }

        /// <summary>
        /// 获取整型列表的方差
        /// </summary>
        /// <param name="src">要计算方差的数据列表</param>
        /// <returns></returns>
        public static double GetVariance(List<double> src)
        {
            double average = src.Average();
            double SumOfSquares = 0;
            src.ForEach(x => { SumOfSquares += Math.Pow(x - average, 2); });
            return SumOfSquares / src.Count;//方差
        }

        /// <summary>
        /// 获取整型列表的方差
        /// </summary>
        /// <param name="src">要计算方差的数据列表</param>
        /// <returns></returns>
        public static float GetVariance(List<float> src)
        {
            float average = src.Average();
            double SumOfSquares = 0;
            src.ForEach(x => { SumOfSquares += Math.Pow(x - average, 2); });
            return (float)SumOfSquares / src.Count;//方差
        }
    }
}
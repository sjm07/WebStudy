using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    /*正太分布算法*/
    public partial class ZTFB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Xs = new List<double>();
            Xs.Add(6);
            Xs.Add(6.5);
            Xs.Add(7);
            Xs.Add(7.5);
            Xs.Add(8);
            Xs.Add(8.5);
            Xs.Add(9);

            Variance = 0.179626402;

            Average = 7.501;
            //Variance = 0.4238235501404;
            StandardVariance = Math.Sqrt(Variance);
        }

        /*X轴值*/
        public List<double> Xs { get; private set; }

        // <summary>
        /// 算数平均值/数学期望
        /// </summary>
        public double Average { get; private set; }

        /// <summary>
        /// 1/ (2π的平方根)的值
        /// </summary>
        public static double InverseSqrt2PI = 1 / Math.Sqrt(2 * Math.PI);

        /// <summary>
        /// 方差
        /// </summary>
        public double Variance { get; private set; }

        /// <summary>
        /// 标准方差
        /// </summary>
        public double StandardVariance { get; private set; }

        /// <summary>
        /// 获取指定X值的Y值  计算正太分布的公式
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double GetGaussianDistributionY(double x)
        {
            double PowOfE = -(Math.Pow(Math.Abs(x - Average), 2) / (2 * Variance));

            double result = (ZTFB.InverseSqrt2PI / StandardVariance) * Math.Pow(Math.E, PowOfE);

            return result;
        }

        protected void btnZTFB_Click(object sender, EventArgs e)
        {
            List<double> Ys = new List<double>();
            foreach (double xValue in Xs)
            {
                Ys.Add(GetGaussianDistributionY(xValue));
            }
        }

        /// <summary>
        /// 线性回归系数计算（R）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnXXHG_Click(object sender, EventArgs e)
        {
            //数据来自上面的例子
            double[] x = new double[10] { 74, 71, 72, 68, 76, 73, 67, 70, 65, 74 };
            double[] y = new double[10] { 76, 75, 71, 70, 76, 79, 65, 77, 62, 72 };
            int count = 10;
            double a, b, maxerr, rr;
            //a b 为系数，rr 为相关系数的平方，maxerr为最大偏差
            CalcRegress(x, y, count, out a, out b, out maxerr, out rr);
            Console.WriteLine("a={0}\n b={1}\n maxerr={2}\n rr={3}", a, b, maxerr, rr);
            Console.ReadKey();
        }

        public static void CalcRegress(double[] x, double[] y, int count, out double a, out double b, out double maxErr, out double r)
        {
            double sumX = 0;
            double sumY = 0;
            double avgX;
            double avgY;
            double r1;
            double r2;
            //数据量过少，无法计算
            //if (count < 4)
            //{
            //    return;
            //}

            for (int i = 0; i < count; i++)
            {
                sumX += x[i];
                sumY += y[i];
            }

            avgX = sumX / count;
            avgY = sumY / count;

            double SPxy = 0;
            double SSx = 0;
            double SSy = 0;
            for (int i = 0; i < count; i++)
            {
                SPxy += (x[i] - avgX) * (y[i] - avgY);
                SSx += (x[i] - avgX) * (x[i] - avgX);
                SSy += (y[i] - avgY) * (y[i] - avgY);
            }
            //如果所有点的x相同，直线平行于y轴，无法计算。
            //如果所有点的y相同直线为平行于x轴的直线y=k+0*x
            if (SSy == 0)
            {
                a = y[1];
                b = 0;
                //rr = 0;
                r = 0;
                maxErr = 0;
                //return -1;
            }

            //y=bx+a
            b = SPxy / SSx;
            a = avgY - b * avgX;

            //开始计算R²值
            r1 = SPxy * SPxy;//分子的平方
            r2 = SSx * SSy;//分母的平方
            r = r1 / r2;    //计算R²值

            //下面代码计算最大偏差            
            maxErr = 0;
            for (int i = 0; i < count; i++)
            {
                double yi = a + b * x[i];
                double absErrYi = Math.Abs(yi - y[i]);//假动作

                if (absErrYi > maxErr)
                {
                    maxErr = absErrYi;
                }
            }
        }

    }
}
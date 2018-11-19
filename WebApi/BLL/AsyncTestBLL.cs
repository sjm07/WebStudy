using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Util.Log;

namespace WebApi.BLL
{
    public class AsyncTestBLL
    {
        public static void MainThread()
        {
            Logger.Info("--------------------------------------------------------------------");
            Logger.Info("主线程开始执行,时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Logger.Info("--------------------------------------------------------------------");
            /*
            * 主线程调用ShowInfoAsync异步方法
            * 使用async和await关键字
            */

            Task sa = new Task(ShowInfoAsync);
            sa.Start();
            sa.Wait();
            //ShowInfoAsync();
            Logger.Info(string.Empty);
            /*同步操作*/
            for (int i = 0; i < 3; i++)
            {
                Logger.Info(string.Format("主线程同步循环显示序号：{0},时间:{1}", i.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            Logger.Info("--------------------------------------------------------------------");
            Logger.Info("主线程执行结束,时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Logger.Info("--------------------------------------------------------------------");
        }

        /// <summary>
        /// 异步显示输出信息
        /// </summary>
        static async void ShowInfoAsync()
        {

            //声明要调用的异步方法，获取异步任务操作类 Task
            Task<string> getTask = GetDataAsync();

            Logger.Info(string.Empty);
            Logger.Info(string.Format("开始执行异步获取数据方法,时间:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

            /*
             * 异步获取数据并显示
             * 这里使用 await 关键词启用并等待异步返回结果
             * 在声明await关键词的地方线程就会在此等待返回结果
             * 只有返回结果才会继续执行下面步骤
             */
            string strShowString = await getTask;

            Logger.Info("***************************************************************");
            Logger.Info(string.Format("异步获取数据执行结束，获取数据值：{0},时间:{1}", strShowString, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            Logger.Info("***************************************************************");
            Logger.Info(string.Empty);
        }

        /// <summary>
        /// 异步获取返回的数据
        /// </summary>
        /// <returns></returns>
        static async Task<string> GetDataAsync()
        {
            //这里也可以使用Task.Run
            return await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                string strReturn = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(500);
                    Logger.Info(string.Format("异步获取数据,第{0}次循环,时间:{1}", i + 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    strReturn += i.ToString();
                }
                return strReturn;
            });
        }

        public static void AsyncTest()
        {
            Logger.Info("AsyncTest begin !!");
            Task sa = new Task(GetInfo);
            sa.Start();
            sa.Wait();

            Task<string> sab = new Task<string>(() => GetAsync());

            //await sab;

            int a = 123;
            Logger.Info(a);
        }

        public static void GetInfo()
        {
            Logger.Info("GetInfo");
            string b = TestGetInfo();
            Logger.Info(b);
        }

        public static string TestGetInfo()
        {
            return "TestGetInfo";
        }

        public static string GetAsync()
        {
            return "async";
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// 线程工具单元测试
    /// </summary>
    public class ThreadTests
    {
        // [Fact]
        // public void NewExecutorTest()
        // {
        //     var executor = ThreadUtil.NewExecutor(5);
        //     Assert.Equal(5, executor.CorePoolSize);
        //     executor.Dispose();
        // }

        // [Fact]
        // public void ExecuteTest()
        // {
        //     var isValid = true;
        //     var executed = false;

        //     ThreadUtil.Execute(() =>
        //     {
        //         Assert.True(isValid);
        //         executed = true;
        //     });

        //     // 等待任务执行完成
        //     Thread.Sleep(100);
        //     Assert.True(executed);
        // }

        // [Fact]
        // public void SafeSleepTest()
        // {
        //     var sleepMillis = RandomUtil.RandomLong(1, 1000);
        //     var startTime = DateTime.Now;
        //     ThreadUtil.SafeSleep(sleepMillis);
        //     var elapsedMillis = (DateTime.Now - startTime).TotalMilliseconds;
        //     Assert.True(elapsedMillis >= sleepMillis);
        // }

        // [Fact]
        // public void AsyncUtilTest()
        // {
        //     var tasks = new Action[3];
        //     var executed = new bool[3];

        //     for (int i = 0; i < 3; i++)
        //     {
        //         var index = i;
        //         tasks[i] = () =>
        //         {
        //             Thread.Sleep(50);
        //             executed[index] = true;
        //         };
        //     }

        //     AsyncUtil.Execute(tasks);
        //     
        //     // 等待所有任务完成
        //     Thread.Sleep(200);
        //     
        //     foreach (var exec in executed)
        //     {
        //         Assert.True(exec);
        //     }
        // }

        // [Fact]
        // public void ConcurrencyTesterTest()
        // {
        //     var counter = 0;
        //     var tester = new ConcurrencyTester(10, 100);
        //     
        //     tester.AddTask(() =>
        //     {
        //         Interlocked.Increment(ref counter);
        //     });

        //     tester.Execute();
        //     
        //     Assert.Equal(1000, counter);
        // }

        // [Fact]
        // public void SyncFinisherTest()
        // {
        //     var finisher = new SyncFinisher(3);
        //     var completed = false;

        //     finisher.AddListener(() =>
        //     {
        //         completed = true;
        //     });

        //     for (int i = 0; i < 3; i++)
        //     {
        //         ThreadUtil.Execute(() =>
        //         {
        //             Thread.Sleep(50);
        //             finisher.CountDown();
        //         });
        //     }

        //     // 等待完成
        //     Thread.Sleep(200);
        //     Assert.True(completed);
        // }
    }
}
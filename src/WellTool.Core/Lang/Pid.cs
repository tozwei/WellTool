using System;
using System.Diagnostics;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 进程ID
    /// </summary>
    public class Pid
    {
        private readonly int _pid;
        private readonly string _name;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Pid(int pid, string name = null)
        {
            _pid = pid;
            _name = name;
        }

        /// <summary>
        /// 进程ID
        /// </summary>
        public int Id => _pid;

        /// <summary>
        /// 进程名称
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// 获取当前进程
        /// </summary>
        public static Pid Current()
        {
            var process = Process.GetCurrentProcess();
            return new Pid(process.Id, process.ProcessName);
        }

        /// <summary>
        /// 根据ID获取进程
        /// </summary>
        public static Pid GetById(int pid)
        {
            try
            {
                var process = Process.GetProcessById(pid);
                return new Pid(process.Id, process.ProcessName);
            }
            catch
            {
                return new Pid(pid);
            }
        }

        /// <summary>
        /// 获取关联的进程
        /// </summary>
        public Process GetProcess()
        {
            try
            {
                return Process.GetProcessById(_pid);
            }
            catch
            {
                return null;
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(_name) ? _pid.ToString() : $"{_name}({_pid})";
        }
    }
}

using System;
using System.Net.Sockets;
using System.Threading;

namespace WellTool.Core.Net
{
    /// <summary>
    /// 本地端口生成器（LocalPortGenerator）。
    /// <p>
    /// 当前类名中"Generater"为拼写错误（正确应为 Generator），为保持兼容性暂未更改。
    /// 该问题将在后续大版本中以重命名方式修复，并保留旧类名的弃用兼容层。
    /// </p>
    /// 
    /// 用于从指定起点开始递增探测一个当前"可用"的本地端口。探测通过短暂绑定
    /// ServerSocket完成，但不会真正占用端口。
    /// <p>注意：</p>
    /// <ul>
    ///   <li>该方法执行的是端口"探测"，非"分配"，返回端口不保证实际使用时仍然可用。</li>
    ///   <li>存在 TOCTOU（检测到使用之间）竞态，多线程下可能返回同一端口。</li>
    ///   <li>不适合作为生产级端口分配策略，推荐使用 new ServerSocket(0)。</li>
    /// </ul>
    /// </summary>
    public class LocalPortGenerater
    {
        private readonly int _beginPort;
        private int _currentPort;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="beginPort">起始端口号</param>
        public LocalPortGenerater(int beginPort)
        {
            _beginPort = beginPort;
            _currentPort = beginPort;
        }

        /// <summary>
        /// 生成一个本地端口，用于远程端口映射
        /// </summary>
        /// <returns>未被使用的本地端口</returns>
        public int Generate()
        {
            int validPort = _currentPort;
            // 获取可用端口
            while (!IsUsableLocalPort(validPort))
            {
                validPort++;
            }
            _currentPort = validPort + 1;
            return validPort;
        }

        /// <summary>
        /// 检查本地端口是否可用
        /// </summary>
        /// <param name="port">端口</param>
        /// <returns>是否可用</returns>
        private static bool IsUsableLocalPort(int port)
        {
            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, port));
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
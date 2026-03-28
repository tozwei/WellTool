using System;
using Xunit;
using WellTool.Core.Exceptions;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// 异常工具单元测试
    /// </summary>
    public class ExceptionsTests
    {
        [Fact]
        public void TestExceptionUtil()
        {
            // 测试获取异常信息
            var exception = new Exception("测试异常");
            string message = ExceptionUtil.GetMessage(exception);
            Assert.Equal("测试异常", message);

            // 测试获取根异常
            var innerException = new Exception("内部异常");
            var outerException = new Exception("外部异常", innerException);
            var rootException = ExceptionUtil.GetRootCause(outerException);
            Assert.Equal(innerException, rootException);

            // 测试获取异常栈信息
            string stackTrace = ExceptionUtil.GetStacktrace(exception);
            Assert.NotNull(stackTrace);

            // 测试获取根异常信息
            string rootCauseMessage = ExceptionUtil.GetRootCauseMessage(outerException);
            Assert.Equal("内部异常", rootCauseMessage);

            // 测试判断异常是否由指定类型引起
            bool isCausedBy = ExceptionUtil.IsCausedBy(outerException, typeof(Exception));
            Assert.True(isCausedBy);
        }

        [Fact]
        public void TestBaseException()
        {
            // 测试BaseException
            var baseException = new BaseException("基础异常");
            Assert.Equal("基础异常", baseException.Message);

            // 测试带内部异常的BaseException
            var innerException = new Exception("内部异常");
            var baseExceptionWithInner = new BaseException("基础异常", innerException);
            Assert.Equal("基础异常", baseExceptionWithInner.Message);
            Assert.Equal(innerException, baseExceptionWithInner.InnerException);
        }

        [Fact]
        public void TestUtilException()
        {
            // 测试UtilException
            var utilException = new UtilException("工具异常");
            Assert.Equal("工具异常", utilException.Message);

            // 测试带内部异常的UtilException
            var innerException = new Exception("内部异常");
            var utilExceptionWithInner = new UtilException("工具异常", innerException);
            Assert.Equal("工具异常", utilExceptionWithInner.Message);
            Assert.Equal(innerException, utilExceptionWithInner.InnerException);
        }

        [Fact]
        public void TestExceptionUtilWrapRuntime()
        {
            // 测试包装异常为运行时异常
            var originalException = new Exception("原始异常");
            var wrappedException = ExceptionUtil.WrapRuntime<UtilException>(originalException);
            Assert.NotNull(wrappedException);
            Assert.Equal("原始异常", wrappedException.Message);
            Assert.Equal(originalException, wrappedException.InnerException);
        }
    }
}
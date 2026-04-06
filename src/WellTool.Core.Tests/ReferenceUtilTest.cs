using Xunit;
using WellTool.Core;
using System;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Reference工具单元测试
    /// </summary>
    public class ReferenceUtilTest
    {
        [Fact]
        public void CreateSoftReferenceTest()
        {
            var obj = new object();
            var softRef = ReferenceUtil.CreateSoftReference(obj);
            Assert.NotNull(softRef);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        [Fact]
        public void CreateWeakReferenceTest()
        {
            var obj = new object();
            var weakRef = ReferenceUtil.CreateWeakReference(obj);
            Assert.NotNull(weakRef);
        }

        [Fact]
        public void CreatePhantomReferenceTest()
        {
            var obj = new object();
            var phantomRef = ReferenceUtil.CreatePhantomReference(obj);
            Assert.NotNull(phantomRef);
        }

        [Fact]
        public void GetTest()
        {
            var obj = new object();
            var weakRef = ReferenceUtil.CreateWeakReference(obj);
            Assert.Same(obj, weakRef.Get());
        }

        [Fact]
        public void IsEnqueuedTest()
        {
            var obj = new object();
            var weakRef = ReferenceUtil.CreateWeakReference(obj);
            Assert.False(weakRef.IsEnqueued);
        }
    }
}

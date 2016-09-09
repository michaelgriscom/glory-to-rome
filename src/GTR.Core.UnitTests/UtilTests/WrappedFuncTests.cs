#region

using System;
using GTR.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace GTR.Core.UnitTests.UtilTests
{
    [TestClass]
    public class WrappedFuncTests
    {
        [TestMethod]
        public void SingleFunc()
        {
            const int returnVal = 2;
            Func<int> returnTwoFunc = (() => returnVal);
            var func = new WrappedFunc<int>(returnTwoFunc);

            int result = func.Execute();
            Assert.AreEqual(returnVal, result);
        }

        [TestMethod]
        public void SingleFuncWithParameter()
        {
            Func<int, int> doubleFunc = (x => x*2);
            var func = new WrappedFunc<int, int>(doubleFunc);
            const int seedVal = 8;
            int result = func.Execute(seedVal);
            Assert.AreEqual(seedVal*2, result);
        }

        [TestMethod]
        public void WrappedFunc()
        {
            const int returnVal = 3;
            Func<int> returnThreeFunc = (() => returnVal);
            Func<int, int> doubleFunc = (x => x*2);

            var func = new WrappedFunc<int>(returnThreeFunc);
            func.Wrap(doubleFunc);

            int result = func.Execute();
            Assert.AreEqual(returnVal*2, result);
        }

        [TestMethod]
        public void WrappedFuncWithParam()
        {
            const int seedVal = 4;
            Func<int, int> doubleFunc = (x => x*2);
            Func<int, int, int> addThreeFunc = ((x, y) => y + 3);
            var func = new WrappedFunc<int, int>(doubleFunc);
            func.Wrap(addThreeFunc);

            int result = func.Execute(seedVal);
            Assert.AreEqual((seedVal*2) + 3, result);
        }

        [TestMethod]
        public void DoubleWrappedFunc()
        {
            const int returnVal = 3;
            Func<int> returnThreeFunc = (() => returnVal);
            Func<int, int> doubleFunc = (x => x*2);
            Func<int, int> tripleFunc = (x => x*3);

            var func = new WrappedFunc<int>(returnThreeFunc);
            func.Wrap(doubleFunc);
            func.Wrap(tripleFunc);

            int result = func.Execute();
            Assert.AreEqual(returnVal*2*3, result);
        }

        [TestMethod]
        public void NestedUnwrappedFunc()
        {
            const int returnVal = 3;
            Func<int> returnThreeFunc = (() => returnVal);
            Func<int, int> doubleFunc = (x => x*2);
            Func<int, int> tripleFunc = (x => x*3);

            var func = new WrappedFunc<int>(returnThreeFunc);
            func.Wrap(doubleFunc);
            func.Wrap(tripleFunc);
            func.Unwrap(doubleFunc);

            int result = func.Execute();
            Assert.AreEqual(returnVal*3, result);
        }

        [TestMethod]
        public void UnwrappedFunc()
        {
            const int returnVal = 3;
            Func<int> returnThreeFunc = (() => returnVal);
            Func<int, int> doubleFunc = (x => x*2);

            var func = new WrappedFunc<int>(returnThreeFunc);
            func.Wrap(doubleFunc);
            func.Unwrap(doubleFunc);

            int result = func.Execute();
            Assert.AreEqual(returnVal, result);
        }
    }
}
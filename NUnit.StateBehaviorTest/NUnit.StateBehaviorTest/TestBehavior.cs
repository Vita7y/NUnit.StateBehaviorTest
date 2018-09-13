using System.Collections.Generic;
using NUnit.Framework;

namespace NUnit.StateBehaviorTest
{
    [TestFixture]
    public class TestBehavior
    {
        private enum TestEnum
        {
            First,
            Second,
            Fird
        }

        [Test]
        public void Then_LongFormForChangeStatuses_Ok()
        {
            var ss = AssertStateBehavior.InitializeStateBehavior<TestEnum>();
            for (var i = 0; i < 3; i++) ss.Then(TestEnum.Fird, TestEnum.Fird).Then(TestEnum.Second, TestEnum.Second).Then(TestEnum.Fird, TestEnum.Fird);
        }

        [Test]
        public void Then_LongFormForStatuses_Ok()
        {
            var ss = AssertStateBehavior.InitializeStateBehavior<TestEnum>();
            var list = new List<TestEnum> {TestEnum.First, TestEnum.Second, TestEnum.Fird};
            for (var i = 0; i < 3; i++)
            {
                var res = list[i];
                ss.Then(res, TestEnum.First).Then(res, TestEnum.Second).Then(res, TestEnum.Fird);
            }
        }

        [Test]
        public void Then_ShortFormChangeStatuses_Ok()
        {
            var ss = AssertStateBehavior.InitializeStateBehavior<TestEnum>();
            var list = new List<TestEnum> {TestEnum.First, TestEnum.Second, TestEnum.Fird};
            for (var i = 0; i < 3; i++)
            {
                var res = list[i];
                ss.Then(res, TestEnum.First).Then(TestEnum.Fird, TestEnum.Fird).Then(TestEnum.Fird);
            }
        }

        [Test]
        public void Then_ShortFormForStatuses_Ok()
        {
            var ss = AssertStateBehavior.InitializeStateBehavior<TestEnum>();
            var list = new List<TestEnum> {TestEnum.First, TestEnum.Second, TestEnum.Fird};
            for (var i = 0; i < 3; i++)
            {
                var res = list[i];
                ss.Then(res, TestEnum.First).Then(TestEnum.Second).Then(TestEnum.Fird);
            }
        }
    }
}
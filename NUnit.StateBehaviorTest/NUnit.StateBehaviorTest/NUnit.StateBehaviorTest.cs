using NUnit.Framework;

namespace NUnit.StateBehaviorTest
{
    public static class AssertStateBehavior
    {
        public static AssertStateFirst<T> InitializeStateBehavior<T>()
        {
            return new AssertStateFirst<T>();
        }

        public static IAssertState<T> Then<T>(this IAssertStateFirst<T> assertedAssertState, T state, T expectState)
        {
            assertedAssertState.CurrentState = state;

            if (assertedAssertState.Next != null) return assertedAssertState.Next;

            Assert.AreEqual(state, expectState);
            assertedAssertState.Next = new AssertStateNext<T>(state, assertedAssertState);
            return new AssertStateLast<T>(assertedAssertState);
        }

        public static IAssertState<T> Then<T>(this IAssertState<T> assertedAssertState, T state, T expectState)
        {
            if (assertedAssertState is IAssertStateLast<T>)
                return assertedAssertState;
            if (assertedAssertState.Next != null)
                return assertedAssertState.Next;

            Assert.AreEqual(state, expectState);
            assertedAssertState.Next = new AssertStateNext<T>(expectState, assertedAssertState.First);
            return new AssertStateLast<T>(assertedAssertState.First);
        }

        public static IAssertState<T> Then<T>(this IAssertState<T> assertedAssertState, T expectState)
        {
            if (assertedAssertState is IAssertStateLast<T>)
                return assertedAssertState;
            if (assertedAssertState.Next != null)
            {
                assertedAssertState.Next.CurrentState = assertedAssertState.CurrentState;
                return assertedAssertState.Next;
            }

            Assert.AreEqual(assertedAssertState.First.CurrentState, expectState);
            assertedAssertState.Next = new AssertStateNext<T>(expectState, assertedAssertState.First);
            return new AssertStateLast<T>(assertedAssertState.First);
        }

        public interface IAssertStateFirst<T>
        {
            IAssertState<T> Next { get; set; }
            T CurrentState { get; set; }
        }

        public interface IAssertState<T> : IAssertStateFirst<T>
        {
            IAssertStateFirst<T> First { get; }
        }

        public interface IAssertStateLast<T> : IAssertState<T>
        {
        }

        public class AssertStateFirst<T> : IAssertStateFirst<T>
        {
            public IAssertState<T> Next { get; set; }
            public T CurrentState { get; set; }
        }

        public class AssertStateNext<T> : IAssertState<T>
        {
            public AssertStateNext(T state, IAssertStateFirst<T> first)
            {
                CurrentState = state;
                First = first;
            }

            public IAssertState<T> Next { get; set; }
            public T CurrentState { get; set; }
            public IAssertStateFirst<T> First { get; }
        }

        public class AssertStateLast<T> : IAssertStateLast<T>
        {
            public AssertStateLast(IAssertStateFirst<T> first)
            {
                First = first;
            }

            public IAssertState<T> Next { get; set; }
            public T CurrentState { get; set; }
            public IAssertStateFirst<T> First { get; }
        }
    }
}
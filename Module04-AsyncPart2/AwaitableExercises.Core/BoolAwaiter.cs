using System;
using System.Runtime.CompilerServices;

namespace AwaitableExercises.Core
{
    public static class BoolExtensions
    {
        public static BoolAwaiter GetAwaiter(this bool b)
        {
            return new BoolAwaiter(b);
        }
    }

    public struct BoolAwaiter : INotifyCompletion
    {
        private bool b;

        public BoolAwaiter(bool b)
        {
            this.b = b;
        }

        public bool IsCompleted => true;

        public bool GetResult() => b;

        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }
    }
}
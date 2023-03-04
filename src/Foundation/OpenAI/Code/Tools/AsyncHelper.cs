using System;
using System.Threading;
using System.Threading.Tasks;

namespace EditorsCopilot.Foundation.OpenAI.Core.Tools
{
    public static class AsyncHelper
    {
        private static readonly TaskFactory MyTaskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return MyTaskFactory
                .StartNew(delegate { return func(); })
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            MyTaskFactory
                .StartNew(delegate { return func(); })
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}
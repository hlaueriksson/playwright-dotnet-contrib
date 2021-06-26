using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlaywrightContrib.FluentAssertions.Internal
{
    internal static class AsyncExtensions
    {
        // https://www.ryadel.com/en/asyncutil-c-helper-class-async-method-sync-result-wait/
        private static readonly TaskFactory _taskFactory = new
            TaskFactory(
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        internal static T Result<T>(this Task<T> task)
        {
            return AsyncExtensions.RunSync(() => task);
        }

        private static TResult RunSync<TResult>(Func<Task<TResult>> task)
#pragma warning disable CA2008 // Do not create tasks without passing a TaskScheduler
            => _taskFactory
                .StartNew(task)
#pragma warning restore CA2008 // Do not create tasks without passing a TaskScheduler
                .Unwrap()
                .GetAwaiter()
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
                .GetResult();
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits
    }
}

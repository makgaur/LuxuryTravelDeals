using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Common.Caching
{
    /// <summary>
    /// Provides support for async lazy initialization.
    /// </summary>
    /// <typeparam name="T">The type of object that is being lazily initialized.</typeparam>
    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class. 
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
        public AsyncLazy(Func<T> valueFactory)
            : base(() => Task.Factory.StartNew(valueFactory))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class. 
        /// Takes an async operation as parameter
        /// </summary>
        /// <param name="taskFactory">
        /// The task of the delegate that is invoked to produce the lazily initialized value when it is needed.
        /// </param>
        public AsyncLazy(Func<Task<T>> taskFactory)
            : base(() => Task.Factory.StartNew(taskFactory).Unwrap())
        {
        }

        /// <summary>
        /// Gets the awaiter to avoid having to call .Value for the result
        /// </summary>
        /// <returns>The task awaiter</returns>
        public TaskAwaiter<T> GetAwaiter()
        {
            return Value.GetAwaiter();
        }
    }
}

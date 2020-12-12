// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SampSharp.Core.Threading
{
    /// <summary>
    ///     Represents a message queue for messages sent to a <see cref="SampSharpSynchronizationContext" /> which can be retrieved via a semaphore.
    /// </summary>
    [Obsolete("Multi-process mode is deprecated and will be removed in a future release.")]
    public class SemaphoreMessageQueue : IMessageQueue
    {
        private readonly ConcurrentQueue<SendOrPostCallbackItem> _queue = new ConcurrentQueue<SendOrPostCallbackItem>();
        private readonly Semaphore _semaphore = new Semaphore(0, int.MaxValue);
        private readonly ManualResetEvent _stopSignal = new ManualResetEvent(false);
        private readonly WaitHandle[] _waitHandles;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SemaphoreMessageQueue" /> class.
        /// </summary>
        public SemaphoreMessageQueue()
        {
            _waitHandles = new WaitHandle[] { _semaphore, _stopSignal };
        }

        /// <summary>
        ///     Pushes the specified item onto this instance.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void PushMessage(SendOrPostCallbackItem item)
        {
            _queue.Enqueue(item);
            _semaphore.Release();
        }

        /// <summary>
        ///     Pops an item from this instance. If none are available, this call will wait for an item to be pushed.
        /// </summary>
        /// <returns>The next message, or null if canceled.</returns>
        public SendOrPostCallbackItem WaitForMessage()
        {
            // Wait for a signal (send by Enqueue or ReleaseReader)
            WaitHandle.WaitAny(_waitHandles);

            // Dequeue from the internal queue.
            if (_queue.TryDequeue(out var result))
                return result;

            // If no item was dequeued, a stop signal must have been sent, reset the signal back for the next read and return a default value.
            _stopSignal.Reset();

            return null;
        }
        
        /// <summary>
        ///     Releases the reader waiting in a <see cref="WaitForMessage" />.
        /// </summary>
        public void ReleaseReader()
        {
            _stopSignal.Set();
        }
    }
}
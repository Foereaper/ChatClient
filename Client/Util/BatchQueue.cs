﻿using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Client
{
    /// <summary>
    /// Multiple-writers, single-reader queue returning results in batches ignoring any further element queued till next batch
    /// </summary>
    public class BatchQueue<T>
    {
        ConcurrentQueue<T> queue = new ConcurrentQueue<T>();

        public void Enqueue(T item)
        {
            queue.Enqueue(item);
        }

        /// <summary>
        /// Returns the content of the queue at the time it is called, ignoring any any further element queued till next call
        /// </summary>
        public IEnumerable<T> BatchDequeue()
        {
            for (var currentCount = queue.Count; currentCount > 0; currentCount--)
                if (queue.TryDequeue(out var item))
                    yield return item;
        }
    }
}

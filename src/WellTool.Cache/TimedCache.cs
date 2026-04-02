// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Cache
{
    /// <summary>
    /// 定时缓存<br>
    /// 此缓存没有容量限制，对象只有在过期后才会被移除
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public class TimedCache<K, V> : ReentrantCache<K, V>
    {
        /// <summary>
        /// 正在执行的定时任务
        /// </summary>
        private CancellationTokenSource pruneJobCancellationTokenSource;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="timeout">超时（过期）时长，单位毫秒</param>
        public TimedCache(long timeout) : base(0, timeout)
        {}

        /// <summary>
        /// 清理过期对象
        /// </summary>
        /// <returns>清理数</returns>
        protected override int PruneCache()
        {
            int count = 0;
            var keysToRemove = new List<K>();

            // 清理过期对象
            foreach (var entry in CacheMap)
            {
                var cacheObj = entry.Value;
                if (cacheObj.IsExpired())
                {
                    keysToRemove.Add(entry.Key);
                    OnRemove(cacheObj.Key, cacheObj.Value);
                    count++;
                }
            }

            // 移除过期对象
            foreach (var key in keysToRemove)
            {
                CacheMap.Remove(key);
            }

            return count;
        }

        /// <summary>
        /// 缓存是否已满，TimedCache没有容量限制，永远返回false
        /// </summary>
        /// <returns>缓存是否已满，TimedCache永远返回false</returns>
        public override bool IsFull()
        {
            return false; // TimedCache没有容量限制
        }

        /// <summary>
        /// 将对象加入到缓存，使用指定失效时长
        /// 如果缓存空间满了，<see cref="Prune()"/> 将被调用以获得空间来存放新对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="timeout">失效时长，单位毫秒</param>
        public override void Put(K key, V value, long timeout)
        {
            // 当timeout为0时，特殊处理，不添加到缓存
            if (timeout == 0)
            {
                return;
            }
            base.Put(key, value, timeout);
        }

        /// <summary>
        /// 定时清理
        /// </summary>
        /// <param name="delay">间隔时长，单位毫秒</param>
        public void SchedulePrune(long delay)
        {
            // 取消之前的定时任务
            CancelPruneSchedule();

            pruneJobCancellationTokenSource = new CancellationTokenSource();
            var token = pruneJobCancellationTokenSource.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(delay), token);
                    if (!token.IsCancellationRequested)
                    {
                        Prune();
                    }
                }
            }, token);
        }

        /// <summary>
        /// 取消定时清理
        /// </summary>
        public void CancelPruneSchedule()
        {
            if (pruneJobCancellationTokenSource != null)
            {
                pruneJobCancellationTokenSource.Cancel();
                pruneJobCancellationTokenSource.Dispose();
                pruneJobCancellationTokenSource = null;
            }
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            CancelPruneSchedule();
            base.Clear();
        }
    }
}

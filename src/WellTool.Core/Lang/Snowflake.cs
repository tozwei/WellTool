using System;

namespace WellTool.Core.Lang
{
    public class Snowflake
    {
        private const long EPOCH = 1609459200000L; // 2021-01-01 00:00:00 UTC
        private const long WORKER_ID_BITS = 5L;
        private const long DATACENTER_ID_BITS = 5L;
        private const long SEQUENCE_BITS = 12L;

        private const long MAX_WORKER_ID = -1L ^ (-1L << (int)WORKER_ID_BITS);
        private const long MAX_DATACENTER_ID = -1L ^ (-1L << (int)DATACENTER_ID_BITS);

        private const long WORKER_ID_SHIFT = SEQUENCE_BITS;
        private const long DATACENTER_ID_SHIFT = SEQUENCE_BITS + WORKER_ID_BITS;
        private const long TIMESTAMP_SHIFT = SEQUENCE_BITS + WORKER_ID_BITS + DATACENTER_ID_BITS;
        private const long SEQUENCE_MASK = -1L ^ (-1L << (int)SEQUENCE_BITS);

        private long workerId;
        private long datacenterId;
        private long sequence = 0L;
        private long lastTimestamp = -1L;

        public Snowflake(long workerId, long datacenterId)
        {
            if (workerId > MAX_WORKER_ID || workerId < 0)
            {
                throw new ArgumentException($"Worker ID must be between 0 and {MAX_WORKER_ID}");
            }
            if (datacenterId > MAX_DATACENTER_ID || datacenterId < 0)
            {
                throw new ArgumentException($"Datacenter ID must be between 0 and {MAX_DATACENTER_ID}");
            }
            this.workerId = workerId;
            this.datacenterId = datacenterId;
        }

        public long NextId()
        {
            lock (this)
            {
                long timestamp = TimeGen();

                if (timestamp < lastTimestamp)
                {
                    throw new InvalidOperationException($"Clock moved backwards. Refusing to generate id for {lastTimestamp - timestamp} milliseconds");
                }

                if (lastTimestamp == timestamp)
                {
                    sequence = (sequence + 1) & SEQUENCE_MASK;
                    if (sequence == 0)
                    {
                        timestamp = TilNextMillis(lastTimestamp);
                    }
                }
                else
                {
                    sequence = 0L;
                }

                lastTimestamp = timestamp;

                return ((timestamp - EPOCH) << (int)TIMESTAMP_SHIFT)
                    | (datacenterId << (int)DATACENTER_ID_SHIFT)
                    | (workerId << (int)WORKER_ID_SHIFT)
                    | sequence;
            }
        }

        private long TilNextMillis(long lastTimestamp)
        {
            long timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        private long TimeGen()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}
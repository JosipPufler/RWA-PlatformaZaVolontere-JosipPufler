﻿namespace RWA.BL.BLModels
{
    public class BlLog
    {
        public int Idlog { get; set; }

        public DateTime Timestamp { get; set; }

        public int Level { get; set; }

        public string Message { get; set; } = null!;
    }
}

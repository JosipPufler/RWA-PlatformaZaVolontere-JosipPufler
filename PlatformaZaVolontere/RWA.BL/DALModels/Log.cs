using System;
using System.Collections.Generic;

namespace RWA.BL.DALModels;

public partial class Log
{
    public int Idlog { get; set; }

    public DateTime Timestamp { get; set; }

    public int Level { get; set; }

    public string Message { get; set; } = null!;
}

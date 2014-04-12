using System;

namespace MtgDbAdminDriver
{
    public class Ruling
    {
        public string ReleasedAt { get; set; }
        public string Rule       { get; set; }
    }

    public class Format
    {
        public string Name              { get; set; }
        public string Legality          { get; set; }
    }
}


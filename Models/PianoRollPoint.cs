using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow
{
    class PianoRollPoint
    {
        public long Tick { get; set; }
        public long BeatNumber { get; set; }
        public long DenominatolTicksBefore { get; set; }

        public long NextWholeBeatNumber { get; set; }
        public long NextWholeBeatDistance { get; set; }
    }
}

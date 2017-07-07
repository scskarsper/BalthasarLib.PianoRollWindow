using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow.DrawUtils
{
    public class DrawUtils
    {
        internal BalthasarLib.D2DPainter.D2DPaintEventArgs baseEvent;
        BalthasarLib.D2DPainter.D2DPaintEventArgs D2DPaintEventArgs { get; set; }
        internal DrawUtils(BalthasarLib.D2DPainter.D2DPaintEventArgs e, RollConfigures rconf)
        {
            baseEvent = e;
        }
    }
}

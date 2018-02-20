using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow.DrawUtils
{
    public class DrawUtils
    {
        internal BalthasarLib.D2DPainter.D2DPaintEventArgs baseEvent;
        public BalthasarLib.D2DPainter.D2DPaintEventArgs D2DPaintEventArgs { get { return baseEvent; } set { baseEvent = value; } }
        internal RollConfigures rconf;
        internal DrawUtils(BalthasarLib.D2DPainter.D2DPaintEventArgs e, RollConfigures rconf)
        {
            baseEvent = e;
            this.rconf=rconf;
        }

    }
}

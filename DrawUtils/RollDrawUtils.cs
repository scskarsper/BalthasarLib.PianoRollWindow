using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow.DrawUtils
{
    public class RollDrawUtils : DrawUtils
    {
        internal RollDrawUtils(BalthasarLib.D2DPainter.D2DPaintEventArgs e, RollConfigures rconf, PianoProperties pprops)
            : base(e,rconf)
        {
        }
    }
}

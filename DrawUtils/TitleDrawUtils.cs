using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow.DrawUtils
{
    public class TitleDrawUtils : DrawUtils
    {
        internal TitleDrawUtils(BalthasarLib.D2DPainter.D2DPaintEventArgs e, RollConfigures rconf)
            : base(e,rconf)
        {
            baseEvent = e;
        }
    }
}

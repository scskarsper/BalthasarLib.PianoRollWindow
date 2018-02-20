using BalthasarLib.D2DPainter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow.DrawUtils
{
    public class ParamBtnsDrawUtils : DrawUtils
    {
        internal ParamBtnsDrawUtils(BalthasarLib.D2DPainter.D2DPaintEventArgs e, RollConfigures rconf)
            : base(e,rconf)
        {
            baseEvent = e;
        }

        public void DrawString(System.Drawing.Point LeftTopAxis, System.Drawing.Color FontColor, string Text)
        {
            D2DGraphics g = baseEvent.D2DGraphics;
            g.DrawText(Text, new System.Drawing.Rectangle(LeftTopAxis.X, LeftTopAxis.Y, baseEvent.ClipRectangle.Width - LeftTopAxis.X, baseEvent.ClipRectangle.Height - LeftTopAxis.Y), FontColor, new System.Drawing.Font("Tahoma", 9));
        }
    }
}

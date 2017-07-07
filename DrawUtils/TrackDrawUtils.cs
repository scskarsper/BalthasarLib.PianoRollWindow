using BalthasarLib.D2DPainter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow.DrawUtils
{
    public class TrackDrawUtils : DrawUtils
    {
        internal TrackDrawUtils(BalthasarLib.D2DPainter.D2DPaintEventArgs e, RollConfigures rconf, PianoProperties pprops)
            : base(e,rconf)
        {
        }

        public void DrawPianoMouseAxis()
        {
            D2DGraphics g = baseEvent.D2DGraphics;
            //横线
            Point L1_p1 = new Point(baseEvent.ClipRectangle.Left, baseEvent.MousePoint.Y);
            Point L1_p2 = new Point(baseEvent.ClipRectangle.Left+baseEvent.ClipRectangle.Width, baseEvent.MousePoint.Y);
            //竖线
            Point L2_p1 = new Point(baseEvent.MousePoint.X, baseEvent.ClipRectangle.Top);
            Point L2_p2 = new Point(baseEvent.MousePoint.X, baseEvent.ClipRectangle.Top+baseEvent.ClipRectangle.Height);
            g.DrawLine(L1_p1, L1_p2, Color.Red, 1);
            g.DrawLine(L2_p1, L2_p2, Color.Red, 1);
        }
    }
}

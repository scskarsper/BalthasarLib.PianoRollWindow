using BalthasarLib.D2DPainter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow.DrawUtils
{
    public class ParamAreaDrawUtils : DrawUtils
    {
        PianoProperties pprops;
        BalthasarLib.D2DPainter.D2DPaintEventArgs D2DArgs;
        internal ParamAreaDrawUtils(BalthasarLib.D2DPainter.D2DPaintEventArgs e, RollConfigures rconf, PianoProperties pprops)
            : base(e,rconf)
        {
            
            this.pprops = pprops;
            this.D2DArgs = e;
        }


        private Point Tick2Point(long tick, SortedDictionary<long, double> DPair, long MinTick, long MaxTick)
        {
            long ETick = tick - MinTick;//获得左边界距离启绘点距离；
            int NodeXPixel = baseEvent.ClipRectangle.X;      
            if (ETick >= 0)
            {
                long draw_pixel = (long)Math.Round(pprops.dertTick2dertPixel(ETick), 0);
                NodeXPixel = baseEvent.ClipRectangle.X + (int)draw_pixel;
            }

            int NodeYPixel =  (int)((1 - DPair[tick]) * D2DArgs.ClipRectangle.Height);
            if (NodeYPixel < 0) NodeYPixel = 0;
            return new Point(NodeXPixel, NodeYPixel);
           // return new Point((int)(NodeXPixel*1.265), NodeYPixel);
        }
        public void DrawPitchLine(SortedDictionary<long, double> SortedPitchPointSilk, Color AreaColor)
        {
            //计算X相位边界
            long MinTick = pprops.PianoStartTick;
            long MaxTick = pprops.PianoStartTick + (long)Math.Round(pprops.dertPixel2dertTick(baseEvent.ClipRectangle.Width), 0) + 1;
            
            List<Point> PixelSilkLine = new List<Point>();
            bool First = true;
            long[] KeyArr=SortedPitchPointSilk.Keys.ToArray();

            
            Point PS = Tick2Point(KeyArr[0], SortedPitchPointSilk, MinTick, MaxTick);
            PS.Y=0;
            Point PE = Tick2Point(KeyArr[KeyArr.Length-1], SortedPitchPointSilk, MinTick, MaxTick);
            PE.Y = baseEvent.ClipRectangle.Height;
            Rectangle rb = new Rectangle(PS.X, PS.Y, PE.X - PS.X, PE.Y - PS.Y);
            PS.Y = baseEvent.ClipRectangle.Height;

            D2DGraphics g = baseEvent.D2DGraphics;
            g.FillRectangle(rb, Color.Black);

            PixelSilkLine.Add(PS);

            for (int i = 0; i < KeyArr.Length; i++)
            {
                if (KeyArr[i] > MinTick && KeyArr[i] < MaxTick)
                {
                    Point P = Tick2Point(KeyArr[i], SortedPitchPointSilk, MinTick, MaxTick);
                    PixelSilkLine.Add(P);
                }
            }
            PixelSilkLine.Add(PE);

            if (PixelSilkLine.Count > 1) g.FillPathGeometrySink(PixelSilkLine, AreaColor);
        }
        
    }
}

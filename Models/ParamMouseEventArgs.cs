using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BalthasarLib.PianoRollWindow
{
    public class ParamMouseEventArgs
    {
        MouseEventArgs _me;
        public MouseEventArgs MouseEventArgs
        {
            get{return _me;}
        }
        public ParamMouseEventArgs(MouseEventArgs e)
        {
            _me = e;
        }
        internal enum AreaType
        {
            None,
            Area,
            Btns
        }
        private AreaType _area;
        internal AreaType Area { get { return _area; } }

        private long _tick;
        public long Tick { get { return _tick; } }

        private double _tallPercent;
        public double TallPercent { get { return _tallPercent; } }

        internal long windowheight=0;

        internal void CalcAxis(PianoProperties pprops, RollConfigures rconf, ParamMouseEventArgs cache,long windowheight)
        {
            if (cache!=null && cache.MouseEventArgs.X == _me.X && cache.MouseEventArgs.Y == _me.Y)
            {
                _tick = cache.Tick;
                _area = cache.Area;
                if (windowheight == cache.windowheight)
                {
                    _tallPercent = cache._tallPercent;
                }
                else
                {
                    _tallPercent = CalcPercent(_me.Y, windowheight);
                    cache._tallPercent = _tallPercent;
                    cache.windowheight = windowheight;
                }
            }
            else
            {
                CalcAxis(pprops, rconf,windowheight);
            }
        }
        internal void CalcAxis(PianoProperties pprops, RollConfigures rconf,long windowheight)
        {
            if (_me.X <= rconf.Const_RollWidth && _me.X >= 0)
            {
                _area = AreaType.Btns;
            }
            else
            {
                _area = AreaType.Area;
            }
            if (_area != AreaType.Btns)
            {
                long drawed_pixel = _me.X - rconf.Const_RollWidth;
                _tick = (long)Math.Round((pprops.PianoStartTick + pprops.dertPixel2dertTick(drawed_pixel)), 0);
            }
            _tallPercent = CalcPercent(_me.Y, windowheight);
        }
        internal double CalcPercent(long Y,long wH)
        {
            double Zero = wH;
            double ZH = Zero - Y;
            double bf = ZH / wH;
            return bf;
        }
    }
}

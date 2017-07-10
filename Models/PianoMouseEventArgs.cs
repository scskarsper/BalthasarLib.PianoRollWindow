using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BalthasarLib.PianoRollWindow
{
    public class PianoMouseEventArgs
    {
        MouseEventArgs _me;
        public MouseEventArgs MouseEventArgs
        {
            get{return _me;}
        }
        public PianoMouseEventArgs(MouseEventArgs e)
        {
            _me = e;
        }
        internal enum AreaType
        {
            None,
            Roll,
            Track,
            Title
        }
        private AreaType _area;
        internal AreaType Area { get { return _area; } }

        private PitchValuePair _pitchvp=new PitchValuePair(0,0);
        public PitchValuePair PitchValue { get { return _pitchvp; } }
        
        private long _tick;
        public long Tick { get { return _tick; } }

        internal void CalcAxis(PianoProperties pprops, RollConfigures rconf, PianoMouseEventArgs cache)
        {
            if (cache!=null && cache.MouseEventArgs.X == _me.X && cache.MouseEventArgs.Y == _me.Y)
            {
                _tick = cache.Tick;
                _pitchvp = cache.PitchValue;
                _area = cache.Area;
            }
            else
            {
                CalcAxis(pprops, rconf);
            }
        }
        internal void CalcAxis(PianoProperties pprops, RollConfigures rconf)
        {
            if (_me.Y <= rconf.Const_TitleHeight && _me.Y>=0)
            {
                _area = AreaType.Title;
            }
            else if (_me.X <= rconf.Const_RollWidth && _me.X >= 0)
            {
                _area = AreaType.Roll;
            }
            else
            {
                _area = AreaType.Track;
            }
            if (_area != AreaType.Title)
            {
                double drawed_noteSpt = (double)(_me.Y-rconf.Const_TitleHeight) / rconf.Const_RollNoteHeight;
                uint _noteNumber = pprops.PianoTopNote - (uint)drawed_noteSpt;
                double cent=0.5-drawed_noteSpt+(uint)drawed_noteSpt;
                int _notePitchWheel = (int)Math.Round(cent * 0x2000, 0);
                _pitchvp=new PitchValuePair(_noteNumber,_notePitchWheel);
                _pitchvp.OctaveType = pprops.OctaveType;
            }
            if (_area != AreaType.Roll)
            {
                long drawed_pixel = _me.X - rconf.Const_RollWidth;
                _tick=(long)Math.Round((pprops.PianoStartTick+pprops.dertPixel2dertTick(drawed_pixel)),0);
            }
        }
    }
}

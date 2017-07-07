using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow
{
    public class PitchValuePair
    {
        double _pv;
        uint _nn;
        int _pw;
        public double PitchValue { get { return _pv; } }
        public uint NoteNumber { get { return _nn; } }
        public int PitchWheel { get { return _pw; } }
        public PitchValuePair(uint NoteNumber, int PitchWheel)
        {
            _nn = NoteNumber;
            _pw = PitchWheel;
            _pv = ((double)_pw / 0x2000) + _nn;
        }
        public PitchValuePair(double PitchValue)
        {
            _pv = PitchValue;

            _nn = (uint)_pv;
            double pr = _pv-_nn;
            if (pr > 0.5)
            {
                _nn = _nn + 1;
                pr = _pv - _nn;
            }

            _pw = (int)Math.Round(pr * 0x2000,0);
        }
    }
}

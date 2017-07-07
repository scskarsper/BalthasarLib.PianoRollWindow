using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow
{
    class CurrentConfigure
    {
        public PianoProperties PianoProps = new PianoProperties();
        RollConfigures rconf;
        public CurrentConfigure(RollConfigures rconf)
        {
            this.rconf = rconf;
        }
        private int _CurrentTopNote=127;

        public int CurrentTopNote
        {
            get
            {
                return _CurrentTopNote;
            }
            set
            {
                if (value > rconf.MaxNoteNumber)
                {
                    _CurrentTopNote = rconf.MaxNoteNumber;
                }
                else if (value < rconf.MinNoteNumber)
                {
                    _CurrentTopNote = rconf.MinNoteNumber;
                }
                else
                {
                    _CurrentTopNote = value;
                }
            }
        }

        internal PianoRollPoint getPianoStartPoint()
        {
            //获取当前信息
            long BeatCountBefore = PianoProps.PianoStartTick / PianoProps.BeatLength;//获取之前有几个整拍子
            uint BeatDenominatolBefore = PianoProps.PianoStartTick % PianoProps.BeatLength;//获取余数拍子

            PianoRollPoint ret = new PianoRollPoint();
            ret.Tick = PianoProps.PianoStartTick;
            ret.BeatNumber = BeatCountBefore;
            ret.DenominatolTicksBefore = BeatDenominatolBefore;
            ret.NextWholeBeatNumber = ret.BeatNumber + 1;
            ret.NextWholeBeatDistance = PianoProps.BeatLength - BeatDenominatolBefore;
            return ret;
        }
    }
    class PianoRollPoint
    {
        public long Tick { get; set; }
        public long BeatNumber { get; set; }
        public uint DenominatolTicksBefore { get; set; }

        public long NextWholeBeatNumber { get; set; }
        public uint NextWholeBeatDistance { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow
{
    public class PianoProperties
    {
        RollConfigures rconf;
        /// <summary>
        /// Numerator of Beats
        /// How much Beats in one Summery
        /// </summary>
        public uint BeatsCountPerSummery {get;set;}
        /// <summary>
        /// Denominator of Beats
        //  Use one type of Note as a Beat
        /// </summary>
        public NoteType BeatType { get; set; }
        public enum NoteType
        {
            Semibreve=1,//全音符
            Minim=2,//二分音符
            Crotchet=4,//四分音符
            Quaver=8,//八分音符
            Demiquaver=16,//十六分音符
            Demisemiquaver=32//三十二分音符
        }
        private const uint SemibreveLength=1920;
        internal uint BeatLength
        {
            get
            {
                return (uint)(SemibreveLength / (uint)BeatType);
            }
        }

        internal PianoProperties(RollConfigures rconf)
        {
            BeatsCountPerSummery = 4;
            BeatType = NoteType.Crotchet;
            this.rconf = rconf;
        }

        private uint _crotchetLengthPixel = 66;
        public uint CrotchetLengthPixel
        {
            get { return _crotchetLengthPixel; }
            set
            {
                if (value < 3)
                {
                    _crotchetLengthPixel = 3;
                }
                else
                {
                    _crotchetLengthPixel = value;
                }
            } 
        }
        public double dertTick2dertPixel(long dertTick)
        {
            double PixelPerTick = (double)(_crotchetLengthPixel*4) / SemibreveLength;
            double ret = dertTick * PixelPerTick;
            return ret;
        }
        public double dertPixel2dertTick(long dertPixel)
        {
            double PixelPerTick = (double)(_crotchetLengthPixel * 4) / SemibreveLength;
            double ret = dertPixel / PixelPerTick;
            return ret;
        }

        private long _pianoStartTick = 0;
        internal long PianoStartTick
        {
            get
            {
                return _pianoStartTick;
            }
            set
            {
                if(value!=_pianoStartTick)
                {
                    _pianoStartTick = value;
                }
            }
        }

        private uint _pianoTopNote = 127;
        internal uint PianoTopNote
        {
            get
            {
                return _pianoTopNote;
            }
            set
            {
                if (value > rconf.MaxNoteNumber)
                {
                    _pianoTopNote = (uint)rconf.MaxNoteNumber;
                }
                else if (value < rconf.MinNoteNumber)
                {
                    _pianoTopNote = (uint)rconf.MinNoteNumber;
                }
                else
                {
                    _pianoTopNote = value;
                }
            }
        }

        internal PianoRollPoint getPianoStartPoint()
        {
            //获取当前信息
            long BeatCountBefore = PianoStartTick / BeatLength;//获取之前有几个整拍子
            long BeatDenominatolBefore = PianoStartTick % BeatLength;//获取余数拍子

            PianoRollPoint ret = new PianoRollPoint();
            ret.Tick = PianoStartTick;
            ret.BeatNumber = BeatCountBefore;
            ret.DenominatolTicksBefore = BeatDenominatolBefore;
            ret.NextWholeBeatNumber = ret.BeatNumber + 1;
            ret.NextWholeBeatDistance = BeatLength - BeatDenominatolBefore;
            return ret;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow
{
    public class PianoProperties
    {
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
    
        public PianoProperties()
        {
            BeatsCountPerSummery = 4;
            BeatType = NoteType.Crotchet;
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
        public long dertTick2dertPixel(long dertTick)
        {
            double PixelPerTick = (double)(_crotchetLengthPixel*4) / SemibreveLength;
            double ret = dertTick * PixelPerTick;
            return (long)ret;
        }
        public long dertPixel2dertTick(long dertPixel)
        {
            double PixelPerTick = (double)(_crotchetLengthPixel * 4) / SemibreveLength;
            double ret = dertPixel/PixelPerTick;
            return (long)ret;
        }

        private uint _pianoStartTick=0;
        public uint PianoStartTick
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
    }
}

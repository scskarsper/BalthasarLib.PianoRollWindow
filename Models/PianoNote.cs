using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow.Models
{
    public class PianoNote
    {
        /// <summary>
        /// INIT 初始化
        /// </summary>
        #region
        private const uint SemibreveLength = 1920;

        public PianoNote(long Tick,BalthasarLib.PianoRollWindow.PianoProperties.NoteType NoteType,PitchValuePair PitchValue)
        {
            this.pvp = PitchValue;
            this.Tick = Tick;
            this.Length = (uint)(SemibreveLength / (uint)NoteType);
        }
        public PianoNote(long Tick, BalthasarLib.PianoRollWindow.PianoProperties.NoteType NoteType, long Length, double PitchValue)
        {
            this.pvp = new PitchValuePair(PitchValue);
            this.Tick = Tick;
            this.Length = (uint)(SemibreveLength / (uint)NoteType);
        }
        public PianoNote(long Tick, BalthasarLib.PianoRollWindow.PianoProperties.NoteType NoteType, long Length, uint NoteNumber, int PitchWheel)
        {
            this.pvp = new PitchValuePair(NoteNumber, PitchWheel);
            this.Tick = Tick;
            this.Length = (uint)(SemibreveLength / (uint)NoteType);
        }

        public PianoNote(long Tick, BalthasarLib.PianoRollWindow.PianoProperties.NoteType NoteType, PitchValuePair PitchValue, string Lyric)
        {
            this.Lyric = Lyric;
            this.pvp = PitchValue;
            this.Tick = Tick;
            this.Length = (uint)(SemibreveLength / (uint)NoteType);
        }
        public PianoNote(long Tick, BalthasarLib.PianoRollWindow.PianoProperties.NoteType NoteType, long Length, double PitchValue, string Lyric)
        {
            this.Lyric = Lyric;
            this.pvp = new PitchValuePair(PitchValue);
            this.Tick = Tick;
            this.Length = (uint)(SemibreveLength / (uint)NoteType);
        }
        public PianoNote(long Tick, BalthasarLib.PianoRollWindow.PianoProperties.NoteType NoteType, long Length, uint NoteNumber, int PitchWheel, string Lyric)
        {
            this.Lyric=Lyric;
            this.pvp = new PitchValuePair(NoteNumber, PitchWheel);
            this.Tick = Tick;
            this.Length = (uint)(SemibreveLength / (uint)NoteType);
        }

        public PianoNote(long Tick, long Length, double PitchValue)
        {
            this.pvp = new PitchValuePair(PitchValue);
            this.Tick = Tick;
            this.Length = Length;
        }
        public PianoNote(long Tick, long Length, uint NoteNumber, int PitchWheel)
        {
            this.pvp = new PitchValuePair(NoteNumber, PitchWheel);
            this.Tick = Tick;
            this.Length = Length;
        }
        public PianoNote(long Tick, long Length,PitchValuePair PitchValue)
        {
            this.pvp = PitchValue;
            this.Tick = Tick;
            this.Length = Length;
        }

        public PianoNote(long Tick, long Length, double PitchValue, string Lyric)
        {
            this.pvp = new PitchValuePair(PitchValue);
            this.Tick = Tick;
            this.Length = Length;
            this.Lyric = Lyric;
        }
        public PianoNote(long Tick, long Length, uint NoteNumber, int PitchWheel, string Lyric)
        {
            this.pvp = new PitchValuePair(NoteNumber, PitchWheel);
            this.Tick = Tick;
            this.Length = Length;
            this.Lyric = Lyric;
        }
        public PianoNote(long Tick, long Length, PitchValuePair PitchValue,string Lyric)
        {
            this.pvp = PitchValue;
            this.Tick = Tick;
            this.Length = Length;
            this.Lyric = Lyric;
        }

        #endregion


        public long Tick { get; set; }
        public long Length { get; set; }
        string _lyc = "";
        public string Lyric {
            get { return _lyc;}
            set { _lyc = value; }
        }
        PitchValuePair pvp = new PitchValuePair(60);
        public PitchValuePair PitchValue
        {
            get
            {
                return pvp;
            }
            set
            {
                pvp = value;
            }
        }
    }
}

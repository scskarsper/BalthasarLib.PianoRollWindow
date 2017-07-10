using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow.Models
{
    public class PitchNode
    {
        public long Tick { get; set; }
        public long Length { get; set; }
        public BalthasarLib.PianoRollWindow.PitchValuePair.OctaveTypeEnum OctaveType { get { return this.pvp.OctaveType; } set { this.pvp.OctaveType = value; } }
        public PitchNode(long Tick, double PitchValue)
        {
            this.pvp = new PitchValuePair(PitchValue);
            this.Tick = Tick;
        }
        public PitchNode(long Tick, uint NoteNumber, int PitchWheel)
        {
            this.pvp = new PitchValuePair(NoteNumber, PitchWheel);
            this.Tick = Tick;
        }
        public PitchNode(long Tick, PitchValuePair PitchValue)
        {
            this.pvp = PitchValue;
            this.Tick = Tick;
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

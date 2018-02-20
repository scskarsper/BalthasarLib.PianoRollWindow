using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow.Models
{
    public class ParamValuePair
    {
        private bool _isBindedNote = false;

        public bool IsBindedNote
        {
            get { return _isBindedNote; }
        }
        public long Tick;
        public long Value;
        public PianoNote PNote;
        public ParamValuePair(long Tick, long Value)
        {
            this.Tick = Tick;
            this.Value = Value;
            _isBindedNote = false;
        }
        public ParamValuePair(PianoNote Note, long Value)
        {
            this.PNote = Note;
            this.Tick = PNote.Tick;
            this.Value = Value;
            _isBindedNote = true;
        }

    }
}

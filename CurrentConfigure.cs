using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow
{
    class CurrentConfigure
    {
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

    }
}

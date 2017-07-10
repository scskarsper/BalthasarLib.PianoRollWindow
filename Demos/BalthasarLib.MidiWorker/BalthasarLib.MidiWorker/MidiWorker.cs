using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Midi;
using System.IO;

namespace MIDIReader
{
    public class PitchWheelEvent
    {
        public int PBS;
        public int PIT;
        public double pNotePlus;
        public PitchWheelChangeEvent PitchWheelChangeEvent;
        public PitchWheelEvent(PitchWheelChangeEvent pwce)
        {
            PitchWheelChangeEvent = pwce;
        }
    }
    public class MidiRResult
    {        
        public List<PitchWheelEvent> Pitchs;
        public List<NoteOnEvent> Notes;
        public MidiRResult(List<PitchWheelEvent> p, List<NoteOnEvent> n)
        {
            Pitchs = p;
            Notes = n;
        }
        public MidiRResult()
        {
            Pitchs = new List<PitchWheelEvent>();
            Notes = new List<NoteOnEvent>();
        }
    }
    public class TempoWorker
    {
        public enum NoteType
        {
            Semibreve = 1,//全音符
            Minim = 2,//二分音符
            Crotchet = 4,//四分音符
            Quaver = 8,//八分音符
            Demiquaver = 16,//十六分音符
            Demisemiquaver = 32//三十二分音符
        }

        private const uint SemibreveLength = 1920;
        private double _tempo = 120;
        private uint _beatlength = 480;
        private double _millisecondpertick = 0;
        private double _tickpermillsecond = 0;

        public double Tempo { get { return _tempo; } set { _tempo = value; calcRuler(); } }//BPM：BeatsPerMinute
        public uint BeatLength { get { return _beatlength; } set { _beatlength = value; calcRuler(); } }
        
        private void calcRuler()
        {
            _millisecondpertick = 60000 / (_tempo * _beatlength);
            _tickpermillsecond = (_tempo * _beatlength) / 60000;
        }
        
        public TempoWorker(double Tempo, uint OneBeatLength)
        {
            this._tempo = Tempo;
            this._beatlength = OneBeatLength;
            calcRuler();
        }
        public TempoWorker(double Tempo, NoteType BeatType)
        {
            this._tempo = Tempo;
            this._beatlength = (uint)(SemibreveLength / (uint)BeatType);
            calcRuler();
        }
        public TempoWorker(NoteType BeatType)
        {
            this._tempo = 120;
            this._beatlength = (uint)(SemibreveLength / (uint)BeatType);
            calcRuler();
        }
        public TempoWorker(uint OneBeatLength)
        {
            this._tempo = 120;
            this._beatlength = OneBeatLength;
            calcRuler();
        }
        public TempoWorker(double Tempo)
        {
            this._tempo = Tempo;
            this._beatlength = 480;
            calcRuler();
        }
        public TempoWorker()
        {
            this._tempo = 120;
            this._beatlength = 480;
            calcRuler();
        }

        public double Tick2Millisecond(double Tick)
        {
            return Tick * _millisecondpertick;
        }
        public double Millisecond2Tick(double Millisecond)
        {
            return Millisecond * _tickpermillsecond;
        }
    }
    public class MidiWorker
    {
        public static MidiRResult ReadMidi(string MidiPath,string TrackName)
        {
            MidiFile midifile = new MidiFile(MidiPath);
            List<PitchWheelEvent> Pitchs = new List<PitchWheelEvent>();
            List<NoteOnEvent> Notes = new List<NoteOnEvent>();
            for (int t = 0; t < midifile.Tracks; t++)
            {
                List<MidiEvent> Events = (List<MidiEvent>)midifile.Events[t];
                for (int i = 0; i < Events.Count; i++)
                {
                    if (Events[i].GetType() == typeof(TextEvent))
                    {
                        TextEvent te = (TextEvent)Events[i];
                        if (te.MetaEventType == MetaEventType.SequenceTrackName)
                        {
                            string trackname = te.Text;
                            if (trackname != TrackName)
                            {
                                Pitchs.Clear();
                                Notes.Clear();
                                break;
                            }
                        }
                    }
                    else if (Events[i].GetType() == typeof(PitchWheelChangeEvent))
                    {
                        PitchWheelChangeEvent pw = (PitchWheelChangeEvent)Events[i];
                        PitchWheelEvent pwe=new PitchWheelEvent(pw);
                        pwe.PBS=12;
                        int EachNote = 8192 / 12;
                        pwe.PIT=pwe.PitchWheelChangeEvent.Pitch-8192;
                        pwe.pNotePlus=pwe.PIT/EachNote;
                        Pitchs.Add(pwe);
                    }
                    else if (Events[i].GetType() == typeof(NoteOnEvent))
                    {
                        NoteOnEvent ne = (NoteOnEvent)Events[i];
                        if (ne.Velocity != 0)
                        {
                            Notes.Add(ne);
                        }
                    }
                }
            }
            return new MidiRResult(Pitchs,Notes);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BalthasarLib.PianoRollWindow;
using BalthasarLib.PianoRollWindow.Models;
using MIDIReader;
using NAudio.Midi;

namespace Demo.MIDIViewer
{
    public partial class Form1 : Form
    {
        List<PianoNote> NoteList = new List<PianoNote>();
        Dictionary<int, List<PitchNode>> PitchList = new Dictionary<int, List<PitchNode>>();
        public Form1()
        {
            InitializeComponent();
            pianoRollWindow1.setCrotchetSize((uint)trackBar1.Value);
            pianoRollWindow1.OctaveType = PitchValuePair.OctaveTypeEnum.Piano;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            pianoRollWindow1.setPianoStartTick(hScrollBar1.Value);
        }


        private void pianoRollWindow1_TrackPaint(object sender, BalthasarLib.PianoRollWindow.DrawUtils.TrackDrawUtils utils)
        {
            long mt=pianoRollWindow1.MaxShownTick;
            long nt=pianoRollWindow1.MinShownTick;
            for(int i=0;i<NoteList.Count;i++)
            {
                PianoNote PN=NoteList[i];
                if(PN.Tick+PN.Length>nt && PN.Tick<mt)
                {
                    utils.DrawNote(PN, Color.SkyBlue);
                }
            }
            foreach (KeyValuePair<int, List<PitchNode>> KV in PitchList)
            {
                utils.DrawPitchLine(KV.Value, Color.Red);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pianoRollWindow1.setCrotchetSize((uint)trackBar1.Value);
        }


        void LoadMidi(string MidFile,string Track)
        {
            TempoWorker tempo = new TempoWorker(120, TempoWorker.NoteType.Crotchet);
            MidiRResult MidiRecv = MidiWorker.ReadMidi(MidFile, Track);
            NoteOnEvent FN = MidiRecv.Notes[0];
            long off = FN.AbsoluteTime;
            for (int i = 0; i < MidiRecv.Notes.Count; i++)
            {
                NoteOnEvent NoteEvent = MidiRecv.Notes[i];
                PianoNote PNote = new PianoNote(
                        NoteEvent.AbsoluteTime - off,
                        NoteEvent.NoteLength,
                        new BalthasarLib.PianoRollWindow.PitchValuePair((uint)NoteEvent.NoteNumber, 0)
                        );
                PNote.OctaveType = PitchValuePair.OctaveTypeEnum.Piano;
                PNote.Lyric = PNote.PitchValue.NoteChar + PNote.PitchValue.Octave.ToString();
                NoteList.Add(PNote);
            }
            int j = 0;
            int k = 0;
            PitchList.Add(k, new List<PitchNode>());
            for (int i = 0; i < MidiRecv.Pitchs.Count; i++)
            {
                if (j >= MidiRecv.Notes.Count) break;
                if (MidiRecv.Pitchs[i].PitchWheelChangeEvent.AbsoluteTime - off < NoteList[j].Tick) continue;//在音符前，抛弃
                if (MidiRecv.Pitchs[i].PitchWheelChangeEvent.AbsoluteTime - off > NoteList[j].Tick + NoteList[j].Length)
                {
                    //在音符后
                    j++;
                    k++;
                    i--;//重来一遍
                    PitchList.Add(k, new List<PitchNode>());
                    continue;
                }
                PitchValuePair pvp = new PitchValuePair((uint)MidiRecv.Notes[j].NoteNumber, MidiRecv.Pitchs[i].PIT, (uint)MidiRecv.Pitchs[i].PBS);
                PitchNode PitN = new PitchNode(MidiRecv.Pitchs[i].PitchWheelChangeEvent.AbsoluteTime - off, pvp.PitchValue);
                PitchList[k].Add(PitN);
            }
        }
        private void PlayNote(int NoteNumber)
        {
            System.Threading.Thread th=new System.Threading.Thread(new System.Threading.ParameterizedThreadStart((note)=>{
                try
                {
                    using (MidiOut midiOut = new MidiOut(0))
                    {
                        midiOut.Volume = 65535;
                        midiOut.Send(MidiMessage.StartNote(NoteNumber, 127, 1).RawData);
                        System.Threading.Thread.Sleep(1000);
                        midiOut.Send(MidiMessage.StopNote(NoteNumber, 127, 1).RawData);
                    }
                }
                catch { ;}
            }));
            th.Start(NoteNumber);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.mid|*.mid";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
//                string filename = @"E:\GitPool\PianoWindows\BalthasarLib.PianoRollWindow\Demos\test.mid";
                string filename = ofd.FileName;
                string track = "Voice1";
                PlayMidi(filename, track);
                LoadMidi(filename, track);
            }

        }
        void PlayMidi(string MidFile, string Track)
        {
            List<PianoNote> NoteListEX = new List<PianoNote>();
            TempoWorker tempo = new TempoWorker(120, TempoWorker.NoteType.Crotchet);
            MidiRResult MidiRecv = MidiWorker.ReadMidi(MidFile, Track);
            NoteOnEvent FN = MidiRecv.Notes[0];
            long off = FN.AbsoluteTime;
            long lastEnd = 0;
            for (int i = 0; i < MidiRecv.Notes.Count; i++)
            {
                NoteOnEvent NoteEvent = MidiRecv.Notes[i];
                PianoNote PNote = new PianoNote(
                        NoteEvent.AbsoluteTime - off,
                        NoteEvent.NoteLength,
                        new BalthasarLib.PianoRollWindow.PitchValuePair((uint)NoteEvent.NoteNumber, 0)
                        );
                PNote.OctaveType = PitchValuePair.OctaveTypeEnum.Piano;
                PNote.Lyric = PNote.PitchValue.NoteChar + PNote.PitchValue.Octave.ToString();
                if (PNote.Tick > lastEnd)
                {
                    NoteListEX.Add(new PianoNote(lastEnd, PNote.Tick - lastEnd, 0));
                }
                NoteListEX.Add(PNote);
            }
            using (MidiOut midiOut = new MidiOut(0))
            {
                midiOut.Volume = 65535;
                    int j = 0;
                    int k = 0;
                    bool isp = false;
                    for (int i = 0; i < MidiRecv.Pitchs.Count; i++)
                    {
                        if (j >= MidiRecv.Notes.Count) break;
                        if (MidiRecv.Pitchs[i].PitchWheelChangeEvent.AbsoluteTime - off < 0) continue;
                        if (MidiRecv.Pitchs[i].PitchWheelChangeEvent.AbsoluteTime - off < NoteListEX[j].Tick)
                        {
                            System.Threading.Thread.Sleep((int)tempo.Tick2Millisecond(MidiRecv.Pitchs[i].PitchWheelChangeEvent.DeltaTime));
                            continue;//在音符前，抛弃
                        }
                        if (MidiRecv.Pitchs[i].PitchWheelChangeEvent.AbsoluteTime - off > NoteListEX[j].Tick + NoteListEX[j].Length)
                        {
                            //在音符后
                            midiOut.Send(MidiMessage.StopNote((int)NoteListEX[j].PitchValue.NoteNumber, 0, 1).RawData);
                            isp = false;
                            j++;
                            k++;
                            i--;//重来一遍
                            PitchList.Add(k, new List<PitchNode>());
                            continue;
                        }
                        if (!isp)
                        {
                            isp = true;
                            midiOut.Send(MidiMessage.StartNote((int)NoteListEX[j].PitchValue.NoteNumber, 127, 1).RawData);
                        }
                        midiOut.Send(MidiRecv.Pitchs[i].PitchWheelChangeEvent.GetAsShortMessage());
                        System.Threading.Thread.Sleep((int)tempo.Tick2Millisecond(MidiRecv.Pitchs[i].PitchWheelChangeEvent.DeltaTime));
                            
                    }
            }
        }

        private void pianoRollWindow1_RollMouseClick(object sender, PianoMouseEventArgs e)
        {
        }

        private void pianoRollWindow1_RollMouseDown(object sender, PianoMouseEventArgs e)
        {
            PlayNote((int)e.PitchValue.NoteNumber);
        }
    }
}

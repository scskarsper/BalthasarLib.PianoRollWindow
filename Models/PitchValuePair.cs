﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow
{
    public class PitchValuePair
    {
        private string[] KeyChar = { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B" };
        private bool[] KeyIsBlack = { false, true, false, true, false, false, true, false, true, false, true, false };
        private int getOctave(uint NoteNumber)
        {
            int K = (int)((double)NoteNumber / 12);
            return K - 2;
        }
        private int getKey(uint NoteNumber)
        {
            return (int)NoteNumber % 12;
        }

        double _pv;
        uint _nn;
        int _pw;
        int _key;
        int _octave;
        public enum OctaveTypeEnum
        {
            Piano,
            Voice
        }
        private OctaveTypeEnum otype = OctaveTypeEnum.Voice;
        public OctaveTypeEnum OctaveType { get { return otype; } set { otype = value; } }
        public double PitchValue { get { return _pv; } }
        public uint NoteNumber { get { return _nn; } }
        public int PitchWheel { get { return _pw; } }
        public PitchValuePair(uint NoteNumber, int PitchWheel, uint PBS=1)
        {
            if (PBS < 1) PBS = 1;
            _nn = NoteNumber;
            _pw = PitchWheel;
            if (PitchWheel != 0)
            {
                _pv = (((double)_pw / 0x2000) * PBS) + _nn;
            }
            else
            {
                _pv = _nn;
            }
            _key = getKey(_nn);
            _octave = getOctave(_nn);
        }
        public PitchValuePair(double PitchValue, uint PBS = 1)
        {
            if (PBS < 1) PBS = 1;
            _pv = PitchValue;

            _nn = (uint)_pv;
            double pr = _pv-_nn;
            if (pr > 0.5)
            {
                _nn = _nn + 1;
                pr = _pv - _nn;
            }

            _pw = (int)Math.Round(pr * ((double)0x2000/PBS), 0);
            _key = getKey(_nn);
            _octave = getOctave(_nn);
        }
        public int Octave
        {
             get
             {
                 if (otype == OctaveTypeEnum.Piano)
                 {
                     return _octave+1;
                 }
                 else
                 {
                     return _octave;
                 }
             }
        }
        public int Key
        {
            get
            {
                return _key;
            }
        }
        public bool IsBlackKey
        {
            get
            {
                return KeyIsBlack[_key];
            }
        }
        public string NoteChar
        {
            get
            {
                return KeyChar[_key];
            }
        }
    }
}

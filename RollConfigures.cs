using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BalthasarLib.PianoRollWindow
{
    class RollConfigures
    {
        public int Const_TitleHeight = 28;//标题头大小
        public int Const_TitleHeightSpliter = 3;//标题头分割线先高
        public int Const_TitleLineTop = 6;//标题头分割线先高
        public int Const_TitleRulerTop = 20;//标题头分割线先高
        public int Const_RollWidth = 82;//琴键宽
        public int Const_RollBlackWidth = 48;//琴键黑宽
        public int Const_RollNoteHeight = 13;//琴键高
        public int Const_VScrollBarWidth = 19;
        public int MaxNoteNumber = 127;
        public int MinNoteNumber = 0;

        public Color RollColor_WhiteKey_NormalSound;//正常区域白键
        public Color RollColor_BlackKey_NormalSound;
        public Color RollColor_WhiteKey_OverSound;//超音域区域白键
        public Color RollColor_BlackKey_OverSound;
        public Color RollColor_WhiteKey_NoSound;//无音域区域白键
        public Color RollColor_BlackKey_NoSound;
        public Color RollColor_LineKey_NormalSound;//普通分割线
        public Color RollColor_LineKey_OverSound;//普通分割线
        public Color RollColor_LineKey_NoSound;//普通分割线
        public Color RollColor_LineOctive_OverSound;//大跨度分割线
        public Color RollColor_LineOctive_NormalSound;//大跨度分割线

        public Color PianoColor_WhiteKey;
        public Color PianoColor_BlackKey;
        public Color PianoColor_MouseKey;
        public Color PianoColor_Line;

        public Color TitleColor_Line;
        public Color TitleColor_Ruler;
        public Color TitleColor_Marker;

        public string[] KeyChar = {"C","C#","D","Eb","E","F","F#","G","G#","A","Bb","B" };
        public bool[] KeyIsBlack = {false,true,false,true,false,false,true,false,true,false,true,false };
        public int getOctave(int NoteNumber)
        {
            int K=(int)((double)NoteNumber / 12);
            return K - 2;
        }
        public int getKey(int NoteNumber)
        {
            return NoteNumber % 12;
        }
        public enum VoiceKeyArea
        {
            Normal,
            OverSound,
            NoSound
        }
        public VoiceKeyArea getVoiceArea(int NoteNumber)
        {
            int Oc = getOctave(NoteNumber);
            if (Oc == 0 || Oc==5) return VoiceKeyArea.OverSound;
            if (Oc < 0 || Oc > 5 ) return VoiceKeyArea.NoSound;
            return VoiceKeyArea.Normal;
        }
        public RollConfigures()
        {
            RollColor_WhiteKey_NormalSound = Color.FromArgb(216, 216, 216);
            RollColor_BlackKey_NormalSound = Color.FromArgb(190, 190, 190);
            RollColor_WhiteKey_OverSound = Color.FromArgb(183, 183, 183);
            RollColor_BlackKey_OverSound = Color.FromArgb(142, 142, 142);
            RollColor_WhiteKey_NoSound = Color.FromArgb(155, 155, 155);
            RollColor_BlackKey_NoSound = Color.FromArgb(106, 106, 106);
            RollColor_LineKey_NormalSound = Color.FromArgb(170, 170, 170);
            RollColor_LineKey_OverSound = Color.FromArgb(113, 113, 113);
            RollColor_LineKey_NoSound = Color.FromArgb(75, 75, 75);
            RollColor_LineOctive_OverSound = Color.FromArgb(75, 75, 75);
            RollColor_LineOctive_NormalSound = Color.FromArgb(189, 184, 154);

            PianoColor_WhiteKey = Color.FromArgb(240, 240, 240);
            PianoColor_BlackKey = Color.FromArgb(49, 49, 49);
            PianoColor_Line = Color.FromArgb(220, 220, 220);
            PianoColor_MouseKey = Color.FromArgb(11, 233, 244);

            TitleColor_Line = Color.FromArgb(35, 105, 107);
            TitleColor_Ruler = Color.FromArgb(91,91,91);
            TitleColor_Marker = Color.FromArgb(131,131,131);

        }
    }
}

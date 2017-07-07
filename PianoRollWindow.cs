using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BalthasarLib.D2DPainter;

namespace BalthasarLib.PianoRollWindow
{
    public partial class PianoRollWindow : UserControl
    {
        RollConfigures rconf = new RollConfigures();
        CurrentConfigure cconf;

        public PianoProperties PianoProps
        {
            get
            {
                return cconf.PianoProps;
            }
            set
            {
                cconf.PianoProps = value;
                d2DPainterBox1.Refresh();
            }
        }
        public PianoRollWindow()
        {
            InitializeComponent();
            cconf = new CurrentConfigure(rconf);
            cconf.CurrentTopNote = rconf.MaxNoteNumber - noteScrollBar1.Value;
            InitGUI();
        }

        void InitGUI()
        {
            int noteArea = this.ClientRectangle.Height - rconf.Const_TitleHeight;
            int noteCount = noteArea / rconf.Const_RollNoteHeight;
            int ScrollMax = rconf.MaxNoteNumber - noteCount;
            if (noteScrollBar1.Value > ScrollMax)
            {
                noteScrollBar1.Value = ScrollMax;
                noteScrollBar1_Scroll(null, null);
            }
            noteScrollBar1.Maximum = ScrollMax;
            noteScrollBar1.Height = this.ClientRectangle.Height - rconf.Const_TitleHeight;
            noteScrollBar1.Top = rconf.Const_TitleHeight;
            d2DPainterBox1.Top = 0;
            d2DPainterBox1.Left = 0;
            d2DPainterBox1.Width = this.ClientRectangle.Width - noteScrollBar1.Width;
            d2DPainterBox1.Height = this.ClientRectangle.Height;
        }

        private void d2DPainterBox1_D2DPaint(object sender, BalthasarLib.D2DPainter.D2DPaintEventArgs e)
        {
            DrawPianoArea(e);
            DrawPianoMouseAxis(e);
            DrawPianoRoll(e);
            DrawPianoTitle(e);
        }

        private void DrawPianoMouseAxis(BalthasarLib.D2DPainter.D2DPaintEventArgs e)
        {
            D2DGraphics g = e.D2DGraphics;
            Point L1_p1 = new Point(0, e.MousePoint.Y);
            Point L1_p2 = new Point(e.ClipRectangle.Width, e.MousePoint.Y);
            Point L2_p1 = new Point(e.MousePoint.X, 0);
            Point L2_p2 = new Point(e.MousePoint.X, e.ClipRectangle.Height);
            g.DrawLine(L1_p1, L1_p2, Color.Red, 1);
            g.DrawLine(L2_p1, L2_p2, Color.Red, 1);
        }
        private void DrawPianoArea(BalthasarLib.D2DPainter.D2DPaintEventArgs e)
        {
            D2DGraphics g = e.D2DGraphics;

            int y = rconf.Const_TitleHeight;//绘制点纵坐标
            int cNote = cconf.CurrentTopNote;//绘制区域第一个阶的音符
            while (y < e.ClipRectangle.Height)//若未画超界
            {
                //计算绘制区域
                Point LT = new Point(rconf.Const_RollWidth, y);//矩形左上角
                Point LB = new Point(rconf.Const_RollWidth, y + rconf.Const_RollNoteHeight);//矩形左下角
                Point RT = new Point(e.ClipRectangle.Width, y);//矩形右上角
                Point RB = new Point(e.ClipRectangle.Width, y + rconf.Const_RollNoteHeight);//矩形右下角
                Rectangle Rect = new Rectangle(LT, new Size(e.ClipRectangle.Width - rconf.Const_RollWidth, rconf.Const_RollNoteHeight));//矩形区域
                //计算色域
                int Octave = rconf.getOctave(cNote);
                int Key = rconf.getKey(cNote);
                bool isBlackKey = rconf.KeyIsBlack[Key];
                Color KeyColor = isBlackKey ? rconf.RollColor_BlackKey_NormalSound : rconf.RollColor_WhiteKey_NormalSound;
                Color LineColor = rconf.RollColor_LineKey_NormalSound;
                Color OLineColor = rconf.RollColor_LineOctive_NormalSound;
                switch (rconf.getVoiceArea(cNote))
                {
                    case RollConfigures.VoiceKeyArea.OverSound: KeyColor = isBlackKey ? rconf.RollColor_BlackKey_OverSound : rconf.RollColor_WhiteKey_OverSound; LineColor = rconf.RollColor_LineKey_OverSound; OLineColor = rconf.RollColor_LineOctive_OverSound; break;
                    case RollConfigures.VoiceKeyArea.NoSound: KeyColor = isBlackKey ? rconf.RollColor_BlackKey_NoSound : rconf.RollColor_WhiteKey_NoSound; LineColor = rconf.RollColor_LineKey_NoSound; OLineColor = rconf.RollColor_LineOctive_OverSound; break;
                }
                //绘制矩形
                g.FillRectangle(Rect, KeyColor);
                //绘制边线
                g.DrawLine(LB, RB, (Key == 5 || Key == 0) ? OLineColor : LineColor,2);//isB ? LineL : LineB);
                //g.DrawText(cNote.ToString()+rconf.KeyChar[Key]+Octave.ToString(), Rect, Color.White, new System.Drawing.Font("宋体", 12));
                //递归
                y = y + rconf.Const_RollNoteHeight;
                cNote = cNote - 1;
            }

            Rectangle CurrentRect = new Rectangle(
                rconf.Const_RollWidth,
                rconf.Const_TitleHeight,
                e.ClipRectangle.Width-rconf.Const_RollWidth,
                e.ClipRectangle.Height-rconf.Const_TitleHeight);//可绘制区域
                
            //Rise 绘制Note等//传递事件

            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 10000; i++)
            {
                Point p1 = new Point(rnd.Next(0, e.ClipRectangle.Width), rnd.Next(0, e.ClipRectangle.Height));
                Point p2 = new Point(rnd.Next(0, e.ClipRectangle.Width), rnd.Next(0, e.ClipRectangle.Height));
                g.DrawLine(p1, p2, Color.Red);
            }
        }
        private void DrawPianoTitle(BalthasarLib.D2DPainter.D2DPaintEventArgs e)
        {
            D2DGraphics g = e.D2DGraphics;
            Rectangle BlackRect = new Rectangle(
                0,
                0,
                e.ClipRectangle.Width,
                rconf.Const_TitleHeight
            );
            g.FillRectangle(BlackRect, Color.Black);
            Rectangle TitleSpliterRect = new Rectangle(
                0, 
                rconf.Const_TitleHeight-rconf.Const_TitleHeightSpliter-1, 
                e.ClipRectangle.Width, 
                rconf.Const_TitleHeightSpliter
            );
            g.DrawLine(new Point(0, rconf.Const_TitleLineTop), new Point(e.ClipRectangle.Width, rconf.Const_TitleLineTop), rconf.TitleColor_Line,2);
            g.DrawLine(new Point(0, rconf.Const_TitleRulerTop), new Point(e.ClipRectangle.Width, rconf.Const_TitleRulerTop), rconf.TitleColor_Ruler,2);

            g.DrawLine(new Point(rconf.Const_RollWidth, 0), new Point(rconf.Const_RollWidth, rconf.Const_TitleHeight), Color.White, 2); 
            g.FillRectangle(TitleSpliterRect, rconf.PianoColor_WhiteKey);
                
            Rectangle CurrentRect = new Rectangle(
                0,
                0,
                e.ClipRectangle.Width,
                rconf.Const_TitleHeight
            );//可绘制区域
        }
        private void DrawPianoRoll(BalthasarLib.D2DPainter.D2DPaintEventArgs e)
        {
            D2DGraphics g = e.D2DGraphics;

            int y = rconf.Const_TitleHeight;//绘制点纵坐标
            int cNote = cconf.CurrentTopNote;//绘制区域第一个阶的音符
            int MyNotePx = (int)((e.MousePoint.Y - rconf.Const_TitleHeight) / rconf.Const_RollNoteHeight);//获取当前琴键
            int MyNote = cconf.CurrentTopNote - MyNotePx;//获取坐标
            while (y < e.ClipRectangle.Height)//若未画超界
            {
                //计算绘制区域
                Point LT = new Point(0, y);//矩形左上角
                Point LB = new Point(0, y + rconf.Const_RollNoteHeight);//矩形左下角
                Point RT = new Point(rconf.Const_RollWidth, y);//矩形右上角
                Point RB = new Point(rconf.Const_RollWidth, y + rconf.Const_RollNoteHeight);//矩形右下角
                Rectangle WhiteRect = new Rectangle(LT, new Size(rconf.Const_RollWidth, rconf.Const_RollNoteHeight));//矩形区域
                Rectangle BlackRect = new Rectangle(LT, new Size(rconf.Const_RollBlackWidth, rconf.Const_RollNoteHeight));//矩形区域
                Rectangle WordRect = new Rectangle(new Point(LT.X + rconf.Const_RollBlackWidth, LT.Y), new Size(rconf.Const_RollWidth - rconf.Const_RollBlackWidth, rconf.Const_RollNoteHeight));//矩形区域
                //绘制基础矩形
                g.FillRectangle(WhiteRect, rconf.PianoColor_WhiteKey);
                g.DrawRectangle(WhiteRect, rconf.PianoColor_Line);
                //绘制黑键
                int Octave = rconf.getOctave(cNote);
                int Key = rconf.getKey(cNote);
                bool isBlackKey = rconf.KeyIsBlack[Key];
                if (isBlackKey)
                {
                    g.FillRectangle(BlackRect, rconf.PianoColor_BlackKey);
                }
                //获取鼠标位置琴键
                //绘制符号
                if (MyNote == cNote)
                {
                    g.FillRectangle(WordRect, rconf.PianoColor_MouseKey);
                    g.DrawText("  " + rconf.KeyChar[Key] + Octave.ToString(), WordRect, rconf.PianoColor_BlackKey, new System.Drawing.Font("微软雅黑", 10));
                }
                else if (Key == 0)
                {
                    g.DrawText("  " + rconf.KeyChar[Key] + Octave.ToString(), WordRect, rconf.PianoColor_BlackKey, new System.Drawing.Font("微软雅黑", 10));
                }

                /*
                //计算色域
                int Octave = rconf.getOctave(cNote);
                int Key = rconf.getKey(cNote);
                bool isBlackKey = rconf.KeyIsBlack[Key];
                Color KeyColor = isBlackKey ? rconf.RollColor_BlackKey_NormalSound : rconf.RollColor_WhiteKey_NormalSound;
                Color LineColor = rconf.RollColor_LineKey_NormalSound;
                Color OLineColor = rconf.RollColor_LineOctive_NormalSound;
                switch (rconf.getVoiceArea(cNote))
                {
                    case RollConfigures.VoiceKeyArea.OverSound: KeyColor = isBlackKey ? rconf.RollColor_BlackKey_OverSound : rconf.RollColor_WhiteKey_OverSound; LineColor = rconf.RollColor_LineKey_OverSound; OLineColor = rconf.RollColor_LineOctive_OverSound; break;
                    case RollConfigures.VoiceKeyArea.NoSound: KeyColor = isBlackKey ? rconf.RollColor_BlackKey_NoSound : rconf.RollColor_WhiteKey_NoSound; LineColor = rconf.RollColor_LineKey_NoSound; OLineColor = rconf.RollColor_LineOctive_OverSound; break;
                }
                //绘制矩形
                g.FillRectangle(Rect, KeyColor);
                //绘制边线
                g.DrawLine(LB, RB, (Key == 5 || Key == 0) ? OLineColor : LineColor);//isB ? LineL : LineB);
                g.DrawText(cNote.ToString() + rconf.KeyChar[Key] + Octave.ToString(), Rect, Color.White, new System.Drawing.Font("宋体", 12));
                */
                //递归
                y = y + rconf.Const_RollNoteHeight;
                cNote = cNote - 1;
            }
            //Rise 绘制Note等//传递事件
            //            e.ClipRectangle.Width - rconf.Const_RollWidth, rconf.Const_RollNoteHeight

            Rectangle CurrentRect = new Rectangle(
                0,
                rconf.Const_TitleHeight,
                rconf.Const_RollWidth,
                e.ClipRectangle.Height - rconf.Const_TitleHeight);//可绘制区域
        }

        public void RedrawPiano()
        {
            d2DPainterBox1.Refresh();
        }

        private void noteScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            cconf.CurrentTopNote = rconf.MaxNoteNumber - noteScrollBar1.Value;
            d2DPainterBox1.Refresh();
        }
        
        private void d2DPainterBox1_MouseMove(object sender, MouseEventArgs e)
        {
            d2DPainterBox1.Refresh();
        }

        private void PianoRollWindow_Resize(object sender, EventArgs e)
        {
            InitGUI();
        }
    }
}

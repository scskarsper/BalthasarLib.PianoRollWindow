namespace BalthasarLib.PianoRollWindow
{
    partial class PianoRollWindow
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.noteScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.d2DPainterBox1 = new BalthasarLib.D2DPainter.D2DPainterBox();
            this.SuspendLayout();
            // 
            // noteScrollBar1
            // 
            this.noteScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noteScrollBar1.LargeChange = 12;
            this.noteScrollBar1.Location = new System.Drawing.Point(233, 0);
            this.noteScrollBar1.Maximum = 127;
            this.noteScrollBar1.Name = "noteScrollBar1";
            this.noteScrollBar1.Size = new System.Drawing.Size(19, 226);
            this.noteScrollBar1.TabIndex = 3;
            this.noteScrollBar1.Value = 43;
            this.noteScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.noteScrollBar1_Scroll);
            // 
            // d2DPainterBox1
            // 
            this.d2DPainterBox1.BackColor = System.Drawing.Color.Black;
            this.d2DPainterBox1.Location = new System.Drawing.Point(0, 0);
            this.d2DPainterBox1.Name = "d2DPainterBox1";
            this.d2DPainterBox1.Size = new System.Drawing.Size(233, 226);
            this.d2DPainterBox1.TabIndex = 2;
            this.d2DPainterBox1.D2DPaint += new BalthasarLib.D2DPainter.D2DPainterBox.OnD2DPaintHandler(this.d2DPainterBox1_D2DPaint);
            this.d2DPainterBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.d2DPainterBox1_MouseMove);
            // 
            // PianoRollWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.Controls.Add(this.noteScrollBar1);
            this.Controls.Add(this.d2DPainterBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PianoRollWindow";
            this.Size = new System.Drawing.Size(252, 226);
            this.Resize += new System.EventHandler(this.PianoRollWindow_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar noteScrollBar1;
        private D2DPainter.D2DPainterBox d2DPainterBox1;
    }
}

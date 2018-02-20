namespace BalthasarLib.PianoRollWindow
{
    partial class ParamCurveWindow
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
            this.d2DPainterBox1 = new BalthasarLib.D2DPainter.D2DPainterBox();
            this.SuspendLayout();
            // 
            // d2DPainterBox1
            // 
            this.d2DPainterBox1.Antialias = false;
            this.d2DPainterBox1.BackColor = System.Drawing.Color.Black;
            this.d2DPainterBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.d2DPainterBox1.Location = new System.Drawing.Point(0, 0);
            this.d2DPainterBox1.Margin = new System.Windows.Forms.Padding(4);
            this.d2DPainterBox1.Name = "d2DPainterBox1";
            this.d2DPainterBox1.Size = new System.Drawing.Size(648, 276);
            this.d2DPainterBox1.TabIndex = 0;
            this.d2DPainterBox1.D2DPaint += new BalthasarLib.D2DPainter.D2DPainterBox.OnD2DPaintHandler(this.d2DPainterBox1_D2DPaint);
            this.d2DPainterBox1.Load += new System.EventHandler(this.d2DPainterBox1_Load);
            this.d2DPainterBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.d2DPainterBox1_MouseClick);
            this.d2DPainterBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.d2DPainterBox1_MouseDoubleClick);
            this.d2DPainterBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.d2DPainterBox1_MouseDown);
            this.d2DPainterBox1.MouseEnter += new System.EventHandler(this.d2DPainterBox1_MouseEnter);
            this.d2DPainterBox1.MouseLeave += new System.EventHandler(this.d2DPainterBox1_MouseLeave);
            this.d2DPainterBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.d2DPainterBox1_MouseMove);
            this.d2DPainterBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.d2DPainterBox1_MouseUp);
            this.d2DPainterBox1.Resize += new System.EventHandler(this.ParametersWindow_Resize);
            // 
            // ParametersWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.d2DPainterBox1);
            this.Name = "ParametersWindow";
            this.Size = new System.Drawing.Size(648, 276);
            this.ResumeLayout(false);

        }

        #endregion

        private D2DPainter.D2DPainterBox d2DPainterBox1;

    }
}

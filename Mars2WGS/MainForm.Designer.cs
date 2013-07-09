namespace Mars2WGS
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlAction = new System.Windows.Forms.Panel();
            this.cbbConvertAlgorithm = new System.Windows.Forms.ComboBox();
            this.cbbMapSource = new System.Windows.Forms.ComboBox();
            this.lblConvertAlgorithm = new System.Windows.Forms.Label();
            this.lblMapSource = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.grpSrc = new System.Windows.Forms.GroupBox();
            this.lblSrcLon = new System.Windows.Forms.Label();
            this.edSrcLon = new System.Windows.Forms.TextBox();
            this.lblSrcLat = new System.Windows.Forms.Label();
            this.edSrcLat = new System.Windows.Forms.TextBox();
            this.grpDst = new System.Windows.Forms.GroupBox();
            this.lblDstLon = new System.Windows.Forms.Label();
            this.edDstLon = new System.Windows.Forms.TextBox();
            this.lblDstLat = new System.Windows.Forms.Label();
            this.edDstLat = new System.Windows.Forms.TextBox();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.btnToMars = new System.Windows.Forms.Button();
            this.btnToWGS = new System.Windows.Forms.Button();
            this.grpDiff = new System.Windows.Forms.GroupBox();
            this.lblDiffLon = new System.Windows.Forms.Label();
            this.edDiffLon = new System.Windows.Forms.TextBox();
            this.lblDiffLat = new System.Windows.Forms.Label();
            this.edDiffLat = new System.Windows.Forms.TextBox();
            this.grpConvert = new System.Windows.Forms.GroupBox();
            this.pbConvert = new System.Windows.Forms.ProgressBar();
            this.rbToMars = new System.Windows.Forms.RadioButton();
            this.rbToWGS = new System.Windows.Forms.RadioButton();
            this.lblFileDst = new System.Windows.Forms.Label();
            this.edFileDst = new System.Windows.Forms.TextBox();
            this.lblFileSrc = new System.Windows.Forms.Label();
            this.edFileSrc = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.pnlAction.SuspendLayout();
            this.grpSrc.SuspendLayout();
            this.grpDst.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.grpDiff.SuspendLayout();
            this.grpConvert.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAction
            // 
            this.pnlAction.Controls.Add(this.cbbConvertAlgorithm);
            this.pnlAction.Controls.Add(this.cbbMapSource);
            this.pnlAction.Controls.Add(this.lblConvertAlgorithm);
            this.pnlAction.Controls.Add(this.lblMapSource);
            this.pnlAction.Controls.Add(this.btnExit);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAction.Location = new System.Drawing.Point(0, 395);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(624, 47);
            this.pnlAction.TabIndex = 0;
            // 
            // cbbConvertAlgorithm
            // 
            this.cbbConvertAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbConvertAlgorithm.FormattingEnabled = true;
            this.cbbConvertAlgorithm.Items.AddRange(new object[] {
            "Mars2Wgs.txt",
            "Formula"});
            this.cbbConvertAlgorithm.Location = new System.Drawing.Point(249, 13);
            this.cbbConvertAlgorithm.Name = "cbbConvertAlgorithm";
            this.cbbConvertAlgorithm.Size = new System.Drawing.Size(89, 20);
            this.cbbConvertAlgorithm.TabIndex = 15;
            this.cbbConvertAlgorithm.SelectedIndexChanged += new System.EventHandler(this.cbbConvertAlgorithm_SelectedIndexChanged);
            // 
            // cbbMapSource
            // 
            this.cbbMapSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMapSource.Enabled = false;
            this.cbbMapSource.FormattingEnabled = true;
            this.cbbMapSource.Items.AddRange(new object[] {
            "Google Map",
            "Baidu Map"});
            this.cbbMapSource.Location = new System.Drawing.Point(99, 13);
            this.cbbMapSource.Name = "cbbMapSource";
            this.cbbMapSource.Size = new System.Drawing.Size(89, 20);
            this.cbbMapSource.TabIndex = 14;
            this.cbbMapSource.SelectedIndexChanged += new System.EventHandler(this.cbbMapSource_SelectedIndexChanged);
            // 
            // lblConvertAlgorithm
            // 
            this.lblConvertAlgorithm.Location = new System.Drawing.Point(201, 10);
            this.lblConvertAlgorithm.Name = "lblConvertAlgorithm";
            this.lblConvertAlgorithm.Size = new System.Drawing.Size(42, 26);
            this.lblConvertAlgorithm.TabIndex = 13;
            this.lblConvertAlgorithm.Text = "Mode:";
            this.lblConvertAlgorithm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMapSource
            // 
            this.lblMapSource.Location = new System.Drawing.Point(18, 10);
            this.lblMapSource.Name = "lblMapSource";
            this.lblMapSource.Size = new System.Drawing.Size(75, 26);
            this.lblMapSource.TabIndex = 12;
            this.lblMapSource.Text = "Map Source:";
            this.lblMapSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(508, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 32);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Close";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grpSrc
            // 
            this.grpSrc.AutoSize = true;
            this.grpSrc.Controls.Add(this.lblSrcLon);
            this.grpSrc.Controls.Add(this.edSrcLon);
            this.grpSrc.Controls.Add(this.lblSrcLat);
            this.grpSrc.Controls.Add(this.edSrcLat);
            this.grpSrc.Location = new System.Drawing.Point(12, 20);
            this.grpSrc.Name = "grpSrc";
            this.grpSrc.Size = new System.Drawing.Size(113, 156);
            this.grpSrc.TabIndex = 1;
            this.grpSrc.TabStop = false;
            this.grpSrc.Text = "Source";
            // 
            // lblSrcLon
            // 
            this.lblSrcLon.Location = new System.Drawing.Point(6, 26);
            this.lblSrcLon.Name = "lblSrcLon";
            this.lblSrcLon.Size = new System.Drawing.Size(100, 23);
            this.lblSrcLon.TabIndex = 3;
            this.lblSrcLon.Text = "Lon(X):";
            this.lblSrcLon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edSrcLon
            // 
            this.edSrcLon.Location = new System.Drawing.Point(6, 52);
            this.edSrcLon.Name = "edSrcLon";
            this.edSrcLon.Size = new System.Drawing.Size(100, 21);
            this.edSrcLon.TabIndex = 0;
            this.edSrcLon.Text = "109.6910297";
            // 
            // lblSrcLat
            // 
            this.lblSrcLat.Location = new System.Drawing.Point(6, 89);
            this.lblSrcLat.Name = "lblSrcLat";
            this.lblSrcLat.Size = new System.Drawing.Size(100, 23);
            this.lblSrcLat.TabIndex = 1;
            this.lblSrcLat.Text = "Lat(Y):";
            this.lblSrcLat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edSrcLat
            // 
            this.edSrcLat.Location = new System.Drawing.Point(6, 115);
            this.edSrcLat.Name = "edSrcLat";
            this.edSrcLat.Size = new System.Drawing.Size(100, 21);
            this.edSrcLat.TabIndex = 1;
            this.edSrcLat.Text = "18.63227691";
            // 
            // grpDst
            // 
            this.grpDst.AutoSize = true;
            this.grpDst.Controls.Add(this.lblDstLon);
            this.grpDst.Controls.Add(this.edDstLon);
            this.grpDst.Controls.Add(this.lblDstLat);
            this.grpDst.Controls.Add(this.edDstLat);
            this.grpDst.Location = new System.Drawing.Point(224, 20);
            this.grpDst.Name = "grpDst";
            this.grpDst.Size = new System.Drawing.Size(114, 156);
            this.grpDst.TabIndex = 2;
            this.grpDst.TabStop = false;
            this.grpDst.Text = "Target";
            // 
            // lblDstLon
            // 
            this.lblDstLon.Location = new System.Drawing.Point(6, 26);
            this.lblDstLon.Name = "lblDstLon";
            this.lblDstLon.Size = new System.Drawing.Size(100, 23);
            this.lblDstLon.TabIndex = 3;
            this.lblDstLon.Text = "Lon(X):";
            this.lblDstLon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edDstLon
            // 
            this.edDstLon.Location = new System.Drawing.Point(6, 52);
            this.edDstLon.Name = "edDstLon";
            this.edDstLon.Size = new System.Drawing.Size(100, 21);
            this.edDstLon.TabIndex = 0;
            this.edDstLon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDstLat
            // 
            this.lblDstLat.Location = new System.Drawing.Point(6, 89);
            this.lblDstLat.Name = "lblDstLat";
            this.lblDstLat.Size = new System.Drawing.Size(100, 23);
            this.lblDstLat.TabIndex = 1;
            this.lblDstLat.Text = "Lat(Y):";
            this.lblDstLat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edDstLat
            // 
            this.edDstLat.Location = new System.Drawing.Point(6, 115);
            this.edDstLat.Name = "edDstLat";
            this.edDstLat.Size = new System.Drawing.Size(100, 21);
            this.edDstLat.TabIndex = 1;
            this.edDstLat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpActions
            // 
            this.grpActions.AutoSize = true;
            this.grpActions.Controls.Add(this.btnToMars);
            this.grpActions.Controls.Add(this.btnToWGS);
            this.grpActions.Location = new System.Drawing.Point(131, 20);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(87, 157);
            this.grpActions.TabIndex = 3;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Action";
            // 
            // btnToMars
            // 
            this.btnToMars.Location = new System.Drawing.Point(6, 114);
            this.btnToMars.Name = "btnToMars";
            this.btnToMars.Size = new System.Drawing.Size(75, 23);
            this.btnToMars.TabIndex = 1;
            this.btnToMars.Text = "To Mars";
            this.btnToMars.UseVisualStyleBackColor = true;
            this.btnToMars.Click += new System.EventHandler(this.btnToMars_Click);
            // 
            // btnToWGS
            // 
            this.btnToWGS.Location = new System.Drawing.Point(6, 51);
            this.btnToWGS.Name = "btnToWGS";
            this.btnToWGS.Size = new System.Drawing.Size(75, 23);
            this.btnToWGS.TabIndex = 0;
            this.btnToWGS.Text = "To WGS";
            this.btnToWGS.UseVisualStyleBackColor = true;
            this.btnToWGS.Click += new System.EventHandler(this.btnToWGS_Click);
            // 
            // grpDiff
            // 
            this.grpDiff.AutoSize = true;
            this.grpDiff.Controls.Add(this.lblDiffLon);
            this.grpDiff.Controls.Add(this.edDiffLon);
            this.grpDiff.Controls.Add(this.lblDiffLat);
            this.grpDiff.Controls.Add(this.edDiffLat);
            this.grpDiff.Location = new System.Drawing.Point(500, 20);
            this.grpDiff.Name = "grpDiff";
            this.grpDiff.Size = new System.Drawing.Size(112, 156);
            this.grpDiff.TabIndex = 4;
            this.grpDiff.TabStop = false;
            this.grpDiff.Text = "Difference";
            // 
            // lblDiffLon
            // 
            this.lblDiffLon.Location = new System.Drawing.Point(6, 26);
            this.lblDiffLon.Name = "lblDiffLon";
            this.lblDiffLon.Size = new System.Drawing.Size(100, 23);
            this.lblDiffLon.TabIndex = 3;
            this.lblDiffLon.Text = "Lon(X):";
            this.lblDiffLon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edDiffLon
            // 
            this.edDiffLon.Location = new System.Drawing.Point(6, 52);
            this.edDiffLon.Name = "edDiffLon";
            this.edDiffLon.Size = new System.Drawing.Size(100, 21);
            this.edDiffLon.TabIndex = 0;
            this.edDiffLon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDiffLat
            // 
            this.lblDiffLat.Location = new System.Drawing.Point(6, 89);
            this.lblDiffLat.Name = "lblDiffLat";
            this.lblDiffLat.Size = new System.Drawing.Size(100, 23);
            this.lblDiffLat.TabIndex = 1;
            this.lblDiffLat.Text = "Lat(Y):";
            this.lblDiffLat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edDiffLat
            // 
            this.edDiffLat.Location = new System.Drawing.Point(6, 115);
            this.edDiffLat.Name = "edDiffLat";
            this.edDiffLat.Size = new System.Drawing.Size(100, 21);
            this.edDiffLat.TabIndex = 1;
            this.edDiffLat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpConvert
            // 
            this.grpConvert.Controls.Add(this.pbConvert);
            this.grpConvert.Controls.Add(this.rbToMars);
            this.grpConvert.Controls.Add(this.rbToWGS);
            this.grpConvert.Controls.Add(this.lblFileDst);
            this.grpConvert.Controls.Add(this.edFileDst);
            this.grpConvert.Controls.Add(this.lblFileSrc);
            this.grpConvert.Controls.Add(this.edFileSrc);
            this.grpConvert.Controls.Add(this.btnConvert);
            this.grpConvert.Location = new System.Drawing.Point(14, 198);
            this.grpConvert.Name = "grpConvert";
            this.grpConvert.Size = new System.Drawing.Size(597, 191);
            this.grpConvert.TabIndex = 5;
            this.grpConvert.TabStop = false;
            this.grpConvert.Text = "GPX/KML/OruxMaps Convert";
            // 
            // pbConvert
            // 
            this.pbConvert.Location = new System.Drawing.Point(494, 30);
            this.pbConvert.Name = "pbConvert";
            this.pbConvert.Size = new System.Drawing.Size(94, 12);
            this.pbConvert.TabIndex = 7;
            // 
            // rbToMars
            // 
            this.rbToMars.Location = new System.Drawing.Point(101, 106);
            this.rbToMars.Name = "rbToMars";
            this.rbToMars.Size = new System.Drawing.Size(104, 24);
            this.rbToMars.TabIndex = 6;
            this.rbToMars.Text = "To Mars";
            this.rbToMars.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbToMars.UseVisualStyleBackColor = true;
            this.rbToMars.CheckedChanged += new System.EventHandler(this.rbToMars_CheckedChanged);
            // 
            // rbToWGS
            // 
            this.rbToWGS.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbToWGS.Checked = true;
            this.rbToWGS.Location = new System.Drawing.Point(101, 76);
            this.rbToWGS.Name = "rbToWGS";
            this.rbToWGS.Size = new System.Drawing.Size(104, 24);
            this.rbToWGS.TabIndex = 5;
            this.rbToWGS.TabStop = true;
            this.rbToWGS.Text = "To WGS";
            this.rbToWGS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbToWGS.UseVisualStyleBackColor = true;
            this.rbToWGS.CheckedChanged += new System.EventHandler(this.rbToWGS_CheckedChanged);
            // 
            // lblFileDst
            // 
            this.lblFileDst.Location = new System.Drawing.Point(13, 127);
            this.lblFileDst.Name = "lblFileDst";
            this.lblFileDst.Size = new System.Drawing.Size(100, 23);
            this.lblFileDst.TabIndex = 4;
            this.lblFileDst.Text = "Target:";
            this.lblFileDst.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edFileDst
            // 
            this.edFileDst.AllowDrop = true;
            this.edFileDst.Location = new System.Drawing.Point(15, 153);
            this.edFileDst.Name = "edFileDst";
            this.edFileDst.Size = new System.Drawing.Size(473, 21);
            this.edFileDst.TabIndex = 3;
            this.edFileDst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.edFileDst_MouseDoubleClick);
            // 
            // lblFileSrc
            // 
            this.lblFileSrc.Location = new System.Drawing.Point(13, 22);
            this.lblFileSrc.Name = "lblFileSrc";
            this.lblFileSrc.Size = new System.Drawing.Size(100, 23);
            this.lblFileSrc.TabIndex = 2;
            this.lblFileSrc.Text = "Source:";
            this.lblFileSrc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edFileSrc
            // 
            this.edFileSrc.AllowDrop = true;
            this.edFileSrc.Location = new System.Drawing.Point(15, 48);
            this.edFileSrc.Name = "edFileSrc";
            this.edFileSrc.Size = new System.Drawing.Size(473, 21);
            this.edFileSrc.TabIndex = 1;
            this.edFileSrc.TextChanged += new System.EventHandler(this.edFileSrc_TextChanged);
            this.edFileSrc.DragDrop += new System.Windows.Forms.DragEventHandler(this.edFileSrc_DragDrop);
            this.edFileSrc.DragEnter += new System.Windows.Forms.DragEventHandler(this.edFileSrc_DragEnter);
            this.edFileSrc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.edFileSrc_MouseDoubleClick);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(494, 48);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(94, 126);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            this.dlgOpen.SupportMultiDottedExtensions = true;
            // 
            // dlgSave
            // 
            this.dlgSave.SupportMultiDottedExtensions = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.grpConvert);
            this.Controls.Add(this.grpDiff);
            this.Controls.Add(this.grpActions);
            this.Controls.Add(this.grpDst);
            this.Controls.Add(this.grpSrc);
            this.Controls.Add(this.pnlAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mars <-> WGS84";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlAction.ResumeLayout(false);
            this.grpSrc.ResumeLayout(false);
            this.grpSrc.PerformLayout();
            this.grpDst.ResumeLayout(false);
            this.grpDst.PerformLayout();
            this.grpActions.ResumeLayout(false);
            this.grpDiff.ResumeLayout(false);
            this.grpDiff.PerformLayout();
            this.grpConvert.ResumeLayout(false);
            this.grpConvert.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlAction;
        private System.Windows.Forms.GroupBox grpSrc;
        private System.Windows.Forms.Label lblSrcLat;
        private System.Windows.Forms.TextBox edSrcLat;
        private System.Windows.Forms.Label lblSrcLon;
        private System.Windows.Forms.TextBox edSrcLon;
        private System.Windows.Forms.GroupBox grpDst;
        private System.Windows.Forms.Label lblDstLon;
        private System.Windows.Forms.TextBox edDstLon;
        private System.Windows.Forms.Label lblDstLat;
        private System.Windows.Forms.TextBox edDstLat;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.Button btnToMars;
        private System.Windows.Forms.Button btnToWGS;
        private System.Windows.Forms.GroupBox grpDiff;
        private System.Windows.Forms.Label lblDiffLon;
        private System.Windows.Forms.TextBox edDiffLon;
        private System.Windows.Forms.Label lblDiffLat;
        private System.Windows.Forms.TextBox edDiffLat;
        private System.Windows.Forms.GroupBox grpConvert;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox edFileSrc;
        private System.Windows.Forms.Label lblFileSrc;
        private System.Windows.Forms.Label lblFileDst;
        private System.Windows.Forms.TextBox edFileDst;
        private System.Windows.Forms.RadioButton rbToMars;
        private System.Windows.Forms.RadioButton rbToWGS;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ProgressBar pbConvert;
        private System.Windows.Forms.ComboBox cbbConvertAlgorithm;
        private System.Windows.Forms.ComboBox cbbMapSource;
        private System.Windows.Forms.Label lblConvertAlgorithm;
        private System.Windows.Forms.Label lblMapSource;
    }
}


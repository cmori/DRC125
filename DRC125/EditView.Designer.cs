namespace DRC125
{
    partial class EditView
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.出力SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.白黒反転するToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.白黒反転戻すToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自動角度補正ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.すべて破棄ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.カウンタリセットToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.再採番ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.txtFileKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHinshitsu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTsusanCnt = new System.Windows.Forms.Label();
            this.lblReadCnt = new System.Windows.Forms.Label();
            this.lblOutputCnt = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHyoshiNum = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.mainPanel = new DRC125.FramePalentPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1354, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.出力SToolStripMenuItem,
            this.toolStripMenuItem1,
            this.白黒反転するToolStripMenuItem,
            this.白黒反転戻すToolStripMenuItem,
            this.自動角度補正ToolStripMenuItem,
            this.toolStripMenuItem6,
            this.すべて破棄ToolStripMenuItem,
            this.カウンタリセットToolStripMenuItem,
            this.toolStripMenuItem2,
            this.再採番ToolStripMenuItem,
            this.toolStripMenuItem5,
            this.終了XToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(205, 6);
            // 
            // 出力SToolStripMenuItem
            // 
            this.出力SToolStripMenuItem.Name = "出力SToolStripMenuItem";
            this.出力SToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.出力SToolStripMenuItem.Text = "フォルダに出力(&S)";
            this.出力SToolStripMenuItem.Click += new System.EventHandler(this.出力SToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 6);
            // 
            // 白黒反転するToolStripMenuItem
            // 
            this.白黒反転するToolStripMenuItem.Name = "白黒反転するToolStripMenuItem";
            this.白黒反転するToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.白黒反転するToolStripMenuItem.Text = "白黒反転する";
            this.白黒反転するToolStripMenuItem.Click += new System.EventHandler(this.白黒反転するToolStripMenuItem_Click);
            // 
            // 白黒反転戻すToolStripMenuItem
            // 
            this.白黒反転戻すToolStripMenuItem.Name = "白黒反転戻すToolStripMenuItem";
            this.白黒反転戻すToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.白黒反転戻すToolStripMenuItem.Text = "白黒反転戻す";
            this.白黒反転戻すToolStripMenuItem.Click += new System.EventHandler(this.白黒反転戻すToolStripMenuItem_Click);
            // 
            // 自動角度補正ToolStripMenuItem
            // 
            this.自動角度補正ToolStripMenuItem.Name = "自動角度補正ToolStripMenuItem";
            this.自動角度補正ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.自動角度補正ToolStripMenuItem.Text = "自動角度補正";
            this.自動角度補正ToolStripMenuItem.Click += new System.EventHandler(this.自動角度補正ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(205, 6);
            // 
            // すべて破棄ToolStripMenuItem
            // 
            this.すべて破棄ToolStripMenuItem.Name = "すべて破棄ToolStripMenuItem";
            this.すべて破棄ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.すべて破棄ToolStripMenuItem.Text = "すべて破棄";
            this.すべて破棄ToolStripMenuItem.Click += new System.EventHandler(this.すべて破棄ToolStripMenuItem_Click);
            // 
            // カウンタリセットToolStripMenuItem
            // 
            this.カウンタリセットToolStripMenuItem.Name = "カウンタリセットToolStripMenuItem";
            this.カウンタリセットToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.カウンタリセットToolStripMenuItem.Text = "通算カウンタをリセット";
            this.カウンタリセットToolStripMenuItem.Click += new System.EventHandler(this.カウンタリセットToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(205, 6);
            // 
            // 再採番ToolStripMenuItem
            // 
            this.再採番ToolStripMenuItem.Name = "再採番ToolStripMenuItem";
            this.再採番ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.再採番ToolStripMenuItem.Text = "再採番";
            this.再採番ToolStripMenuItem.Click += new System.EventHandler(this.再採番ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(205, 6);
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            this.終了XToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.終了XToolStripMenuItem.Text = "終了(&X)";
            this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 667);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1354, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderPath.Location = new System.Drawing.Point(805, 3);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(378, 19);
            this.txtFolderPath.TabIndex = 3;
            this.txtFolderPath.Text = "C:\\Users\\chihiro\\Documents\\スキャン";
            // 
            // txtFileKey
            // 
            this.txtFileKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileKey.Location = new System.Drawing.Point(1233, 3);
            this.txtFileKey.Name = "txtFileKey";
            this.txtFileKey.Size = new System.Drawing.Size(116, 19);
            this.txtFileKey.TabIndex = 4;
            this.txtFileKey.Text = "Manga";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1188, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "接頭語";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(761, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "フォルダ";
            // 
            // txtHinshitsu
            // 
            this.txtHinshitsu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHinshitsu.Location = new System.Drawing.Point(727, 4);
            this.txtHinshitsu.Name = "txtHinshitsu";
            this.txtHinshitsu.Size = new System.Drawing.Size(30, 19);
            this.txtHinshitsu.TabIndex = 7;
            this.txtHinshitsu.Text = "75";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(694, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "品質";
            // 
            // lblTsusanCnt
            // 
            this.lblTsusanCnt.AutoSize = true;
            this.lblTsusanCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTsusanCnt.Location = new System.Drawing.Point(184, 6);
            this.lblTsusanCnt.Name = "lblTsusanCnt";
            this.lblTsusanCnt.Size = new System.Drawing.Size(2, 14);
            this.lblTsusanCnt.TabIndex = 9;
            // 
            // lblReadCnt
            // 
            this.lblReadCnt.AutoSize = true;
            this.lblReadCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReadCnt.Location = new System.Drawing.Point(391, 6);
            this.lblReadCnt.Name = "lblReadCnt";
            this.lblReadCnt.Size = new System.Drawing.Size(2, 14);
            this.lblReadCnt.TabIndex = 10;
            // 
            // lblOutputCnt
            // 
            this.lblOutputCnt.AutoSize = true;
            this.lblOutputCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutputCnt.Location = new System.Drawing.Point(286, 6);
            this.lblOutputCnt.Name = "lblOutputCnt";
            this.lblOutputCnt.Size = new System.Drawing.Size(2, 14);
            this.lblOutputCnt.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(332, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "読込枚数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(100, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "通算出力枚数";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(227, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "出力枚数";
            // 
            // txtHyoshiNum
            // 
            this.txtHyoshiNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHyoshiNum.Location = new System.Drawing.Point(592, 4);
            this.txtHyoshiNum.Name = "txtHyoshiNum";
            this.txtHyoshiNum.Size = new System.Drawing.Size(26, 19);
            this.txtHyoshiNum.TabIndex = 16;
            this.txtHyoshiNum.Text = "2";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(535, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "表紙枚数";
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 26);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1354, 641);
            this.mainPanel.TabIndex = 2;
            // 
            // EditView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 689);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtHyoshiNum);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblOutputCnt);
            this.Controls.Add(this.lblReadCnt);
            this.Controls.Add(this.lblTsusanCnt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHinshitsu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFileKey);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EditView";
            this.Text = "DRC125";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditView_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 出力SToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private FramePalentPanel mainPanel;
        private System.Windows.Forms.ToolStripMenuItem すべて破棄ToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.TextBox txtFileKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHinshitsu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTsusanCnt;
        private System.Windows.Forms.ToolStripMenuItem カウンタリセットToolStripMenuItem;
        private System.Windows.Forms.Label lblReadCnt;
        private System.Windows.Forms.Label lblOutputCnt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.TextBox txtHyoshiNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripMenuItem 再採番ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem 白黒反転するToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 白黒反転戻すToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自動角度補正ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
    }
}


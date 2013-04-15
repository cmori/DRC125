using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace DRC125
{
    public partial class EditView : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int MessageBeep(uint n);

        public struct EditState
        {
            public int TsusanNum;
            public int HyoshiNum;
            public int Hinshitsu;
            public string FolderPath;
            public string FileKey;
        }

        protected bool IsOutput { get; set; }
        
        public EditView()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            IsOutput = false;

            bool isFocus = false;

            txtFolderPath.GotFocus += (object sender, EventArgs e) =>
            {
                isFocus = true;
            };
            txtFolderPath.LostFocus += (object sender, EventArgs e) =>
            {
                isFocus = false;
            };
            txtFileKey.GotFocus += (object sender, EventArgs e) =>
            {
                isFocus = true;
            };
            txtFileKey.LostFocus += (object sender, EventArgs e) =>
            {
                isFocus = false;
            };
            txtHinshitsu.GotFocus += (object sender, EventArgs e) =>
            {
                isFocus = true;
            };
            txtHinshitsu.LostFocus += (object sender, EventArgs e) =>
            {
                isFocus = false;
            };
            txtHyoshiNum.GotFocus += (object sender, EventArgs e) =>
            {
                isFocus = true;
            };
            txtHyoshiNum.LostFocus += (object sender, EventArgs e) =>
            {
                isFocus = false;
            };
            mainPanel.MouseEnter += (object sender, EventArgs e) =>
            {
                if (Form.ActiveForm == this && isFocus == false)
                {
                    mainPanel.Focus();
                }
            };

        }

        public EditView(List<string> pathList)
            : this()
        {
            EditState state = LoadEditState();

            txtFileKey.Text = state.FileKey;
            txtFolderPath.Text = state.FolderPath;
            txtHinshitsu.Text = state.Hinshitsu.ToString();
            txtHyoshiNum.Text = state.HyoshiNum.ToString();
            lblTsusanCnt.Text = state.TsusanNum.ToString();

            mainPanel.DestroyFrames();

            mainPanel.ShowFrames(pathList);

            int hyoshiCnt = int.Parse(txtHyoshiNum.Text == "" ? "0" : txtHyoshiNum.Text);
            int tsusanCnt = int.Parse(lblTsusanCnt.Text.Replace("枚", "") == "" ? "0" : lblTsusanCnt.Text.Replace("枚", ""));
            mainPanel.ResetNumber(tsusanCnt - hyoshiCnt);

            mainPanel.AutoRotate();

            lblReadCnt.Text = pathList.Count.ToString() + "枚";

            state.FileKey = txtFileKey.Text;
            state.FolderPath = txtFolderPath.Text;
            state.Hinshitsu = (txtHinshitsu.Text == "" ? 0 : int.Parse(txtHinshitsu.Text));
            state.HyoshiNum = (txtHyoshiNum.Text == "" ? 0 : int.Parse(txtHyoshiNum.Text));
            state.TsusanNum = (lblTsusanCnt.Text == "" ? 0 : int.Parse(lblTsusanCnt.Text.Replace("枚", "")));

        }

        public EditState LoadEditState()
        {
            EditState state = new EditState();

            string dicPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            using (System.IO.StreamReader r = new System.IO.StreamReader(dicPath + "\\state"))
            {
                int index = 0;
                while (r.Peek() >= 0)
                {
                    string[] v = r.ReadLine().Split('\t');

                    switch (index)
                    {
                        case 0:
                            state.FileKey = v[1];
                            break;
                        case 1:
                            state.FolderPath = v[1];
                            break;
                        case 2:
                            state.Hinshitsu = int.Parse(v[1]);
                            break;
                        case 3:
                            state.HyoshiNum = int.Parse(v[1]);
                            break;
                        case 4:
                            state.TsusanNum = int.Parse(v[1]);
                            break;
                    }
                    index++;
                }
            }

            return state;
        }

        public void SaveEditSaate()
        {
            EditState state = new EditState();
            state.FileKey = txtFileKey.Text;
            state.FolderPath = txtFolderPath.Text;
            state.Hinshitsu = (txtHinshitsu.Text == "" ? 0 : int.Parse(txtHinshitsu.Text));
            state.HyoshiNum = (txtHyoshiNum.Text == "" ? 0 : int.Parse(txtHyoshiNum.Text));
            state.TsusanNum = (lblTsusanCnt.Text == "" ? 0 : int.Parse(lblTsusanCnt.Text.Replace("枚", "")));

            string dicPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            System.IO.File.Delete(dicPath + "\\state");
            System.IO.File.AppendAllText(dicPath + "\\state", "FileKey\t" + state.FileKey.ToString() + "\n");
            System.IO.File.AppendAllText(dicPath + "\\state", "FolderPath\t" + state.FolderPath.ToString() + "\n");
            System.IO.File.AppendAllText(dicPath + "\\state", "Hinshitsu\t" + state.Hinshitsu.ToString() + "\n");
            System.IO.File.AppendAllText(dicPath + "\\state", "HyoshiNum\t" + state.HyoshiNum.ToString() + "\n");
            System.IO.File.AppendAllText(dicPath + "\\state", "TsusanNum\t" + state.TsusanNum.ToString());
        }
        
        protected override void OnClosed(EventArgs e)
        {
            mainPanel.DestroyFrames();

            lblReadCnt.Text = "";
            lblOutputCnt.Text = "";

            SaveEditSaate();

            base.OnClosed(e);
        }

        #region めにゅーClick

        private void カウンタリセットToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblTsusanCnt.Text = "0";
        }

        private void すべて破棄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPanel.DestroyFrames();

            lblReadCnt.Text = "";
            lblOutputCnt.Text = "";
        }

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 出力SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            List<string> pathList =  mainPanel.OutputJpegs(txtFolderPath.Text, txtFileKey.Text, Int64.Parse(txtHinshitsu.Text));
            
            int cnt = pathList.Count;

            lblTsusanCnt.Text = (int.Parse(lblTsusanCnt.Text == "" ? "0" : lblTsusanCnt.Text.Replace("枚", "")) + cnt).ToString() + "枚";
            lblOutputCnt.Text = cnt.ToString() + "枚";

            this.Enabled = true;

            MessageBeep(0);
            MessageBox.Show(this, cnt.ToString() + "枚出力しました");

            IsOutput = true;
        }

        private void 再採番ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int hyoshiCnt = int.Parse(txtHyoshiNum.Text == "" ? "0" : txtHyoshiNum.Text);
            int tsusanCnt = int.Parse(lblTsusanCnt.Text.Replace("枚", "") == "" ? "0" : lblTsusanCnt.Text.Replace("枚", ""));
            mainPanel.ResetNumber(tsusanCnt - hyoshiCnt);
        }

        private void 白黒反転するToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPanel.ViewShirokuro(true);
        }

        private void 白黒反転戻すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPanel.ViewShirokuro(false);
        }

        private void 自動角度補正ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainPanel.AutoRotate();
            MessageBeep(0);
            MessageBox.Show(this, "終了!");
        }
        #endregion
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void EditView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainPanel.IsSaveEnd == false)
            {
                e.Cancel = true;
                MessageBox.Show("まだ");
            }

            if (IsOutput == false)
            {
                e.Cancel = MessageBox.Show("出力してない", "", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel;
            }
        }
        
    }
}

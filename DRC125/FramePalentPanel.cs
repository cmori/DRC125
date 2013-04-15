using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;

namespace DRC125
{
    class FramePalentPanel : Panel
    {
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private Dictionary<ImportFrame, Point> DefaultLocation = new Dictionary<ImportFrame, Point>();

        public FramePalentPanel()
        {
            vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(1337, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(15, this.Height);
            this.vScrollBar1.TabIndex = 0;
            vScrollBar1.Visible = true;
            vScrollBar1.Value = 0;
            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = this.Height;
            vScrollBar1.Scroll += (object sender, ScrollEventArgs e) =>
            {
                foreach (Control c in this.Controls)
                {
                    if (c.GetType() != typeof(ImportFrame)) continue;

                    ImportFrame title = (ImportFrame)c;
                    if (DefaultLocation.ContainsKey(title) == false) DefaultLocation.Add(title, title.Location);

                    title.Location = new Point(title.Location.X, DefaultLocation[title].Y - e.NewValue); // x,y
                }
            };
            vScrollBar1.ValueChanged += (object sender, EventArgs e) =>
            {
                foreach (Control c in this.Controls)
                {
                    if (c.GetType() != typeof(ImportFrame)) continue;

                    ImportFrame title = (ImportFrame)c;
                    if (DefaultLocation.ContainsKey(title) == false) DefaultLocation.Add(title, title.Location);

                    title.Location = new Point(title.Location.X, DefaultLocation[title].Y - vScrollBar1.Value); // x,y
                }
            };
            Controls.Add(this.vScrollBar1);

        }

        //protected override void OnMouseEnter(EventArgs e)
        //{
        //    base.OnMouseEnter(e);

        //    Focus();
        //}
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Focus();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            var offset = Math.Min(Math.Max(this.vScrollBar1.Value + ((e.Delta * -1) / 120 * 100), this.vScrollBar1.Minimum), this.vScrollBar1.Maximum);

            vScrollBar1.Value = offset;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            if (_doTiles == false) Tiles();
        }

        protected override void Dispose(bool disposing)
        {
            DestroyFrames();
            vScrollBar1.Dispose();
            base.Dispose(disposing);
        }

        public void DestroyFrames()
        {
            DefaultLocation.Clear();
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {

                Control c = this.Controls[i];
                if (c.GetType() != typeof(ImportFrame)) continue;

                this.Controls.Remove(c);
                c.Dispose();
            }
            //this.Controls.Clear();
        }

        private bool _doTiles = false;

        public void Tiles()
        {
            _doTiles = true;

            int h = ImportFrame.HEIGHT;

            int index = 0;
            int dansu = 0;
            foreach (Control c in this.Controls)
            {
                if (c.GetType() != typeof(ImportFrame)) continue;

                ImportFrame title = (ImportFrame)c;

                if (index * (title.Width + 10) + title.Width + 10 > this.Width)
                {
                    dansu++;
                    index = 0;
                    h = (title.Height + 30) * (dansu + 1);
                }

                title.Location = new Point(index * (title.Width + 10), ((title.Height + 30) * dansu)); // x,y
                index++;
            }

            vScrollBar1.Location = new Point(this.Width - 15, 0);
            vScrollBar1.Visible = true;
            vScrollBar1.Value = 0;
            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = (ImportFrame.HEIGHT + 30) * dansu + 50;

            _doTiles = false;
        }

        public bool IsSaveEnd
        {
            get
            {
                bool isEnd = true;
                foreach (Control ctl in this.Controls)
                {

                    if (ctl.GetType() != typeof(ImportFrame)) continue;

                    ImportFrame frame = (ImportFrame)ctl;
                    isEnd = isEnd && frame.IsSaveEnd;
                }
                return isEnd;
            }
        }

        public List<string> OutputJpegs(string directoryPath, string headName, Int64 quality)
        {
            if (System.IO.Directory.Exists(directoryPath) == false) System.IO.Directory.CreateDirectory(directoryPath);

            List<string> pathList = new List<string>();

            int cnt = 0;
            int index = 1;
            foreach (Control ctl in this.Controls)
            {

                if (ctl.GetType() != typeof(ImportFrame)) continue;

                ImportFrame frame = (ImportFrame)ctl;

                while (true)
                {

                    string saveName = directoryPath + "\\" + headName + "_" + String.Format("{0:000}", index) + ".jpg";

                    index++;

                    if (System.IO.File.Exists(saveName)) continue;

                    frame.SaveBackground(saveName, quality);

                    pathList.Add(saveName);

                    cnt++;

                    break;
                }

                // 速さ重視 終わってんのかわからんくなるけど。。。
                //// 終了待ち
                //List<string> exist = new List<string>();
                //while (exist.Count != pathList.Count)
                //{
                //    for (int i = 0; i < pathList.Count; i++)
                //    {
                //        if (exist.Contains(pathList[i]) == false)
                //        {
                //            if (System.IO.File.Exists(pathList[i]))
                //            {
                //                exist.Add(pathList[i]);
                //            }
                //        }
                //    }

                //    Application.DoEvents(); // かっこ悪い。。。

                //}
            }

            return pathList;
        }

        public void ResetNumber(int index)
        {
            foreach (Control ctl in this.Controls)
            {

                if (ctl.GetType() != typeof(ImportFrame)) continue;

                ImportFrame frame = (ImportFrame)ctl;
                frame.No = index;
                index++;
                if (index == 0) index++;
            }
        }

        public void ViewShirokuro(bool visible)
        {
            foreach (Control ctl in this.Controls)
            {

                if (ctl.GetType() != typeof(ImportFrame)) continue;
                ImportFrame frame = (ImportFrame)ctl;
                frame.ViewShirokuro(visible);
            }
        }

        public void AutoRotate()
        {
            foreach (Control ctl in this.Controls)
            {

                if (ctl.GetType() != typeof(ImportFrame)) continue;
                ImportFrame frame = (ImportFrame)ctl;
                frame.AutoRotate();
            }
        }

        public void ShowFrames(List<string> pathList)
        {
            foreach (string p in pathList)
            {
                ImportFrame addItem = new ImportFrame(p);

                addItem.OnSelectedChange += (ImportFrame frame) =>
                {
                    if (frame.IsSelect == false) return;
                    foreach (Control ctl in this.Controls)
                    {

                        if (ctl.GetType() != typeof(ImportFrame)) continue;
                        if (frame == ctl) continue;
                        ((ImportFrame)ctl).IsSelect = false;
                    }
                };

                addItem.OnRemove += (ImportFrame frame) =>
                {
                    int index = frame.No;

                    Controls.Remove(frame);
                    frame.Dispose();

                    ResetNumber(Math.Min(Controls.Count > 1 ? ((ImportFrame)Controls[1]).No : 1, index));
                    Tiles();
                };

                //addItem.ViewGaidoLine(true);

                this.Controls.Add(addItem);
            }

            ResetNumber(1);
            Tiles();
        }
    }
}

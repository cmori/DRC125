using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DRC125
{
    delegate void SelectedChangeHandler(ImportFrame frame);
    delegate void RemoveHandler(ImportFrame frame);

    class ImportFrame : System.Windows.Forms.Panel
    {
        #region DLLImport
        /// <summary>
        /// グローバルメモリオブジェクトをロックし、メモリブロックの最初の 1 バイトへのポインタを返します。
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);

        /// <summary>
        /// 指定されたグローバルメモリオブジェクトを解放し、そのハンドルを無効にします。
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);
        #endregion

        #region プロパティ
        public static int HEIGHT = 980;

        public static int WIDTH = 600;

        public event SelectedChangeHandler OnSelectedChange;

        public event RemoveHandler OnRemove;

        //private IntPtr BmpHandle { get; set; } // ファイルパスのときはない
        //private IntPtr BmpPtr { set; get; } // ファイルパスのときはない
        public Bitmap Bmp { get; private set; }

        private Bitmap OriginalBitmap { set; get; }

        private PixelFormat OrignalPixcelFormat { get; set; }

        private string _fileName;
        public string FileName 
        {
            get
            {
                return _fileName;
            }
            private set
            {
                _fileName = value;
                ResetLabel();
            }
        }

        private int _no;
        public int No 
        {
            get
            {
                return _no;
            }
            set
            {
                _no = value;
                ResetLabel();
            }
        }

        private string Path { get; set; }

        private PictureBox PicCtl { set; get; }

        private Label Lbl { set; get; }

        private bool IsShirokuro { set; get; }

        private List<Panel> gaidoLines { set; get; }

        private ToolStripMenuItem SakujoMenu { get; set; }

        private ToolStripMenuItem ResetMenu { get; set; }

        private ToolStripMenuItem GridEnableMenu { get; set; }

        private ToolStripMenuItem GridDisableMenu { get; set; }

        private ToolStripMenuItem Rotete01RightMenu { get; set; }

        private ToolStripMenuItem Rotete01LeftMenu { get; set; }

        private ToolStripMenuItem Rotete1RightMenu { get; set; }

        private ToolStripMenuItem Rotete1LeftMenu { get; set; }

        private ToolStripMenuItem Rotete3RightMenu { get; set; }

        private ToolStripMenuItem Rotete3LeftMenu { get; set; }

        private ToolStripMenuItem Rotete5RightMenu { get; set; }

        private ToolStripMenuItem Rotete5LeftMenu { get; set; }

        private ToolStripMenuItem Rotete90RightMenu { get; set; }

        private ToolStripMenuItem AutoRotateMenu { get; set; }

        private bool _isSelect;
        public bool IsSelect
        {
            get
            {
                return _isSelect;
            }
            set 
            {
                _isSelect = value;
                if (this.OnSelectedChange != null) this.OnSelectedChange(this);
                Refresh();
            }
        }

        private bool IsAutoRotate { get; set; }
        #endregion

        #region イベント
        protected override void  OnMouseClick(MouseEventArgs e)
        {
            IsSelect = true;
            base.OnMouseClick(e);
        }

        void AutoRotateMenu_Click(object sender, EventArgs e)
        {
            AutoRotate();
        }

        void SakujoMenu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("削除？", "ImportFrame", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (this.OnRemove != null) this.OnRemove(this);
            }
        }

        void ResetMenu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("リセット？", "ImportFrame", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ResetEdit();
            }
        }

        void GridEnableMenu_Click(object sender, EventArgs e)
        {
            ViewGaidoLine(true);
        }

        void GridDisableMenu_Click(object sender, EventArgs e)
        {
            ViewGaidoLine(false);
        }

        void RoteteRightMenu_Click(object sender, EventArgs e)
        {
            string value = ((ToolStripMenuItem)sender).Text;
            float v = float.Parse(value.Substring(0, value.IndexOf(" ")));
            TurnPicture(v);
        }

        void RoteteLeftMenu_Click(object sender, EventArgs e)
        {
            string value = ((ToolStripMenuItem)sender).Text;
            float v = float.Parse(value.Substring(0, value.IndexOf(" ")));
            TurnPicture(v * -1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Pen p = new Pen(IsSelect ? Color.Red : Color.DarkSlateGray, 1);

            //位置(10, 20)に100x 80
            //g.DrawRectangle(p, 10, 20, 100, 80)
            e.Graphics.DrawRectangle(p, 3, 3, Width - 6, Height - 6);

            if (IsAutoRotate)
            {
                Color penColor = Color.MediumVioletRed; // 最大

                if (-0.3 <= angle && angle <= 0.3)
                {
                    penColor = Color.Goldenrod;
                }
                else if (-0.5 <= angle && angle <= 0.5)
                {
                    penColor = Color.GreenYellow;
                }
                else if (-1 <= angle && angle <= 1)
                {
                    penColor = Color.DeepSkyBlue;
                }
                else if (-1.5 <= angle && angle <= 1.5)
                {
                    penColor = Color.DarkViolet;
                }


                Pen p2 = new Pen(penColor, 2);
                //p2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                e.Graphics.DrawRectangle(p2, 1, 1, Width - 4, Height - 4);
                p2.Dispose();
            }

            e.Graphics.Dispose();
            p.Dispose();
        }
        #endregion

        private Bitmap CopyBitmap(Bitmap tmp)
        {
            //Bitmap result = new Bitmap(tmp.Width, tmp.Height);
            //result.SetResolution(tmp.HorizontalResolution, tmp.VerticalResolution);
            //Graphics g = Graphics.FromImage(result);
            //g.DrawImage(tmp, 0, 0, tmp.Width, tmp.Height);
            //g.Dispose();
            //return result;

            //    return CopyBitmapPixelFormat(tmp, tmp.PixelFormat);
            //}

            //private static Bitmap CopyBitmapPixelFormat(Bitmap tmp, PixelFormat format)
            //{
            Bitmap result = new Bitmap(tmp.Width, tmp.Height, tmp.PixelFormat);
            result.SetResolution(tmp.HorizontalResolution, tmp.VerticalResolution);

            if (result.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                ColorPalette pal = tmp.Palette;
                //ColorPalette pal = result.Palette;
                //for (int i = 0; i < tmp.Palette.Entries.Length; ++i)
                //{
                //    pal.Entries[i] = tmp.Palette.Entries[i];
                //    //pal.Entries[i] = Color.FromArgb(i,i,i);
                //    //pal.Entries[i] = Color.FromArgb(tmp.Palette.Entries[i].R, tmp.Palette.Entries[i].G, tmp.Palette.Entries[i].B);
                //}
                result.Palette = pal;
                //ColorPalette pal = result.Palette;
                //for (int i = 0; i < tmp.Palette.Entries.Length; ++i)
                //{
                //    pal.Entries[i] = Color.FromArgb(i, i, i);
                //}
                //result.Palette = pal;
            }

            Rectangle rectOriginal = new Rectangle(0, 0, tmp.Width, tmp.Height);
            Rectangle rectResult = new Rectangle(0, 0, tmp.Width, tmp.Height);

            BitmapData bmpDataOriginal = tmp.LockBits(rectOriginal, ImageLockMode.ReadWrite, tmp.PixelFormat);
            BitmapData bmpDataResult = result.LockBits(rectResult, ImageLockMode.ReadWrite, result.PixelFormat);

            int bytes = bmpDataOriginal.Stride * bmpDataOriginal.Height;
            byte[] bytesOriginal = new byte[bytes];
            Marshal.Copy(bmpDataOriginal.Scan0, bytesOriginal, 0, bytes);

            Marshal.Copy(bytesOriginal, 0, bmpDataResult.Scan0, bytes);

            tmp.UnlockBits(bmpDataOriginal);
            result.UnlockBits(bmpDataResult);
            return result;
        }

        #region コンストラクタ
        public ImportFrame(string path)
        {
            IsSaveEnd = true;

            Height = HEIGHT;
            Width = WIDTH;

            Path = path;

            Bmp = new Bitmap(path);

            OrignalPixcelFormat = Bmp.PixelFormat;

            FileName = System.IO.Path.GetFileName(path);

            IsSelect = false;

            IsAutoRotate = false;

            IsShirokuro = false;

            gaidoLines = new List<Panel>();
            int hLines = 30;
            while (hLines < Height)
            {
                Panel pnl = new Panel();
                pnl.Size = new Size(Width - 10, 1);
                pnl.Location = new Point(6, hLines);
                pnl.BackColor = Color.FromArgb(100, 0, 0, 255);
                pnl.Visible = false;

                gaidoLines.Add(pnl);
                Controls.Add(pnl);

                hLines += 30;
            }
            int wLines = 6;
            while (wLines < Height)
            {
                Panel pnl = new Panel();
                pnl.Size = new Size(1, HEIGHT - 30);
                pnl.Location = new Point(wLines, 6);
                pnl.BackColor = Color.FromArgb(100, 0, 0, 255);
                pnl.Visible = false;

                gaidoLines.Add(pnl);
                Controls.Add(pnl);

                wLines += 30;
            }

            PicCtl = new PictureBox();
            PicCtl.MouseClick += (object sender, MouseEventArgs e) =>
            {
                this.OnMouseClick(e);
            };
            PicCtl.Size = new Size(Width - 8, Height - 28);
            PicCtl.Location = new Point(4, 4);
            PicCtl.Image = Bmp;
            PicCtl.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(PicCtl);

            Lbl = new Label();
            Lbl.Location = new Point(4, PicCtl.Location.Y + PicCtl.Height + 2);
            Lbl.AutoSize = true;
            Controls.Add(Lbl);


            ContextMenuStrip = new ContextMenuStrip();

            Rotete01RightMenu = new ToolStripMenuItem("0.1 度右へ回転");
            Rotete01RightMenu.Click += new EventHandler(RoteteRightMenu_Click);
            Rotete01LeftMenu = new ToolStripMenuItem("0.1 度左へ回転");
            Rotete01LeftMenu.Click += new EventHandler(RoteteLeftMenu_Click);
            Rotete3RightMenu = new ToolStripMenuItem("0.3 度右へ回転");
            Rotete3RightMenu.Click += new EventHandler(RoteteRightMenu_Click);
            Rotete3LeftMenu = new ToolStripMenuItem("0.3 度左へ回転");
            Rotete3LeftMenu.Click += new EventHandler(RoteteLeftMenu_Click);
            Rotete5RightMenu = new ToolStripMenuItem("0.5 度右へ回転");
            Rotete5RightMenu.Click += new EventHandler(RoteteRightMenu_Click);
            Rotete5LeftMenu = new ToolStripMenuItem("0.5 度左へ回転");
            Rotete5LeftMenu.Click += new EventHandler(RoteteLeftMenu_Click);
            Rotete1RightMenu = new ToolStripMenuItem("1 度右へ回転");
            Rotete1RightMenu.Click += new EventHandler(RoteteRightMenu_Click);
            Rotete1LeftMenu = new ToolStripMenuItem("1 度左へ回転");
            Rotete1LeftMenu.Click += new EventHandler(RoteteLeftMenu_Click);
            Rotete90RightMenu = new ToolStripMenuItem("90 度右へ回転");
            Rotete90RightMenu.Click += new EventHandler(RoteteRightMenu_Click);
            AutoRotateMenu = new ToolStripMenuItem("自動角度補正");
            AutoRotateMenu.Click += new EventHandler(AutoRotateMenu_Click);
            ResetMenu = new ToolStripMenuItem("リセット");
            ResetMenu.Click += new EventHandler(ResetMenu_Click);
            GridEnableMenu = new ToolStripMenuItem("グリッド表示");
            GridEnableMenu.Click += new EventHandler(GridEnableMenu_Click);
            GridDisableMenu = new ToolStripMenuItem("グリッド非表示");
            GridDisableMenu.Click += new EventHandler(GridDisableMenu_Click);
            SakujoMenu = new ToolStripMenuItem("削除");
            SakujoMenu.Click += new EventHandler(SakujoMenu_Click);

            ContextMenuStrip.Items.Add(Rotete01RightMenu);
            ContextMenuStrip.Items.Add(Rotete01LeftMenu);
            ContextMenuStrip.Items.Add(Rotete3RightMenu);
            ContextMenuStrip.Items.Add(Rotete3LeftMenu);
            ContextMenuStrip.Items.Add(Rotete5RightMenu);
            ContextMenuStrip.Items.Add(Rotete5LeftMenu);
            ContextMenuStrip.Items.Add(Rotete1RightMenu);
            ContextMenuStrip.Items.Add(Rotete1LeftMenu);
            ContextMenuStrip.Items.Add(Rotete90RightMenu);
            ContextMenuStrip.Items.Add(AutoRotateMenu);
            ContextMenuStrip.Items.Add(ResetMenu);
            ContextMenuStrip.Items.Add(GridEnableMenu);
            ContextMenuStrip.Items.Add(GridDisableMenu);
            ContextMenuStrip.Items.Add(SakujoMenu);
        }

        #endregion

        #region 画像編集

        /// <summary>
        /// 白黒反転画像の取得
        /// ８Bitのみ対応
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        private Bitmap GetShiroKuroBitMap(Bitmap original, double shikiichi, bool kyouseiShirokuro)
        {
            if (original.PixelFormat != PixelFormat.Format8bppIndexed) return null;

            Bitmap bitmap = CopyBitmap(original);

            //ビットマップデータ
            //このブロックは、イメージを各ピクセルごとに記述する。
            //ピクセルは通常左から右へ、下から上に向かって保存されている。
            //各ピクセルは1バイト以上で記述されている。
            //もし水平方向のバイト数が4の倍数ではないときは、ヌル (0x00)で埋めて4の倍数にする

            //Strideを使うと１ラインあたりのバイト数が取れる

            BitmapData bmpData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite,
                    bitmap.PixelFormat);

            int byteCnt = bmpData.Stride * bmpData.Height;
            byte[] bytes = new byte[byteCnt];
            Marshal.Copy(bmpData.Scan0, bytes, 0, byteCnt);

            for(int i = 0; i < 2; i++)
            {
                int shiroCnt = 0;
                int koroCnt = 0;
                int width = bitmap.Width;
                int height = bitmap.Height;
                for (int y = 0; y < bmpData.Height; y++)
                {
                    for (int x = 0; x < bmpData.Stride; x++)
                    {
                        if ((int)bytes[y * bmpData.Stride + x] > shikiichi)
                        {
                            if (i == 0)
                            {
                                bytes[y * bmpData.Stride + x] = 0;
                            }
                            else
                            {
                                bytes[y * bmpData.Stride + x] = 255;
                            }
                            koroCnt++;
                        }
                        else
                        {
                            if (i == 0)
                            {
                                bytes[y * bmpData.Stride + x] = 255;
                            }
                            else
                            {
                                bytes[y * bmpData.Stride + x] = 0;
                            }
                            shiroCnt++;
                        }
                    }
                }
                if (kyouseiShirokuro == false) break;
                if (koroCnt >= shiroCnt) break;
            }

            Marshal.Copy(bytes, 0, bmpData.Scan0, byteCnt);

            bitmap.UnlockBits(bmpData);

            return bitmap;
        }

        private Rectangle GetWidthPoint(Bitmap bitmap)
        {
            if (bitmap.PixelFormat != PixelFormat.Format8bppIndexed) return Rectangle.Empty;

            // 横線
            BitmapData bmpData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite,
                    bitmap.PixelFormat);

            int byteCnt = bmpData.Stride * bmpData.Height;
            byte[] bytes = new byte[byteCnt];
            Marshal.Copy(bmpData.Scan0, bytes, 0, byteCnt);


            // 程よい長さの横線を取得する
            int nextXpoint = 0;
            Point startWhitePoint = new Point();
            Point endWhitePoint = new Point();
            for (int y = 0; y < bmpData.Height; y++)
            {
                for (int x = nextXpoint; x < bmpData.Stride; x++)
                {
                    if (startWhitePoint.IsEmpty && x > bmpData.Stride / 2) break;
                    if (startWhitePoint.IsEmpty)
                    {
                        if (bytes[y * bmpData.Stride + x] == 255)
                        {
                            startWhitePoint = new Point(x, y);
                            endWhitePoint = new Point(x, y);
                            break;
                        }
                    }
                }
                // 終点の検索
                for(int x = startWhitePoint.X; x < bmpData.Stride; x++) 
                {
                    // 上下５ピクセル分の高さ白い部分を捜す。どこかが白ならOK
                    bool isWhite = false;
                    for (int h = Math.Max(endWhitePoint.Y - 5, 0); h < Math.Min(endWhitePoint.Y + 6, bmpData.Height); h++)
                    {
                        if (bytes[h * bmpData.Stride + x] == 255)
                        {
                            if (endWhitePoint.Y - 2 < h && h < endWhitePoint.Y + 2)
                            {
                                bool isLine = false;
                                for (int l = h; l < Math.Min(h + 5, bmpData.Height); l++)
                                {
                                    if (bytes[l * bmpData.Stride + x] == 0)
                                    {
                                        isLine = true;
                                        break;
                                    }
                                }
                                if (isLine == false) continue;
                                isWhite = true;
                                endWhitePoint = new Point(x, h);
                                break;
                            }
                        }
                    }

                    // 白が見つからなくなったら
                    if (isWhite == false)
                    {
                        nextXpoint = x++;
                        // 十分な長さ見つかったかどうか？
                        if (endWhitePoint.X - startWhitePoint.X < bitmap.Width / 5)
                        {
                            // 見つからなかった
                            startWhitePoint = new Point();
                            endWhitePoint = new Point();
                        }
                        break;
                    }
                }
                if (startWhitePoint.IsEmpty == false && endWhitePoint.IsEmpty == false)
                {
                    // 始点の延長
                    for (int x = startWhitePoint.X; x >= 0; x--)
                    {
                        // 上下５ピクセル分の高さ白い部分を捜す。どこかが白ならOK
                        bool isWhite = false;
                        for (int h = Math.Max(startWhitePoint.Y - 5, 0); h < Math.Min(startWhitePoint.Y + 6, bmpData.Height); h++)
                        {
                            if (bytes[h * bmpData.Stride + x] == 255)
                            {
                                if (startWhitePoint.Y - 2 < h && h < startWhitePoint.Y + 2)
                                {
                                    bool isLine = false;
                                    for (int l = h; l < h + 5; l++)
                                    {
                                        if (bytes[l * bmpData.Stride + x] == 0)
                                        {
                                            isLine = true;
                                            break;
                                        }
                                    }
                                    if (isLine == false) continue;
                                    isWhite = true;
                                    startWhitePoint = new Point(x, h);
                                    break;
                                }
                            }
                        }

                        // 白が見つからなくなったら
                        if (isWhite == false)
                        {
                            break;
                        }
                    }
                    break;
                }
            } // End 高さ

            //// テスト
            //if (startWhitePoint.IsEmpty == false && endWhitePoint.IsEmpty == false)
            //{
            //    ////for (int x = startWhitePoint.X; x < endWhitePoint.X; x++)
            //    //for (int x = 0; x < bmpData.Stride; x++)
            //    //{
            //    //    bytes[startWhitePoint.Y * bmpData.Stride + x] = 156;
            //    //}

            //    for (int y = Math.Max(startWhitePoint.Y - 50, 0); y < Math.Min(startWhitePoint.Y + 50, bmpData.Height); y++)
            //    {
            //        for (int x = Math.Max(startWhitePoint.X - 50, 0); x < Math.Min(startWhitePoint.X + 50, bmpData.Stride); x++)
            //        {
            //            bytes[y * bmpData.Stride + x] = 156;
            //        }
            //    }

            //    for (int y = Math.Max(endWhitePoint.Y - 50, 0); y < Math.Min(endWhitePoint.Y + 50, bmpData.Height); y++)
            //    {
            //        for (int x = Math.Max(endWhitePoint.X - 50, 0); x < Math.Min(endWhitePoint.X + 50, bmpData.Stride); x++)
            //        {
            //            bytes[y * bmpData.Stride + x] = 216;
            //        }
            //    }
            //}
            //Marshal.Copy(bytes, 0, bmpData.Scan0, byteCnt); // テスト

            bitmap.UnlockBits(bmpData);

            //ResetBitmpa(DibToImage.CopyBitmap(bitmap)); // テスト

            if (startWhitePoint.IsEmpty == false && endWhitePoint.IsEmpty == false)
            {
                Rectangle r = new Rectangle();
                r.X = startWhitePoint.X;
                r.Y = startWhitePoint.Y;
                r.Width = endWhitePoint.X;
                r.Height = endWhitePoint.Y;

                return r;
            }
            return Rectangle.Empty;
        }

        private double GetBitmapAngle(Bitmap bitmap)
        {
            Rectangle r = GetWidthPoint(bitmap);
            if (r.IsEmpty == false)
            {
                int dx = r.Width - r.X;
                int dy = r.Height - r.Y;
                double radian = Math.Atan2(dy, dx);
                radian = radian * (180 / Math.PI);

                return radian;
            }

            return 0;
        }

        /// <summary>
        /// 8Bitのみ対応
        /// </summary>
        public void AutoRotate()
        {
            if (Bmp.PixelFormat != PixelFormat.Format8bppIndexed) return;

            Bitmap bp = GetShiroKuroBitMap(Bmp, 255 / 2, true);

            double bmpAngle = GetBitmapAngle(bp);

            bmpAngle = bmpAngle * -1 + (bmpAngle > 0 ? 0.1 : -0.1);

            if ((bmpAngle < 0 ? bmpAngle * -1 : bmpAngle) > 0.25 && (bmpAngle < 0 ? bmpAngle * -1 : bmpAngle) < 3)
            {
                // 補正
                TurnPicture(bmpAngle);

                IsAutoRotate = true;
            }

            bp.Dispose();
        }

        /// <summary>
        /// ビットマップ(Bitmap)を回転する
        /// 8Bit解除されちゃうけどあきらめる。。。。
        /// </summary>
        /// <param name="bmp">ビットマップ</param>
        /// <param name="angle">回転角度</param>
        /// <param name="x">中心点Ｘ</param>
        /// <param name="y">中心点Ｙ</param>
        /// <returns></returns>
        private Bitmap RotateBitmap(Bitmap img, double angle)
        {
            Bitmap bmp2 = new Bitmap((int)img.Width, (int)img.Height);
            bmp2.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            Graphics g = Graphics.FromImage(bmp2);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.Clear(Color.White);

            //ラジアン単位に変換
            double d = angle / (180 / Math.PI);
            //新しい座標位置を計算する
            float x = (float)angle;
            float y = 0F;
            float x1 = x + img.Width * (float)Math.Cos(d);
            float y1 = y + img.Width * (float)Math.Sin(d);
            float x2 = x - img.Height * (float)Math.Sin(d);
            float y2 = y + img.Height * (float)Math.Cos(d);
            //PointF配列を作成
            PointF[] destinationPoints = {new PointF(x, y),
                    new PointF(x1, y1),
                    new PointF(x2, y2)};
            //画像を表示
            g.DrawImage(img, destinationPoints);
            //g.DrawImage(img, 0, 0, img.Width, img.Height);
            g.Dispose();

            return bmp2;
        }

        private double angle = 0;

        /// <summary>
        /// 負の場合左回転
        /// </summary>
        /// <param name="value"></param>
        public void TurnPicture(double value)
        {
            if (OriginalBitmap == null)
            {
                OriginalBitmap = CopyBitmap(Bmp);
            }
            angle += value;
            ResetBitmpa(RotateBitmap(OriginalBitmap, angle));
        }

        public void ResetEdit()
        {
            angle = 0F;
            if (OriginalBitmap == null) return;
            ResetBitmpa(OriginalBitmap);
            OriginalBitmap.Dispose();
            OriginalBitmap = null;
            IsAutoRotate = false;
            Refresh();
        }

        public void EndEdit()
        {
            angle = 0F;
            if (OriginalBitmap == null) return;
            OriginalBitmap.Dispose();
            OriginalBitmap = null;
        }
        #endregion

        #region 出力
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public bool IsSaveEnd { get; set; }

        /// <summary>
        /// 8Bitグレイスケールの保存ができない誰か教えて。。。
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="quality"></param>
        public void SaveBackground(string filePath, Int64 quality)
        {
            IsSaveEnd = false;

            Bitmap bmp = CopyBitmap(this.Bmp);

            bool isGrayscale = OrignalPixcelFormat == PixelFormat.Format8bppIndexed; // いろいろ専用。。。
            //bool isGrayscale = bmp.PixelFormat == PixelFormat.Format8bppIndexed; // いろいろ専用。。。

            bool isSaveCompleate = false;
            bool isTranseCompleate = false;

            System.ComponentModel.BackgroundWorker saveBk = new System.ComponentModel.BackgroundWorker();
            System.ComponentModel.BackgroundWorker transBk = new System.ComponentModel.BackgroundWorker();
            System.ComponentModel.BackgroundWorker processBk = new System.ComponentModel.BackgroundWorker();

            processBk.DoWork += (object sender, System.ComponentModel.DoWorkEventArgs e) =>
            {
                while (isSaveCompleate == false)
                {
                    System.Threading.Thread.Sleep(100);
                }

                transBk.RunWorkerAsync();

            };

            saveBk.DoWork += (object sender, System.ComponentModel.DoWorkEventArgs e) =>
            {
                System.Diagnostics.Debug.WriteLine("saveBk.DoWork");

                ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID
                // for the Quality parameter category.
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.
                // An EncoderParameters object has an array of EncoderParameter
                // objects. In this case, there is only one
                // EncoderParameter object in the array.
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                myEncoderParameters.Param[0] = myEncoderParameter;

                bmp.Save(filePath + ".tmp", jgpEncoder, myEncoderParameters);

                myEncoderParameter.Dispose();
                myEncoderParameters.Dispose();

                bmp.Dispose();

                isSaveCompleate = true;
            };

            transBk.DoWork += (object sender, System.ComponentModel.DoWorkEventArgs e) =>
            {
                System.Diagnostics.Debug.WriteLine("transBk.DoWork");

                // jpegtran.exe の場所
                string jpegtran = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\jpegtran.exe";

                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                //psi.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
                psi.FileName = jpegtran;
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                // jpegtran.exe で処理
                //psi.Arguments = string.Format((isGrayscale ? " -grayscale" : "") + " -optimize -progressive \"{0}\" \"{1}\""
                //                                , filePath + ".tmp", filePath);
                psi.Arguments = string.Format((isGrayscale ? " -grayscale" : "") + " -optimize \"{0}\" \"{1}\""
                                                , filePath + ".tmp", filePath);
                System.Diagnostics.Process ps = System.Diagnostics.Process.Start(psi);
                ps.WaitForExit();
                ps.Dispose();

                System.IO.File.Delete(filePath + ".tmp");

                isTranseCompleate = true;
            };

            saveBk.RunWorkerCompleted += (object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) =>
            {
                System.Diagnostics.Debug.WriteLine("saveBk.RunWorkerCompleted");

                saveBk.Dispose();
            };


            transBk.RunWorkerCompleted += (object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) =>
            {
                System.Diagnostics.Debug.WriteLine("transBk.RunWorkerCompleted");

                transBk.Dispose();
            };


            processBk.RunWorkerCompleted += (object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) =>
            {
                System.Diagnostics.Debug.WriteLine("processBk.RunWorkerCompleted");

                while (isTranseCompleate == false)
                {
                    System.Threading.Thread.Sleep(100);
                }

                IsSaveEnd = true;

                processBk.Dispose();
            };

            processBk.RunWorkerAsync();

            saveBk.RunWorkerAsync();
        }
        #endregion

        public void ViewGaidoLine(bool visible)
        {
            for (int i = gaidoLines.Count - 1; i >= 0; i--)
            {
                gaidoLines[i].Visible = visible;
            }
        }

        public void ViewShirokuro(bool visible)
        {
            if (IsShirokuro == visible) return;

            IsShirokuro = visible;

            Bitmap moto = (OriginalBitmap == null ? Bmp : OriginalBitmap);

            if (visible)
            {

                Bitmap bp = GetShiroKuroBitMap(moto, 60, false);

                if (OriginalBitmap == null)
                {
                    OriginalBitmap = CopyBitmap(Bmp);
                }

                ResetBitmpa(bp);

                bp.Dispose();
            }
            else
            {
                ResetBitmpa(angle != 0 ? RotateBitmap(moto, angle) : moto);
            }
        }

        public void ResetBitmpa(Bitmap ori)
        {
            Bmp.Dispose();
            Bmp = null;

            Bmp = CopyBitmap(ori);

            PicCtl.Image.Dispose();
            PicCtl.Image = Bmp;

            ResetLabel();
        }

        public void ResetLabel()
        {
            if (Lbl == null) return;
            Lbl.Text = No.ToString() + ". " + FileName + " " + Math.Ceiling(Bmp.HorizontalResolution).ToString() + "dpi x " + Math.Ceiling(Bmp.VerticalResolution).ToString() + "dpi "
                            + Bmp.Width.ToString() + "ピクセル x " + Bmp.Height.ToString() + "ピクセル " + Bmp.PixelFormat.ToString();
            if (angle != 0)
            {
                Lbl.Text += " " + angle.ToString() + "度回転";
            }
            if (Path != null && Path != "")
            {
                System.IO.FileInfo info = new System.IO.FileInfo(Path);
                Lbl.Text += " " + (info.Length / 1024) + "KB";
                info = null;
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (int i = gaidoLines.Count - 1; i >= 0; i--)
                {
                    Panel pn = gaidoLines[i];

                    Controls.Remove(pn);

                    pn.Dispose();

                    gaidoLines.Remove(pn);
                }
                gaidoLines.Clear();

                Controls.Remove(PicCtl);

                PicCtl.Image.Dispose();

                Bmp.Dispose();
                Bmp = null;

                PicCtl.Dispose();
                PicCtl = null;

                Controls.Remove(Lbl);

                Lbl.Dispose();
                Lbl = null;

                ContextMenuStrip.Items.Remove(SakujoMenu);
                SakujoMenu.Dispose();

                ContextMenuStrip.Items.Remove(ResetMenu);
                ResetMenu.Dispose();

                ContextMenuStrip.Items.Remove(GridEnableMenu);
                GridEnableMenu.Dispose();

                ContextMenuStrip.Items.Remove(GridDisableMenu);
                GridDisableMenu.Dispose();

                ContextMenuStrip.Items.Remove(Rotete1LeftMenu);
                Rotete1LeftMenu.Dispose();

                ContextMenuStrip.Items.Remove(Rotete1RightMenu);
                Rotete1RightMenu.Dispose();

                ContextMenuStrip.Items.Remove(Rotete3LeftMenu);
                Rotete3LeftMenu.Dispose();

                ContextMenuStrip.Items.Remove(Rotete3RightMenu);
                Rotete3RightMenu.Dispose();

                ContextMenuStrip.Items.Remove(Rotete5LeftMenu);
                Rotete5LeftMenu.Dispose();

                ContextMenuStrip.Items.Remove(Rotete5RightMenu);
                Rotete5RightMenu.Dispose();

                ContextMenuStrip.Items.Remove(Rotete90RightMenu);
                Rotete90RightMenu.Dispose();

                ContextMenuStrip.Items.Remove(Rotete01LeftMenu);
                Rotete01LeftMenu.Dispose();

                ContextMenuStrip.Items.Remove(Rotete01RightMenu);
                Rotete01RightMenu.Dispose();

                ContextMenuStrip.Items.Remove(AutoRotateMenu);
                AutoRotateMenu.Dispose();

                ContextMenuStrip.Dispose();

                if (OriginalBitmap != null)
                {
                    OriginalBitmap.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}

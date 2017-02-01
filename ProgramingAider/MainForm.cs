using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Diagnostics;
using Ionic.Zip;

namespace ProgramingAider {
    public partial class MainForm : Form {
        Setting setting = new Setting();

        public MainForm() {
            InitializeComponent();
        }

        #region イベントハンドラ
        /// <summary>
        /// 終了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            setting.AutoFillVer = checkBoxAuto.Checked;
            save();
        }

        /// <summary>
        /// 起動時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e) {
            checkBoxAuto.CheckedChanged -= checkBoxAuto_CheckedChanged;
            load();
            checkBoxAuto.Checked = setting.AutoFillVer;
            setSoftwareIconToPictureBox();
            updateComboBox(0);
            checkBoxAuto.CheckedChanged += checkBoxAuto_CheckedChanged;
        }

        /// <summary>
        /// オプション表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionToolStripMenuItem_Click(object sender, EventArgs e) {
            int index = comboBoxSolution.SelectedIndex;
            Solution solution = setting.SolutionList[index];
            OptionForm of = new OptionForm(setting, index);
            of.ShowDialog();
            setting = of.Setting;

            setSoftwareIconToPictureBox();

            // 最後に選択していたものと同じソリューションがあれば選択する
            index = 0;
            foreach (Solution s in setting.SolutionList) {
                if (s.id == solution.id) break;
                ++index;
            }
            updateComboBox(index);
        }

        /// <summary>
        /// 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitEToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 選択されたソリューションに画面表示を合わせる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSolution_SelectedIndexChanged(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            textBoxLastVer.Text = solution.LastVer.ToString();
            if (checkBoxAuto.Checked) textBoxNewVer.Text = solution.NewVer.ToString();
            clearButton();
            updateButtonForSelectedSolution();

            // アイコン
            Size pBSize = buttonIDE.Size;
            Bitmap errorBmp = getErrorBitmap(pBSize);
            if (File.Exists(solution.Programing.ExeFile))
                setIconToButton(Icon.ExtractAssociatedIcon(solution.Programing.ExeFile).ToBitmap(), buttonExe);
            else
                setIconToButton(errorBmp, buttonExe);
        }

        /// <summary>
        /// 記入されたバージョンを新バージョンとして登録する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxVer_Leave(object sender, EventArgs e) {
            String str = textBoxNewVer.Text;
            if (str == String.Empty) return;

            try {
                Solution solution = getSelectedSolution();
                // バージョン登録
                solution.NewVer = new Version(str);
                // ツールチップの圧縮ファイル名変更
                String fromZip, toZip;
                getZipPath(out fromZip, out toZip);
                if (fromZip != String.Empty && solution.Publishing.ZipDir != String.Empty)
                    toolTip1.SetToolTip(buttonMoveZip, "Copy\n from\t: " + fromZip + "\n to\t: " + toZip);
            }catch(Exception){
                textBoxNewVer.Text = getSelectedSolution().LastVer.ToString(Version.Changing.Maintenance);
            }
        }

        /// <summary>
        /// バージョンを自動補完する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAuto_CheckedChanged(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if(checkBoxAuto.Checked) textBoxNewVer.Text = solution.NewVer.ToString();
        }

        #region ファイルオープン
        /// <summary>
        /// EXEファイル起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExe_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String file = solution.Programing.ExeFile;
            if (File.Exists(file)) Process.Start(file);
        }

        /// <summary>
        /// IDE起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonIDE_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String folder = solution.Programing.SolutionDir;
            String[] file = Directory.GetFiles(folder, "*.sln");
            if (file.Length != 0) Process.Start(setting.Software.Ide, file[0]);
        }

        /// <summary>
        /// プログラムディレクトリ起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrgDir_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String folder = solution.Programing.SolutionDir;
            if(folder != String.Empty) Process.Start(folder);
        }

        /// <summary>
        /// EXEファイル移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMoveExe_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String fromExe, toExe;
            getExePath(out fromExe, out toExe);
            if (File.Exists(fromExe)) {
                if (File.Exists(toExe)) {
                    string text = toExe + " have already exited.";
                    if (MessageBox.Show(text, "Copy", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.No)
                        return;
                }
                File.Copy(fromExe, toExe, true);
            }
        }

        /// <summary>
        /// read meファイル起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReadMe_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String file = solution.Distribution.ReadMeFile;
            if(!File.Exists(setting.Software.TextEditor))
                MessageBox.Show(setting.Software.TextEditor + " isn't exists.", "TextEditor");
            else if(file != String.Empty) Process.Start(setting.Software.TextEditor, file);
        }

        /// <summary>
        /// 圧縮元ディレクトリ起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCmpDirFrom_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String folder = solution.Distribution.FromDir;
            if(folder != String.Empty || Directory.Exists(folder)) Process.Start(folder);
        }

        /// <summary>
        /// ディレクトリ圧縮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCompress_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            ZipFile zip = new ZipFile();
            if(Directory.Exists(solution.Distribution.FromDir)){
                zip.AddDirectory(solution.Distribution.FromDir);
                if(File.Exists(solution.Distribution.ToFile)){
                    string text = solution.Distribution.ToFile + " have already exited.";
                    if (MessageBox.Show(text, "Compression", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.No)
                        return;
                }
                zip.Save(solution.Distribution.ToFile);
            }
        }

        /// <summary>
        /// 圧縮先ディレクトリ起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCmpDirTo_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String file = solution.Distribution.ToFile;
            String folder = Path.GetDirectoryName(file);
            if (file != String.Empty) Process.Start(folder);
        }

        /// <summary>
        /// 圧縮ファイル移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMoveZip_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String fromZip, toZip;
            getZipPath(out fromZip, out toZip);

            if (File.Exists(fromZip)) {
                if (File.Exists(toZip)) {
                    string text = toZip + " have already exited.";
                    if (MessageBox.Show(text, "Copy", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                        return;
                }
                File.Copy(fromZip, toZip, true);

                String text2 = "Would you update last and new version?";
                if (MessageBox.Show(text2, "Update", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
                    updateVersionNumber();
                }
            }
        }

        /// <summary>
        /// HTMLエディタ起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditor_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String file = solution.Publishing.HTMLFile;
            if(!File.Exists(setting.Software.HtmlEditor))
                MessageBox.Show(setting.Software.HtmlEditor + " isn't exists.", "HtmlEditor");
            else if (file != String.Empty)
                Process.Start(setting.Software.HtmlEditor, file);
        }

        /// <summary>
        /// サイトディレクトリ起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPbDir_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String file = solution.Publishing.HTMLFile;
            String folder = Path.GetDirectoryName(file);
            if (file != String.Empty) Process.Start(folder);
        }

        /// <summary>
        /// アップローダ起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUploader_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            if(!File.Exists(setting.Software.Uploader))
                MessageBox.Show(setting.Software.Uploader + " isn't exists.", "Uploader");
            else
                Process.Start(setting.Software.Uploader);
        }

        /// <summary>
        /// ブラウザ起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowser_Click(object sender, EventArgs e) {
            Solution solution = getSelectedSolution();
            if (solution == null) return;

            String url = solution.Publishing.WebPage;
            if(!File.Exists(setting.Software.Browser))
                MessageBox.Show(setting.Software.Browser + " isn't exists.", "Browser");
            else if(url != String.Empty)
                Process.Start(setting.Software.Browser, url);
        }
        #endregion
        #endregion

        /// <summary>
        /// 最新版と新版を新しくする
        /// </summary>
        void updateVersionNumber() {
            Solution solution = getSelectedSolution();

            solution.LastVer = solution.NewVer;
            textBoxLastVer.Text = solution.LastVer.ToString();
            if (setting.AutoFillVer) {
                solution.NewVer = new Version(solution.LastVer, Version.Changing.Maintenance);
                textBoxNewVer.Text = solution.NewVer.ToString();
            }
            updateButtonForSelectedSolution();
        }

        /// <summary>
        /// 選択されているソリューションを得る
        /// </summary>
        /// <returns></returns>
        Solution getSelectedSolution() {
            if (comboBoxSolution.SelectedIndex < 0 || setting.SolutionList.Count <= comboBoxSolution.SelectedIndex) return null;
            return setting.SolutionList[comboBoxSolution.SelectedIndex];
        }

        /// <summary>
        /// ソフトウェアアイコンを表示する
        /// </summary>
        private void setSoftwareIconToPictureBox() {
            Size pBSize = buttonIDE.Size;
            Bitmap errorBmp = getErrorBitmap(pBSize);
            Bitmap folderBmp = new Bitmap(this.GetType(), "folder.jpg");

            // Programing
            if (File.Exists(setting.Software.Ide))
                setIconToButton(Icon.ExtractAssociatedIcon(setting.Software.Ide).ToBitmap(), buttonIDE);
            else
                setIconToButton(errorBmp, buttonIDE);
            setIconToButton(folderBmp, buttonPrgDir);

            // Distribution
            if (File.Exists(setting.Software.TextEditor))
                setIconToButton(Icon.ExtractAssociatedIcon(setting.Software.TextEditor).ToBitmap(), buttonReadMe);
            else
                setIconToButton(errorBmp, buttonReadMe);
            setIconToButton(folderBmp, buttonCmpDirFrom);
            setIconToButton(folderBmp, buttonCmpDirTo);

            // Publishing
            if (File.Exists(setting.Software.HtmlEditor))
                setIconToButton(Icon.ExtractAssociatedIcon(setting.Software.HtmlEditor).ToBitmap(), buttonEditor);
            else
                setIconToButton(errorBmp, buttonEditor);
            setIconToButton(folderBmp, buttonPbDir);
            if (File.Exists(setting.Software.Uploader))
                setIconToButton(Icon.ExtractAssociatedIcon(setting.Software.Uploader).ToBitmap(), buttonUploader);
            else
                setIconToButton(errorBmp, buttonUploader);
            if (File.Exists(setting.Software.Browser))
                setIconToButton(Icon.ExtractAssociatedIcon(setting.Software.Browser).ToBitmap(), buttonBrowser);
            else
                setIconToButton(errorBmp, buttonBrowser);
        }

        /// <summary>
        /// アイコンを表示する
        /// </summary>
        /// <param name="filePath">アイコン画像</param>
        /// <param name="pictureBox">表示する領域</param>
        private void setIconToPictureBox(Bitmap bmpIcon, PictureBox pictureBox) {
            Size pBSize = pictureBox.Size;
            Bitmap toBitmap = new Bitmap(pBSize.Width, pBSize.Height);
            Graphics g = Graphics.FromImage(toBitmap);
            Bitmap bmp = new Bitmap(bmpIcon, pBSize.Width / 2, pBSize.Height / 2);
            g.DrawImage(bmp, pBSize.Width / 4, pBSize.Height / 4);
            bmp.Dispose();
            g.Dispose();
            pictureBox.Image = toBitmap;
        }

        /// <summary>
        /// アイコンを表示する
        /// </summary>
        /// <param name="filePath">アイコン画像</param>
        /// <param name="pictureBox">表示する領域</param>
        private void setIconToButton(Bitmap bmpIcon, Button button) {
            Size pBSize = button.Size;
            Bitmap toBitmap = new Bitmap(pBSize.Width, pBSize.Height);
            Graphics g = Graphics.FromImage(toBitmap);
            Bitmap bmp = new Bitmap(bmpIcon, pBSize.Width / 2, pBSize.Height / 2);
            g.DrawImage(bmp, pBSize.Width / 4, pBSize.Height / 4);
            bmp.Dispose();
            g.Dispose();
            button.BackgroundImage = toBitmap;
        }

        /// <summary>
        /// エラー表示用の画像を得る
        /// </summary>
        /// <param name="size">画像のサイズ</param>
        /// <returns>エラー画像</returns>
        private Bitmap getErrorBitmap(Size size) {
            Bitmap errorBmp = new Bitmap(size.Width / 2, size.Height / 2);
            Graphics g = Graphics.FromImage(errorBmp);
            Pen linePen = new Pen(Brushes.Red, 2);
            g.DrawLine(linePen, 0, 0, size.Width / 2, size.Height / 2);
            g.DrawLine(linePen, size.Width / 2 - 1, 0, 0, size.Height / 2 - 1);
            Pen rectPen = new Pen(Brushes.Black, 2);
            g.DrawRectangle(rectPen, 0, 0, size.Width / 2, size.Height / 2);
            g.Dispose();
            return errorBmp;
        }

        /// <summary>
        /// 設定ファイルセーブ
        /// </summary>
        private void save() {
            FileStream fs = new FileStream("setting.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(Setting));
            serializer.Serialize(fs, setting);
            fs.Close();
        }

        /// <summary>
        /// 設定ファイルロード
        /// </summary>
        private void load() {
            String fileName = "setting.xml";
            if(File.Exists(fileName)){
                FileStream fs = new FileStream(fileName, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(Setting));
                setting = (Setting)serializer.Deserialize(fs);
                fs.Close();
            }
        }

        /// <summary>
        /// コンボボックスの表示を更新する
        /// </summary>
        private void updateComboBox(int index) {
            comboBoxSolution.SelectedIndexChanged -= comboBoxSolution_SelectedIndexChanged;
            comboBoxSolution.Items.Clear();
            foreach (Solution solution in setting.SolutionList) {
                comboBoxSolution.Items.Add(comboBoxSolution.Items.Count + ":" + solution.Name);
            }
            // 引数の値を調整
            if (index < 0) index = 0;
            if (comboBoxSolution.Items.Count <= index) index = comboBoxSolution.Items.Count - 1;
            
            // ソリューションが存在しないとき
            if (comboBoxSolution.Items.Count == 0) {
                comboBoxSolution.Items.Add("(none)");
                comboBoxSolution.SelectedIndex = 0;
            }
            // ソリューションが存在するとき
            else {
                comboBoxSolution.SelectedIndex = index;
                Solution solution = getSelectedSolution();
                //バージョンを設定
                textBoxLastVer.Text = solution.LastVer.ToString();
                if (checkBoxAuto.Checked)
                    textBoxNewVer.Text = solution.NewVer.ToString();
                // アイコンを設定
                Size pBSize = buttonIDE.Size;
                Bitmap errorBmp = getErrorBitmap(pBSize);
                if (File.Exists(solution.Programing.ExeFile))
                    setIconToButton(Icon.ExtractAssociatedIcon(solution.Programing.ExeFile).ToBitmap(), buttonExe);
                else
                    setIconToButton(errorBmp, buttonExe);
            }
            clearButton();
            updateButtonForSelectedSolution();
            comboBoxSolution.SelectedIndexChanged += comboBoxSolution_SelectedIndexChanged;
        }

        /// <summary>
        /// ボタンの表示を選択されているソリューションに合わせる
        /// </summary>
        private void updateButtonForSelectedSolution() {
            Solution selectedSolution = getSelectedSolution();
            if (selectedSolution == null) return;

            // 実行ファイル
            toolTip1.SetToolTip(buttonExe, selectedSolution.Name + ":" + selectedSolution.Programing.ExeFile);

            // プログラミング
            String folder = selectedSolution.Programing.SolutionDir;
            String[] file = Directory.GetFiles(folder, "*.sln");
            if (file.Length != 0 && file[0] != String.Empty) {
                toolTip1.SetToolTip(buttonIDE, "IDE: " + file[0]);
                buttonIDE.Enabled = true;
            }
            if (selectedSolution.Programing.SolutionDir != String.Empty) {
                toolTip1.SetToolTip(buttonPrgDir, "Dir.: " + selectedSolution.Programing.SolutionDir);
                buttonPrgDir.Enabled = true;
            }

            // コピー
            String fromExe, toExe;
            getExePath(out fromExe, out toExe);
            if (fromExe != String.Empty && selectedSolution.Distribution.FromDir != String.Empty) {
                toolTip1.SetToolTip(buttonMoveExe, "Copy\n from\t: " + fromExe + "\n to\t: " + toExe);
                buttonMoveExe.Enabled = true;
            }

            // 配布
            if (selectedSolution.Distribution.ReadMeFile != String.Empty) {
                toolTip1.SetToolTip(buttonReadMe, "read me: " + selectedSolution.Distribution.ReadMeFile);
                buttonReadMe.Enabled = true;
            }
            if (selectedSolution.Distribution.FromDir != String.Empty) {
                toolTip1.SetToolTip(buttonCmpDirFrom, "Dir. (from): " + selectedSolution.Distribution.FromDir);
                buttonCmpDirFrom.Enabled = true;
            }
            if (selectedSolution.Distribution.FromDir != String.Empty && selectedSolution.Distribution.ToFile != String.Empty) {
                toolTip1.SetToolTip(buttonCompress, "Compress\n from\t: " + selectedSolution.Distribution.FromDir + "\n to\t: " + selectedSolution.Distribution.ToFile);
                buttonCompress.Enabled = true;
            }
            if (selectedSolution.Distribution.ToFile != String.Empty) {
                toolTip1.SetToolTip(buttonCmpDirTo, "Dir. (to): " + Path.GetDirectoryName(selectedSolution.Distribution.ToFile));
                buttonCmpDirTo.Enabled = true;
            }

            // コピー
            String fromZip, toZip;
            getZipPath(out fromZip, out toZip);
            if (fromZip != String.Empty && selectedSolution.Publishing.ZipDir != String.Empty) {
                toolTip1.SetToolTip(buttonMoveZip, "Copy\n from\t: " + fromZip + "\n to\t: " + toZip);
                buttonMoveZip.Enabled = true;
            }

            // 公開
            if (selectedSolution.Publishing.HTMLFile != String.Empty) {
                toolTip1.SetToolTip(buttonEditor, "Editor: " + selectedSolution.Publishing.HTMLFile);
                buttonEditor.Enabled = true;
            }
            if (selectedSolution.Publishing.HTMLFile != String.Empty) {
                toolTip1.SetToolTip(buttonPbDir, "Dir.: " + Path.GetDirectoryName(selectedSolution.Publishing.HTMLFile));
                buttonPbDir.Enabled = true;
            }
            if(selectedSolution.Publishing.WebPage != String.Empty){
                toolTip1.SetToolTip(buttonUploader, "Uploader: " + setting.Software.Uploader);
                buttonUploader.Enabled = true;
            }
            if (selectedSolution.Publishing.WebPage != String.Empty) {
                toolTip1.SetToolTip(buttonBrowser, "Browse: " + selectedSolution.Publishing.WebPage);
                buttonBrowser.Enabled = true;
            }
        }

        /// <summary>
        /// ボタンを初期状態に戻す
        /// </summary>
        private void clearButton() {
            toolTip1.SetToolTip(buttonExe, "");

            // プログラミング
            toolTip1.SetToolTip(buttonIDE, "IDE");
            buttonIDE.Enabled = false;
            toolTip1.SetToolTip(buttonPrgDir, "Dir.");
            buttonPrgDir.Enabled = false;

            toolTip1.SetToolTip(buttonMoveExe, "Copy");
            buttonMoveExe.Enabled = false;

            // 配布
            toolTip1.SetToolTip(buttonReadMe, "read me");
            buttonReadMe.Enabled = false;
            toolTip1.SetToolTip(buttonCmpDirFrom, "Dir. (from)");
            buttonCmpDirFrom.Enabled = false;
            toolTip1.SetToolTip(buttonCompress, "Compress");
            buttonCompress.Enabled = false;
            toolTip1.SetToolTip(buttonCmpDirTo, "Dir. (to)");
            buttonCmpDirTo.Enabled = false;

            toolTip1.SetToolTip(buttonMoveZip, "Copy");
            buttonMoveZip.Enabled = false;

            // 公開
            toolTip1.SetToolTip(buttonEditor, "Editor");
            buttonEditor.Enabled = false;
            toolTip1.SetToolTip(buttonPbDir, "Dir.");
            buttonPbDir.Enabled = false;
            toolTip1.SetToolTip(buttonUploader, "Uploader");
            buttonUploader.Enabled = false;
            toolTip1.SetToolTip(buttonBrowser, "Browse");
            buttonBrowser.Enabled = false;
        }

        /// <summary>
        /// 実行ファイルパスを取得する
        /// </summary>
        /// <param name="from">移動元実行ファイル</param>
        /// <param name="to">移動先実行ファイル</param>
        private void getExePath(out String from, out String to) {
            Solution solution = getSelectedSolution();
            from = solution.Programing.ExeFile;
            to = solution.Distribution.FromDir + "\\" + Path.GetFileName(from);
        }

        /// <summary>
        /// 圧縮ファイルパスを取得する
        /// </summary>
        /// <param name="from">圧縮元ファイル</param>
        /// <param name="to">圧縮後ファイル</param>
        private void getZipPath(out String from, out String to) {
            Solution solution = getSelectedSolution();
            from = solution.Distribution.ToFile;
            to = solution.Publishing.ZipDir + "\\" + Path.GetFileNameWithoutExtension(from) + solution.NewVer.ToInt().ToString("00000") + ".zip";
        }
    }
}

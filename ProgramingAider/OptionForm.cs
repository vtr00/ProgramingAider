using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace ProgramingAider {
    public partial class OptionForm : Form {
        int lastSelectedIndex = -1;
        private Setting setting;

        public Setting Setting { get { return setting; } }

        public OptionForm(Setting setting, int index) {
            this.setting = setting;
            if(setting.SolutionList.Count <= index) index = setting.SolutionList.Count - 1;
            lastSelectedIndex = index;
            InitializeComponent();
        }

        #region イベントハンドラ
        private void OptionForm_Load(object sender, EventArgs e) {
            CopyFromSetting();
            UpdateComboBoxSolutions(lastSelectedIndex);
        }

        private void OptionForm_FormClosing(object sender, FormClosingEventArgs e) {
            CopyToSetting();
            if(lastSelectedIndex != -1)
                CopyToSolution(setting.SolutionList[lastSelectedIndex]);
        }

        #region ソフトウェア ブラウズ
        /// <summary>
        /// IDE指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonIDE_Click(object sender, EventArgs e) {
            if (textBoxIDE.Text != String.Empty) {
                openFileDialogIDE.InitialDirectory = Path.GetDirectoryName(textBoxIDE.Text);
                openFileDialogIDE.FileName = Path.GetFileName(textBoxIDE.Text);
            }
            if(openFileDialogIDE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBoxIDE.Text = openFileDialogIDE.FileName;
        }

        /// <summary>
        /// テキストエディタ指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTextEditor_Click(object sender, EventArgs e) {
            if(textBoxTextEditor.Text != String.Empty){
                openFileDialogTextEditor.InitialDirectory = Path.GetDirectoryName(textBoxTextEditor.Text);
                openFileDialogTextEditor.FileName = Path.GetFileName(textBoxTextEditor.Text);
            }
            if (openFileDialogTextEditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBoxTextEditor.Text = openFileDialogTextEditor.FileName;
        }

        /// <summary>
        /// HTMLエディタ指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHTMLEditor_Click(object sender, EventArgs e) {
            if (textBoxHTMLEditor.Text != String.Empty){
                openFileDialogHTMLEditor.InitialDirectory = Path.GetDirectoryName(textBoxHTMLEditor.Text);
                openFileDialogHTMLEditor.FileName = Path.GetFileName(textBoxHTMLEditor.Text);
            }
            if (openFileDialogHTMLEditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBoxHTMLEditor.Text = openFileDialogHTMLEditor.FileName;
        }

        /// <summary>
        /// アップローダ指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUploader_Click(object sender, EventArgs e) {
            if (textBoxUploader.Text != String.Empty){
                openFileDialogUploader.InitialDirectory = Path.GetDirectoryName(textBoxUploader.Text);
                openFileDialogUploader.FileName = Path.GetFileName(textBoxUploader.Text);
            }
            if (openFileDialogUploader.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBoxUploader.Text = openFileDialogUploader.FileName;
        }

        /// <summary>
        /// ブラウザ指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowser_Click(object sender, EventArgs e) {
            if (textBoxBrowser.Text != String.Empty){
                openFileDialogBrowser.InitialDirectory = Path.GetDirectoryName(textBoxBrowser.Text);
                openFileDialogBrowser.FileName = Path.GetFileName(textBoxBrowser.Text); 
            }
            if (openFileDialogBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBoxBrowser.Text = openFileDialogBrowser.FileName;
        }
        #endregion

        #region ソリューション ブラウズ
        /// <summary>
        /// ソリューションディレクトリ指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSolutionDir_Click(object sender, EventArgs e) {
            folderBrowserDialogSolutionDir.SelectedPath = textBoxSolutionDir.Text;

            if (folderBrowserDialogSolutionDir.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBoxSolutionDir.Text = folderBrowserDialogSolutionDir.SelectedPath;
                if (checkBoxAuto.Checked) {
                    String solutionName = Path.GetFileName(textBoxSolutionDir.Text);
                    // exeファイル補完
                    String exeFile = textBoxSolutionDir.Text + @"\" + solutionName + @"\bin\Release\" + solutionName + @".exe";
                    if (File.Exists(exeFile) && textBoxExeFile.Text == String.Empty)
                        textBoxExeFile.Text = exeFile;
                    // ソリューション名補完
                    if (textBoxName.Text == String.Empty || textBoxName.Text == "notitle")
                        textBoxName.Text = solutionName;
                }
            }
        }

        /// <summary>
        /// EXEファイル指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExeFile_Click(object sender, EventArgs e) {
            if (textBoxExeFile.Text != String.Empty) {
                openFileDialogExeFile.InitialDirectory = Path.GetDirectoryName(textBoxExeFile.Text);
                openFileDialogExeFile.FileName = Path.GetFileName(textBoxExeFile.Text);
            } else
                openFileDialogExeFile.InitialDirectory = folderBrowserDialogSolutionDir.SelectedPath;

            if (openFileDialogExeFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBoxExeFile.Text = openFileDialogExeFile.FileName;
        }

        /// <summary>
        /// 圧縮元ディレクトリ指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFromDir_Click(object sender, EventArgs e) {
            folderBrowserDialogFromDir.SelectedPath = textBoxFromDir.Text;

            if (folderBrowserDialogFromDir.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBoxFromDir.Text = folderBrowserDialogFromDir.SelectedPath;
                if (checkBoxAuto.Checked) {
                    // readmeファイル補完
                    String readmeFile = textBoxFromDir.Text + @"\readme.txt";
                    if (File.Exists(readmeFile) && textBoxReadMe.Text == String.Empty)
                        textBoxReadMe.Text = readmeFile;
                }
            }
        }

        /// <summary>
        /// readmeファイル指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReadMe_Click(object sender, EventArgs e) {
            if (textBoxReadMe.Text != String.Empty) {
                openFileDialogReadMe.InitialDirectory = Path.GetDirectoryName(textBoxReadMe.Text);
                openFileDialogReadMe.FileName = Path.GetFileName(textBoxReadMe.Text);
            } else
                openFileDialogReadMe.InitialDirectory = textBoxFromDir.Text;

            if (openFileDialogReadMe.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBoxReadMe.Text = openFileDialogReadMe.FileName;
        }

        /// <summary>
        /// 圧縮先ファイル指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonToFile_Click(object sender, EventArgs e) {
            if (textBoxToFile.Text != String.Empty) {
                openFileDialogToFile.InitialDirectory = Path.GetDirectoryName(textBoxToFile.Text);
                openFileDialogToFile.FileName = Path.GetFileName(textBoxToFile.Text);
            } else
                openFileDialogToFile.InitialDirectory = textBoxFromDir.Text;

            if (openFileDialogToFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBoxToFile.Text = openFileDialogToFile.FileName;
        }

        /// <summary>
        /// ZIPファイルディレクトリ指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonZipDir_Click(object sender, EventArgs e) {
            folderBrowserDialogZipDir.SelectedPath = textBoxZipDir.Text;

            if (folderBrowserDialogZipDir.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                textBoxZipDir.Text = folderBrowserDialogZipDir.SelectedPath;
                if (checkBoxAuto.Checked) {
                    // バージョン補完
                    String[] filePathes = Directory.GetFiles(textBoxZipDir.Text, "*.zip", SearchOption.TopDirectoryOnly);
                    int maxVersion = 0;
                    foreach (String filePath in filePathes) {
                        String fileName = Path.GetFileNameWithoutExtension(filePath);
                        int version = Convert.ToInt32(Regex.Replace(fileName, @"[^0-9]", ""));
                        if (version > maxVersion) maxVersion = version;
                    }
                    textBoxVersion.Text = new Version(maxVersion).ToString();
                }
            }
        }

        /// <summary>
        /// HTMLファイル指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHTMLFile_Click(object sender, EventArgs e) {
            if (textBoxHTMLFile.Text != String.Empty) {
                openFileDialogHTMLFile.InitialDirectory = Path.GetDirectoryName(textBoxHTMLFile.Text);
                openFileDialogHTMLFile.FileName = Path.GetFileName(textBoxHTMLFile.Text);
            } else
                openFileDialogHTMLFile.InitialDirectory = textBoxZipDir.Text;

            if (openFileDialogHTMLFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBoxHTMLFile.Text = openFileDialogHTMLFile.FileName;
        }
        #endregion

        /// <summary>
        /// コンボボックスに合わせて表示を変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSolutions_SelectedIndexChanged(object sender, EventArgs e) {
            if (setting.SolutionList.Count != 0) {
                CopyToSolution(setting.SolutionList[lastSelectedIndex]);
                UpdateComboBoxSolutions(comboBoxSolutions.SelectedIndex);
            } else
                lastSelectedIndex = comboBoxSolutions.SelectedIndex;
        }

        /// <summary>
        /// ソリューションを追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e) {
            if(lastSelectedIndex != -1)
                CopyToSolution(setting.SolutionList[lastSelectedIndex]);

            Solution solution = new Solution();
            solution.Name = "notitle";
            setting.SolutionList.Add(solution);
            UpdateComboBoxSolutions(setting.SolutionList.Count - 1);
            EnableSolution();
        }

        /// <summary>
        /// ソリューションを削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e) {
            setting.SolutionList.RemoveAt(lastSelectedIndex);
            if (setting.SolutionList.Count == 0) {
                UpdateComboBoxSolutions(-1);
            } else {
                if(setting.SolutionList.Count <= lastSelectedIndex) --lastSelectedIndex;
                UpdateComboBoxSolutions(lastSelectedIndex);
            }
        }

        /// <summary>
        /// ソリューション名を動的に変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxName_TextChanged(object sender, EventArgs e) {
            comboBoxSolutions.Items[comboBoxSolutions.SelectedIndex] = comboBoxSolutions.Text.Split(':')[0] + ":" + textBoxName.Text;
        }

        /// <summary>
        /// 設定ウィンドウを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e) {
            Close();
        }
        #endregion

        /// <summary>
        /// コンボボックスの表示を更新する
        /// </summary>
        /// <param name="index">表示するインデックス</param>
        private void UpdateComboBoxSolutions(int index) {
            comboBoxSolutions.SelectedIndexChanged -= comboBoxSolutions_SelectedIndexChanged;
            comboBoxSolutions.Items.Clear();
            foreach (Solution solution in setting.SolutionList)
                comboBoxSolutions.Items.Add(comboBoxSolutions.Items.Count + ":" + solution.Name);
            if (comboBoxSolutions.Items.Count <= index) index = comboBoxSolutions.Items.Count - 1;
            if (comboBoxSolutions.Items.Count == 0) {
                comboBoxSolutions.Items.Add("(none)");
                comboBoxSolutions.SelectedIndex = 0;
                CopyFromSolution(new Solution());
                DisableSolution();
            } else {
                comboBoxSolutions.SelectedIndex = index;
                CopyFromSolution(setting.SolutionList[index]);
            }
            lastSelectedIndex = index;
            comboBoxSolutions.SelectedIndexChanged += comboBoxSolutions_SelectedIndexChanged;
        }

        /// <summary>
        /// settingの内容をフォームにコピーする
        /// </summary>
        private void CopyFromSetting() {
            checkBoxAuto.Checked = setting.AutoFillPath;
            textBoxIDE.Text = setting.Software.Ide;
            textBoxTextEditor.Text = setting.Software.TextEditor;
            textBoxHTMLEditor.Text = setting.Software.HtmlEditor;
            textBoxUploader.Text = setting.Software.Uploader;
            textBoxBrowser.Text = setting.Software.Browser;
        }

        /// <summary>
        /// ソリューションの内容をフォームにコピーする
        /// </summary>
        /// <param name="solution"></param>
        private void CopyFromSolution(Solution solution) {
            textBoxName.Text = solution.Name;
            textBoxVersion.Text = solution.LastVer.ToString();
            textBoxSolutionDir.Text = solution.Programing.SolutionDir;
            textBoxExeFile.Text = solution.Programing.ExeFile;
            textBoxFromDir.Text = solution.Distribution.FromDir;
            textBoxReadMe.Text = solution.Distribution.ReadMeFile;
            textBoxToFile.Text = solution.Distribution.ToFile;
            textBoxZipDir.Text = solution.Publishing.ZipDir;
            textBoxHTMLFile.Text = solution.Publishing.HTMLFile;
            textBoxWebPage.Text = solution.Publishing.WebPage;
        }

        /// <summary>
        /// フォームの設定をsettingにコピーする
        /// </summary>
        private void CopyToSetting() {
            setting.AutoFillPath = checkBoxAuto.Checked;
            setting.Software.Ide = textBoxIDE.Text;
            setting.Software.TextEditor = textBoxTextEditor.Text;
            setting.Software.HtmlEditor = textBoxHTMLEditor.Text;
            setting.Software.Uploader = textBoxUploader.Text;
            setting.Software.Browser = textBoxBrowser.Text;
        }

        /// <summary>
        /// フォームの設定をソリューションにコピーする
        /// </summary>
        /// <returns></returns>
        private void CopyToSolution(Solution solution) {
            solution.Name = textBoxName.Text;
            solution.LastVer = new Version(textBoxVersion.Text);
            solution.NewVer = new Version(solution.LastVer, Version.Changing.Maintenance);
            solution.Programing.SolutionDir = textBoxSolutionDir.Text;
            solution.Programing.ExeFile = textBoxExeFile.Text;
            solution.Distribution.FromDir = textBoxFromDir.Text;
            solution.Distribution.ReadMeFile = textBoxReadMe.Text;
            solution.Distribution.ToFile = textBoxToFile.Text;
            solution.Publishing.ZipDir = textBoxZipDir.Text;
            solution.Publishing.HTMLFile = textBoxHTMLFile.Text;
            solution.Publishing.WebPage = textBoxWebPage.Text;
        }

        /// <summary>
        /// ソリューションタブを使用可能にする
        /// </summary>
        private void EnableSolution() {
            textBoxName.Enabled = true;
            textBoxVersion.Enabled = true;
            textBoxSolutionDir.Enabled = true;
            buttonSolutionDir.Enabled = true;
            textBoxExeFile.Enabled = true;
            buttonExeFile.Enabled = true;
            textBoxFromDir.Enabled = true;
            buttonFromDir.Enabled = true;
            textBoxReadMe.Enabled = true;
            buttonReadMe.Enabled = true;
            textBoxToFile.Enabled = true;
            buttonToFile.Enabled = true;
            textBoxZipDir.Enabled = true;
            buttonZipDir.Enabled = true;
            textBoxHTMLFile.Enabled = true;
            buttonHTMLFile.Enabled = true;
            textBoxWebPage.Enabled = true;
        }

        /// <summary>
        /// ソリューションタブを使用不可にする
        /// </summary>
        private void DisableSolution() {
            textBoxName.Enabled = false;
            textBoxVersion.Enabled = false;
            textBoxSolutionDir.Enabled = false;
            buttonSolutionDir.Enabled = false;
            textBoxExeFile.Enabled = false;
            buttonExeFile.Enabled = false;
            textBoxFromDir.Enabled = false;
            buttonFromDir.Enabled = false;
            textBoxReadMe.Enabled = false;
            buttonReadMe.Enabled = false;
            textBoxToFile.Enabled = false;
            buttonToFile.Enabled = false;
            textBoxZipDir.Enabled = false;
            buttonZipDir.Enabled = false;
            textBoxHTMLFile.Enabled = false;
            buttonHTMLFile.Enabled = false;
            textBoxWebPage.Enabled = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace ProgramingAider {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    /// <summary>
    /// バージョン
    /// </summary>
    public class Version {
        public enum Changing {
            Major, Minor, Maintenance
        }

        /// <summary>
        /// メジャーバージョン
        /// </summary>
        public int Major { get; set; }
        /// <summary>
        /// マイナーバージョン
        /// </summary>
        public int Minor { get; set; }
        /// <summary>
        /// メンテナンスバージョン
        /// </summary>
        public int Maintenance { get; set; }

        public Version() {
            Major = 0;
            Minor = 0;
            Maintenance = 0;
        }

        public Version(string ver) {
            string[] vers = ver.Split('.');
            if (vers.Length != 3) {
                throw new ArgumentException("input error");
            }
            Major = Convert.ToInt32(vers[0]);
            Minor = Convert.ToInt32(vers[1]);
            Maintenance = Convert.ToInt32(vers[2]);
            return;
        }

        public Version(int ver) {
            Major = ver / 10000;
            Minor = ver % 10000 / 100;
            Maintenance = ver % 100;
            return;
        }

        public Version(Version oldVer, Changing? changing = null) {
            int major = oldVer.Major;
            int minor = oldVer.Minor;
            int maintenance = oldVer.Maintenance;

            switch (changing) {
                case Changing.Major:
                    ++major;
                    minor = 0;
                    maintenance = 0;
                    break;
                case Changing.Minor:
                    ++minor;
                    maintenance = 0;
                    break;
                case Changing.Maintenance:
                    ++maintenance;
                    break;
            }

            Major = major;
            Minor = minor;
            Maintenance = maintenance;
        }

        public int ToInt(Changing? changing = null) {
            int major = Major;
            int minor = Minor;
            int maintenance = Maintenance;

            switch (changing) {
                case Changing.Major:
                    ++major;
                    minor = 0;
                    maintenance = 0;
                    break;
                case Changing.Minor:
                    ++minor;
                    maintenance = 0;
                    break;
                case Changing.Maintenance:
                    ++maintenance;
                    break;
            }

            return major * 10000 + minor * 100 + maintenance;
        }

        public string ToString(Changing? changing = null) {
            int major = Major;
            int minor = Minor;
            int maintenance = Maintenance;

            switch (changing) {
                case Changing.Major:
                    ++major;
                    minor = 0;
                    maintenance = 0;
                    break;
                case Changing.Minor:
                    ++minor;
                    maintenance = 0;
                    break;
                case Changing.Maintenance:
                    ++maintenance;
                    break;
            }

            return major + "." + String.Format("{0:00}", minor) + "." + String.Format("{0:00}", maintenance);
        }
    }

    /// <summary>
    /// ソリューション
    /// </summary>
    public class Solution {
        /// <summary>
        /// プログラミング
        /// </summary>
        public class ProgramingPath {
            /// <summary>
            /// ソリューションディレクトリ
            /// </summary>
            public string SolutionDir { set; get; }
            /// <summary>
            /// 実行ファイル
            /// </summary>
            public string ExeFile { set; get; }

            public ProgramingPath() {
                SolutionDir = "";
                ExeFile = "";
            }
        }
        /// <summary>
        /// 配布
        /// </summary>
        public class DistributionPath {
            /// <summary>
            /// 圧縮元ディレクトリ
            /// </summary>
            public string FromDir { set; get; }
            /// <summary>
            /// readmeファイル
            /// </summary>
            public string ReadMeFile { set; get; }
            /// <summary>
            /// 圧縮先ファイル
            /// </summary>
            public string ToFile { set; get; }

            public DistributionPath() {
                FromDir = "";
                ReadMeFile = "";
                ToFile = "";
            }
        }
        /// <summary>
        /// 公開
        /// </summary>
        public class PublishingPath {
            /// <summary>
            /// 圧縮ファイルディレクトリ
            /// </summary>
            public string ZipDir { set; get; }
            /// <summary>
            /// HTMLファイル
            /// </summary>
            public string HTMLFile { set; get; }
            /// <summary>
            /// 公開ページ
            /// </summary>
            public string WebPage { set; get; }

            public PublishingPath (){
                ZipDir = "";
                HTMLFile = "";
                WebPage = "";
            }
        }

        [XmlIgnore]
        static int max_id = 0;
        [XmlIgnore]
        public int id = max_id;
        /// <summary>
        /// ソリューション名
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 新バージョン
        /// </summary>
        public Version NewVer { set; get; }
        /// <summary>
        /// 旧バージョン
        /// </summary>
        public Version LastVer { set; get; }
        /// <summary>
        /// プログラミング
        /// </summary>
        public ProgramingPath Programing { set; get; }
        /// <summary>
        /// 配布
        /// </summary>
        public DistributionPath Distribution { set; get; }
        /// <summary>
        /// 公開
        /// </summary>
        public PublishingPath Publishing { set; get; }

        /// <summary>
        /// ソリューション コンストラクタ
        /// </summary>
        public Solution() {
            Name = "";
            NewVer = new Version("0.0.0");
            LastVer = new Version("0.0.0");
            Programing = new ProgramingPath();
            Distribution = new DistributionPath();
            Publishing = new PublishingPath();
            ++max_id;
        }
    }

    /// <summary>
    /// ソフトウェア
    /// </summary>
    public class SoftwarePath {
        /// <summary>
        /// IDE
        /// </summary>
        public string Ide { set; get; }
        /// <summary>
        /// テキストエディタ
        /// </summary>
        public string TextEditor { set; get; }
        /// <summary>
        /// HTMLエディタ
        /// </summary>
        public string HtmlEditor { set; get; }
        /// <summary>
        /// アップローダ
        /// </summary>
        public string Uploader { set; get; }
        /// <summary>
        /// ブラウザ
        /// </summary>
        public string Browser { set; get; }

        /// <summary>
        /// ソフトウェア コンストラクタ
        /// </summary>
        public SoftwarePath() {
            Ide = "";
            TextEditor = "";
            HtmlEditor = "";
            Uploader = "";
            Browser = "";
        }
    }

    /// <summary>
    /// 設定
    /// </summary>
    public class Setting {
        /// <summary>
        /// 自動補完(パス)
        /// </summary>
        public bool AutoFillPath { set; get; }
        /// <summary>
        /// 自動補完(バージョン)
        /// </summary>
        public bool AutoFillVer { set; get; }
        /// <summary>
        /// ソフトウェア
        /// </summary>
        public SoftwarePath Software { set; get; }
        /// <summary>
        /// ソリューション群
        /// </summary>
        public List<Solution> SolutionList { set; get; }
        
        /// <summary>
        /// 設定 コンストラクタ
        /// </summary>
        public Setting() {
            AutoFillPath = true;
            AutoFillVer = true;
            Software = new SoftwarePath();
            SolutionList = new List<Solution>();
        }
    }
}

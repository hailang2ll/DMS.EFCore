using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word2013 = Microsoft.Office.Interop.Word;

namespace WindowsFormsApp1
{
    public class WordHelper
    {
        private Microsoft.Office.Interop.Word.Application _wordApplication;
        private Microsoft.Office.Interop.Word.Document _wordDocument;
        object nullobj = System.Reflection.Missing.Value;
        public WordHelper(string fileName)
        {
            this._wordApplication = new Microsoft.Office.Interop.Word.Application();
            object objFile = fileName;
            this._wordDocument = _wordApplication.Documents.Open(ref objFile, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);
        }
        /// <summary>
        /// 进行与类创建时指定的文件比较，然后将差异显示在file，并保存退出
        /// </summary>
        /// <param name="file"></param>
        public void Compare(string file)
        {
            object compareTarget = Microsoft.Office.Interop.Word.WdCompareTarget.wdCompareTargetSelected;
            object addToRecentFiles = false;
            _wordDocument.Compare(file, ref nullobj, ref compareTarget, ref nullobj, ref nullobj, ref addToRecentFiles, ref nullobj, ref nullobj);

            object objfile = file;
            this._wordDocument = _wordApplication.Documents.Open(ref objfile, ref nullobj, ref nullobj,
               ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj,
               ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);
            _wordApplication.NormalTemplate.Saved = true;
            _wordDocument.Save();
            object saveOption = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            _wordApplication.Quit(ref saveOption, ref nullobj, ref nullobj);
        }

        /// <summary>
        /// 接受修订
        /// 需重新创建WordHelper类，指定文件
        /// </summary>
        public void AcceptRevised()
        {
            _wordDocument.AcceptAllRevisions();
            _wordDocument.Save();
            object saveOption = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            _wordApplication.Quit(ref saveOption, ref nullobj, ref nullobj);
        }
    }

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //加载之前首先判断系统中是否包含WORD.EXE进程，如果包含，将其杀死,然后再进行查看
            KillProcess();
            //MessageBox.Show(CompareWordFile(@"d:\01- 北辰项目技术标第1部分（资信部分）.doc", @"d:\津北 应征文件 - 技术文件.docx").ToString());

            string file1 = "d:\\1.docx";
            string file2 = "d:\\2.docx";
            string outfile = "d:\\out.pdf";
            PDF.WordCompare(file1, file2, outfile);


            //WordHelper wordHelper = new WordHelper(file1);
            //wordHelper.Compare(file2);
            KillProcess();
        }

        public bool CompareWordFile(String sourceFile, String targetFile)
        {
            object missing = System.Reflection.Missing.Value;
            object sFileName = sourceFile;
            var tFileName = targetFile;
            var wordApp = new Word2013.Application();
            wordApp.Caption = "CompareWordApp";
            wordApp.Visible = false;

            var wordDoc = wordApp.Documents.Open(ref sFileName, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing);

            wordDoc.TrackRevisions = true;
            wordDoc.ShowRevisions = true;
            wordDoc.PrintRevisions = true;

            object comparetarget = Word2013.WdCompareTarget.wdCompareTargetSelected;
            object addToRecentFiles = false;
            wordDoc.Compare(tFileName, ref missing, ref comparetarget, ref missing, ref missing, ref addToRecentFiles,
                ref missing, ref missing);

            int changeCount = wordApp.ActiveDocument.Revisions.Count;
            Object saveChanges = Word2013.WdSaveOptions.wdSaveChanges;
            wordDoc.Close(ref saveChanges, ref missing, ref missing);
            wordApp.Quit(ref saveChanges, ref missing, ref missing);

            return changeCount == 0;


        }

        public void KillProcess()
        {
            const string processName = "CompareWordApp";
            var process = Process.GetProcessesByName(processName);
            try
            {
                foreach (var p in process)
                {
                    p.Kill();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("请先关闭系统中的WINWORD.EXE进程!", "文件对比失败", MessageBoxButtons.OK);
                return;
            }
        }




    }
}

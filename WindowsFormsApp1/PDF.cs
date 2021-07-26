using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace WindowsFormsApp1
{
    public static class PDF
    {
        /// <summary>
        /// 比对两个word文件差异
        /// </summary>
        /// <param name="docPath1">word文件1路径</param>
        /// <param name="docPath2">word文件2路径</param>
        /// <param name="comparePath">比对结果路径</param>
        /// <returns></returns>
        public static int WordCompare(string docPath1, string docPath2, string comparePath)
        {
            try
            {
                if (!System.IO.File.Exists(docPath1.ToString()))
                {
                    throw new Exception("源文件不存在！");
                }
                if (!System.IO.File.Exists(docPath2.ToString()))
                {
                    throw new Exception("对比文件不存在！");
                }

                string dir = comparePath.Substring(0, comparePath.LastIndexOf("\\"));
                if (!System.IO.File.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                Microsoft.Office.Interop.Word.Application _wordApplication;
                Microsoft.Office.Interop.Word.Document _wordDocument1;
                Microsoft.Office.Interop.Word.Document _wordDocument2;
                Microsoft.Office.Interop.Word.Document _compareDocument;
                object nullobj = System.Reflection.Missing.Value;

                _wordApplication = new Microsoft.Office.Interop.Word.Application();
                //_wordApplication.Caption = "CompareWordApp";
                //_wordApplication.Visible = false;

                _wordDocument1 = _wordApplication.Documents.Open(docPath1, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj,
                 ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);

                _wordDocument2 = _wordApplication.Documents.Open(docPath2, ref nullobj, ref nullobj,
                ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj,
                 ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);

                _compareDocument = _wordApplication.CompareDocuments(_wordDocument1, _wordDocument2, Microsoft.Office.Interop.Word.WdCompareDestination.wdCompareDestinationNew, Microsoft.Office.Interop.Word.WdGranularity.wdGranularityWordLevel, true, true, true, true, true, true, true, true, true, true, "肖浪", true);



                //int changeCount = _wordApplication.ActiveDocument.Revisions.Count;
                //string title = _wordApplication.ActiveDocument.RevisedDocumentTitle;
                //var content = _wordApplication.ActiveDocument.GetLetterContent();


                object FileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                object LockComments = false;
                object AddToRecentFiles = true;
                object ReadOnlyRecommended = false;
                object EmbedTrueTypeFonts = false;
                object SaveNativePictureFormat = true;
                object SaveFormsData = true;
                object SaveAsAOCELetter = false;
                object Encoding = nullobj;
                object InsertLineBreaks = false;
                object AllowSubstitutions = false;
                object LineEnding = nullobj;
                object AddBiDiMarks = false;
                _compareDocument.SaveAs(comparePath, FileFormat, LockComments,
                        nullobj, AddToRecentFiles, nullobj,
                        ReadOnlyRecommended, EmbedTrueTypeFonts,
                        SaveNativePictureFormat, SaveFormsData,
                        SaveAsAOCELetter, Encoding, InsertLineBreaks,
                        AllowSubstitutions, LineEnding, AddBiDiMarks);
                _compareDocument.Close(true, ref nullobj, ref nullobj);//关闭word
                _wordApplication.Quit(true, ref nullobj, ref nullobj);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Core;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json.Linq;
using Util.Log;

namespace WPFDemo.View
{
    /// <summary>
    /// Interaction logic for InsertFileToMongodb.xaml
    /// </summary>
    public partial class InsertFileToMongodb : ThemedWindow
    {
        public InsertFileToMongodb()
        {
            InitializeComponent();
        }

        #region Property

        private string files_id { get; set; }

        #endregion

        #region Event

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                //设置默认文件类型
                //dlg.DefaultExt = ".png";
                Nullable<bool> isOpenDlg = dlg.ShowDialog();
                if (isOpenDlg == true)
                {
                    string file_name = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                    this.txtFile.Text = file_name;
                    string ext = Path.GetExtension(dlg.FileName);
                    string sourceFile = dlg.FileName;
                    using (var fs = new FileStream(sourceFile, FileMode.Open))
                    {
                        string msg = string.Empty;
                        JObject insertObj = new JObject();
                        insertObj.Add("collection_name", "fs");
                        insertObj.Add("file_name", file_name);
                        insertObj.Add("ext", ext);
                        files_id = Util.MongoDB.BLL.MongoDataBLL.InsertFile(fs, insertObj, out msg);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                DXMessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region 1.组织查询条件

                JObject queryObj = new JObject();
                queryObj.Add("collection_name", "fs.chunks");
                queryObj.Add("files_id", files_id);

                #endregion

                #region 2.获得数据

                string msg = string.Empty;
                List<Util.MongoDB.Model.Fs_Chunks> ls_data = Util.MongoDB.BLL.MongoDataBLL.GetCollectionInfo<Util.MongoDB.Model.Fs_Chunks>(queryObj, out msg);
                if (ls_data == null)
                {
                    DXMessageBox.Show("没有查询到数据", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                #endregion

                #region 3.转换word，在线显示

                Stream destStream = new MemoryStream();
                BinaryWriter binary = new BinaryWriter(destStream);
                for (int i = 0; i < ls_data.Count; i++)
                {
                    binary.Write(ls_data[i].data);
                }
                richEdit.LoadDocument(destStream);

                binary.Close();
                destStream.Close();
                #endregion
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                DXMessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}

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

namespace WPFDemo.View
{
    /// <summary>
    /// Interaction logic for ViewMovie.xaml
    /// </summary>
    public partial class ViewMovie : ThemedWindow
    {
        public ViewMovie()
        {
            InitializeComponent();
        }

        #region Property

        private string full_path { get; set; }

        #endregion

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            //设置默认文件类型
            dlg.Filter = "Video File(*.avi;*.mp4;*.mkv;*.wav;*.rmvb)|*.avi;*.mp4;*.mkv;*.wav;*.rmvb|All File(*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                string ext = Path.GetExtension(dlg.FileName);
                this.txtFile.Text = fileName + "." + ext;
                full_path = dlg.FileName;
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MediaPlayer.Source = new Uri(full_path);
                MediaPlayer.Play();
            }
            catch (Exception ex)
            {
                //LogProxy.Error(ex.Message);
                DXMessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            // Get the lenght of the video
            int duration = MediaPlayer.NaturalDuration.TimeSpan.Seconds;
        }
    }
}

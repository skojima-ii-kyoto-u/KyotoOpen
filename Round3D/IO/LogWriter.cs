using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Round3D.Logic;

namespace Round3D.IO
{
    public class LogWriter
    {
        private string path;

        public LogWriter(string path)
        {
            this.path = (path == "")? "log.txt" : path;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void AddLog(string content)
        {
            StreamWriter streamWriter;
            try
            {
                using (streamWriter = new StreamWriter(this.path, true))
                {
                    streamWriter.WriteLine(content);
                }
            }
            catch(DirectoryNotFoundException e)
            {
                MessageBoxResult value = MessageBox.Show(
                    string.Format(
                        "ファイル {0} を開くときにエラーが発生しました。\n" +
                        "ファイルが存在するディレクトリが正しくない可能性があります。", path),
                    e.Message,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                if (value == MessageBoxResult.OK)
                    Environment.Exit(1);
            }
            catch(Exception e)
            {
                MessageBoxResult value = MessageBox.Show(
                    string.Format(e.ToString()),
                    e.Message,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                if (value == MessageBoxResult.OK)
                    Environment.Exit(1);
            }
        }
    }
}

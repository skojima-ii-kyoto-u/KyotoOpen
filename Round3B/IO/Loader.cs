using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Round3B.Logic;

namespace Round3B.IO
{
    public class Loader
    {
        public static List<Player> LoadPlayer(string path)
        {
            if (!File.Exists(path))
            {
                MessageBoxResult value = MessageBox.Show(
                    string.Format("ファイル {0} が見つかりません。", path),
                    "エラー",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                if (value == MessageBoxResult.OK)
                    Environment.Exit(1);
            }

            StreamReader reader = new StreamReader(path);
            List<Player> result = new List<Player>();
            string str = "";
            while ((str = reader.ReadLine()) != null) {
                //"チーム名"（"プレイヤー1"/"プレイヤー2"）
                foreach(Match m in Regex.Matches(str, @".+（.+/.+）"))
                {
                    int index3 = str.LastIndexOf('）');
                    int index2 = str.LastIndexOf('/', index3);
                    int index1 = str.LastIndexOf('（', index2);

                    result.Add(new Player(
                        m.Value.Substring(0, index1),
                        m.Value.Substring(index1 + 1, index2 - index1 - 1),
                        m.Value.Substring(index2 + 1, index3 - index2 - 1)));
                }
            }
            reader.Close();

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Round3Select.IO
{
    public class FileWriter
    {
        public static void WritePlayerList(List<Player> plist, string path)
        {
            StreamWriter streamWriter;

            using (streamWriter = new StreamWriter(path, false))
            {
                foreach (Player p in plist)
                {
                    streamWriter.WriteLine(p.TeamName);
                    streamWriter.WriteLine(string.Format(
                        "{0}\t{1}", p.Player1, p.Player2));
                }
            }
            
        }
    }
}

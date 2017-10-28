using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ARMRBT
{
    public static class Log
    {
        public static void LogWriteLine(string typeaccess, string message)
        {
            using (FileStream fs = new FileStream("Logs\\Log.log", FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(typeaccess+ " ("+DateTime.Now +") "+message);
                sw.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader.data
{
    class TextFileManager
    {

        public TextFileManager()
        {

        }

        /// <summary>
        /// Read data from a source file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetDataFrom(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}

// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 03.01.2019


using System.IO;

namespace SpicyInvader.data
{
    class TextFileManager
    {

        public TextFileManager()
        {

        }

        /// <summary>
        /// Read data from a source file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetDataFrom(string path)
        {
            return File.ReadAllLines(path);
        }

        /// <summary>
        /// Write data in the file at the specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        public static void Write(string path, string data)
        {
            //File.WriteAllText(path, sb.ToString());

            using (StreamWriter writetext = new StreamWriter(path))
            {
                writetext.WriteLine(data);
            }
        }
    }
}

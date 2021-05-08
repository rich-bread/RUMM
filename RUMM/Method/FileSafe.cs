using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RUMM.Method
{
    public static class FileSafe
    {
        public static FileStream SafeCreateFile(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine(path + "already exists.");

                return null;
            }
            return File.Create(path);
        }

        public static void SafeCreateFile_and_Write(string path, string text)
        {
            if (File.Exists(path))
            {
                Console.WriteLine("Could not create and write due to specified file exists.");
            }
            else
            {
                using (var file = File.Create(path))
                {
                    file.Close();
                }
                File.WriteAllText(path, text);

                Console.WriteLine("Written successfully.");
            }
        }
    }
}
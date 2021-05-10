using System.IO;

namespace RUMM.Method
{
    public static class Safe
    {
        public static FileStream SafeCreateFile(string path)
        {
            if (!File.Exists(path))
            {
                return File.Create(path);
            }
            else
                return null;
        }

        public static void SafeCreateFile_and_Write(string path, string text)
        {
            if (!File.Exists(path))
            {
                using (var file = File.Create(path))
                {
                    file.Close();
                }
                File.WriteAllText(path, text);
            }
            else
                return;
        }
    }
}
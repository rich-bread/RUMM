using System;
using System.IO;

namespace RUMM.Method
{
    public class Setup
    {
        public static void CreateSetupFolder(ulong id)
        {
            string serverfolder = $@"R:\Project\RUMM.warehouse\{id}";

            try
            {
                //『サーバー』フォルダー作成
                DirectoryInfo mainfolder = new DirectoryInfo(serverfolder);
                mainfolder.Create();

                //「データ」フォルダー作成
                DirectoryInfo datamain = mainfolder.CreateSubdirectory("Data");                                     //「データ」全体フォルダー
                DirectoryInfo data_recenter = datamain.CreateSubdirectory("Recenter");                              //「データ」中心座標フォルダー
                DirectoryInfo data_trimmode = datamain.CreateSubdirectory("Definemode");                            //「データ」切り取りモードフォルダー
                DirectoryInfo data_area = datamain.CreateSubdirectory("Area");                                      //「データ」エリアフォルダー

                //「アップロード」フォルダー作成
                DirectoryInfo uploadedmain = mainfolder.CreateSubdirectory("Uploaded");                             //「アップロード」全体フォルダー
                DirectoryInfo uploadedmap = uploadedmain.CreateSubdirectory("UploadedMap");                         //「アップロード」地図フォルダー(一時的にアップロードされたものを置いておく場所)

                //「切り取り」フォルダー作成
                DirectoryInfo trimedmain = mainfolder.CreateSubdirectory("Trimed");                                 //「切り取り」全体フォルダー
                DirectoryInfo trimedmap = trimedmain.CreateSubdirectory("TrimedMap");                               //「切り取り」地図フォルダー
                DirectoryInfo trimedmap_pre = trimedmain.CreateSubdirectory("TrimedMap[Pre]");                      //「切り取り」地図フォルダー(一時的に切り取りしたものを置いておく場所)
                DirectoryInfo trimedmap_backup = trimedmain.CreateSubdirectory("TrimedMap[Backup]");                //「切り取り」地図[バックアップ]フォルダー

                //「完了」フォルダー作成
                DirectoryInfo completedmain = mainfolder.CreateSubdirectory("Completed");                           //「完了」全体フォルダー
                DirectoryInfo completedmain_map = completedmain.CreateSubdirectory("CompletedMap");                 //「完了」地図フォルダー
                DirectoryInfo completedmain_map_backup = completedmain.CreateSubdirectory("CompletedMap[Backup]");  //「完了」地図[バックアップ]フォルダー
                DirectoryInfo completedarea_map = completedmain.CreateSubdirectory("AreaMap");                      //「完了」エリア地図フォルダー
            }
            catch (Exception reason)
            {
                Console.WriteLine(reason);
            }

            //データ用テキストファイルの宣言
            string recenter_txt = $@"{serverfolder}\Data\Recenter\recenter.txt";
            string trimmode_txt = $@"{serverfolder}\Data\Definemode\definemode.txt";

            //中心座標変更テキストファイルの作成・書き込み
            string recentercoord = "0\r\n0";
            Safe.CreateFile_and_Write(recenter_txt, recentercoord);

            //切り取りモードテキストファイルの作成・書き込み
            string defaulttrimmode = "0";
            Safe.CreateFile_and_Write(trimmode_txt, defaulttrimmode);
        }

        public static void DeleteSetupFolder(ulong id)
        {
            string serverfolder = $@"R:\Project\RUMM.warehouse\{id}";

            try
            {
                string[] filepaths = Directory.GetFiles(serverfolder);
                foreach (string filepath in filepaths)
                {
                    File.SetAttributes(filepath, FileAttributes.Normal);
                    File.Delete(filepath);
                }

                string[] directorypaths = Directory.GetDirectories(serverfolder);
                foreach (string directorypath in directorypaths)
                {
                    Directory.Delete(directorypath, true);
                }

                Directory.Delete(serverfolder, false);
            }
            catch (Exception reason)
            {
                Console.WriteLine(reason);
            }
        }
    }
}
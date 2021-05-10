using Discord.Commands;
using OpenCvSharp;
using RUMM.Common;
using RUMM.Method;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration.Original
{
    public class update : ModuleBase<SocketCommandContext>
    {
        [Command("update", RunMode = RunMode.Async)]

        public async Task Update()
        {
            //サーバーID等の変数の宣言
            string serverfolder = $@"R:\Project\RUMM.warehouse\{Context.Guild.Id}";

            string datafolder = $@"{serverfolder}\Data";
            string datafolder_recenter = $@"{datafolder}\Recenter";
            string datafolder_trimmode = $@"{datafolder}\Trimmode";

            string uploadedfolder = $@"{serverfolder}\Uploaded";
            string uploadedfolder_map = $@"{uploadedfolder}\UploadedMap";

            string trimedfolder = $@"{serverfolder}\Trimed";
            string trimedfolder_map = $@"{trimedfolder}\TrimedMap";
            string trimedfolder_map_pre = $@"{trimedfolder}\TrimedMap[Pre]";
            string trimedfolder_map_backup = $@"{trimedfolder}\TrimedMap[Backup]";

            //データ用テキストファイルの指定
            string recenter_txt = $@"{datafolder_recenter}\recenter.txt";
            string trimmode_txt = $@"{datafolder_trimmode}\trimmode.txt";

            //メッセージに画像が添付されているかどうかを判断
            if (!Context.Message.Attachments.Any())
            {
                await Context.Channel.SendErrorAsync("エラー", "画像が添付されてないよ！必ずコマンドと併せて画像を送信してね！");
                return;
            }

            //Discordに送信されたメッセージとそのメッセージに付いているファイルを取得
            var attachments = Context.Message.Attachments;

            //新しいWebClientのインスタンスを作成
            WebClient myWebClient = new WebClient();

            //保存先とURLの指定
            string uploadedmap = $@"{uploadedfolder_map}\uploadedmap.png";
            string url = attachments.ElementAt(0).Url;

            //ファイルをダウンロード
            myWebClient.DownloadFile(url, uploadedmap);

            string trimedmap_pre = $@"{trimedfolder_map_pre}\trimedmap[pre].png";

            Call.Device(uploadedmap, trimedmap_pre);

            Graphic.Resize_Own(trimedmap_pre, 384);

            var comparemap = Directory.EnumerateFiles(trimedfolder_map, "*", SearchOption.AllDirectories);

            float ImageMatch(Mat mat1, Mat mat2, bool show)
            {
                using (var descriptors1 = new Mat())
                using (var descriptors2 = new Mat())
                {
                    // 特徴点を検出
                    var akaze = AKAZE.Create();

                    // キーポイントを検出
                    akaze.DetectAndCompute(mat1, null, out KeyPoint[] keyPoints1, descriptors1);
                    akaze.DetectAndCompute(mat2, null, out KeyPoint[] keyPoints2, descriptors2);

                    // それぞれの特徴量をマッチング
                    var matcher = new BFMatcher(NormTypes.Hamming, false);
                    var matches = matcher.Match(descriptors1, descriptors2);

                    // 平均距離を返却(小さい方が類似度が高い)
                    var sum = matches.Sum(x => x.Distance);
                    return sum / matches.Length;
                }
            }

            foreach (string comparemapnum in comparemap)
            {
                string mapxcoord = comparemapnum.Split(',')[0].Replace(trimedfolder_map + "\\", "");
                string mapzcoord = comparemapnum.Split(',')[1].Replace(".png", "");

                string trimedmap = $@"{trimedfolder_map}\{mapxcoord},{mapzcoord}.png";
                string trimedmap_await = $@"{trimedfolder_map_pre}\{mapxcoord},{mapzcoord}[await].png";

                using (var mat1 = new Mat(trimedmap_pre))
                using (var mat2 = new Mat(trimedmap))
                {
                    // 2つの画像を比較(平均距離をスコアとした)
                    float score = ImageMatch(mat1, mat2, true);

                    Console.WriteLine(score);

                    if (score < 75)
                    {
                        File.Copy(trimedmap_pre, trimedmap_await, true);
                    }
                }
            }

            List<string> coordslist = new List<string>();

            string searchfileword = @"*await*.png";
            string[] comparemap2 = Directory.GetFiles(trimedfolder_map_pre, searchfileword);

            foreach (string mapnum in comparemap2)
            {
                coordslist.Add(mapnum);
            }

            string[] filelist = Directory.GetFiles(trimedfolder_map_pre, searchfileword);

            if (coordslist.Count() == 1)
            {
                foreach (string premapnum in filelist)
                {
                    string mapxcoord = premapnum.Split(',')[0].Replace(trimedfolder_map_pre + "\\", "");
                    string mapzcoord = premapnum.Split(',')[1].Replace("[await].png", "");

                    string trimedmap = $@"{trimedfolder_map}\{mapxcoord},{mapzcoord}.png";

                    string trimedfolder_map_backup_foreach = $@"{trimedfolder_map_backup}\{mapxcoord},{mapzcoord}";
                    string trimedmap_backup = $@"{trimedfolder_map_backup_foreach}\{DateTime.Now.ToString("yyyyMMdd")}.png";

                    File.Copy(premapnum, trimedmap, true);
                    File.Copy(premapnum, trimedmap_backup, true);

                    File.Delete(premapnum);
                }

                await Context.Channel.SendSuccessAsync("完了", "正常に画像を切り取ったよ！");
            }
            else if (coordslist.Count() > 1 || coordslist.Count() == 0)
            {
                foreach (string premapnum in filelist)
                    File.Delete(premapnum);

                await Context.Channel.SendErrorAsync("エラー", "定義されている地図と類似度が高くないよ！");
            }
        }
    }
}
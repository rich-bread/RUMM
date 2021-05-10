using Discord.Addons.Interactive;
using Discord.Commands;
using RUMM.Common;
using RUMM.Method;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration.Original
{
    public class define : InteractiveBase<SocketCommandContext>
    {
        [Command("define", RunMode = RunMode.Async)]

        public async Task Define(string x, string z)
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
            string trimedfolder_map_x128 = $@"{trimedfolder}\TrimedMap[x128]";
            string trimedfolder_map_x256 = $@"{trimedfolder}\TrimedMap[x256]";

            //データ用テキストファイルの指定
            string recenter_txt = $@"{datafolder_recenter}\recenter.txt";
            string trimmode_txt = $@"{datafolder_trimmode}\trimmode.txt";

            string definedmap = $@"{trimedfolder_map}\{x},{z}.png";

            //メッセージに画像が添付されているかどうかを判断
            if (!Context.Message.Attachments.Any())
            {
                await Context.Channel.SendErrorAsync("エラー", "画像が添付されてないよ！必ずコマンドと併せて画像を送信してね！");
                return;
            }

            //既に定義されている地図がある場合、再定義をしてもよいかどうかの確認を取る
            if (File.Exists(definedmap))
            {
                var msg_definedmap = await Context.Channel.SendFileAsync(definedmap);
                var msg_verification = await Context.Channel.SendVerifyAsync("確認", "既に定義された地図が存在するよ！本当に再定義しても大丈夫？" +
                                                                                     "\r\n`はい`か`いいえ`で答えてね！");

                var answer = await NextMessageAsync();

                if (answer != null)
                {
                    switch (answer.Content)
                    {
                        case "はい":
                            break;

                        case "いいえ":
                            await msg_definedmap.DeleteAsync();
                            await msg_verification.DeleteAsync();

                            var msg_answerToNO = await Context.Channel.SendErrorAsync("エラー", "もう一度定義したい場合は同じコマンドを打ってね！");

                            await Task.Delay(10000);
                            await msg_answerToNO.DeleteAsync();

                            return;

                        default:
                            await msg_definedmap.DeleteAsync();
                            await msg_verification.DeleteAsync();

                            var msg_answerToElse = await Context.Channel.SendErrorAsync("エラー", "`はい`か`いいえ`で答えてね！");

                            await Task.Delay(10000);
                            await msg_answerToElse.DeleteAsync();

                            return;
                    }
                }
                else
                {
                    var timeout_msg = await Context.Channel.SendErrorAsync("エラー", "タイムアウトする前に答えを書いてね！");

                    await Task.Delay(10000);
                    await timeout_msg.DeleteAsync();
                    return;
                }
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

            string x_numbering, z_numbering;
            //
            string trimmode = File.ReadLines(trimmode_txt).First();

            if (trimmode == "0")
            {
                string read_x = File.ReadLines(recenter_txt).First();
                string read_z = File.ReadLines(recenter_txt).Skip(1).First();

                Trimmode.Coordinate(read_x, read_z, x, z);

                x_numbering = Trimmode.numbered_x;
                z_numbering = Trimmode.numbered_z;
            }
            else
            {
                x_numbering = x;
                z_numbering = z;
            }

            string trimedmap = $@"{trimedfolder_map}\{x_numbering},{z_numbering}.png";

            string trimedmap_x128 = $@"{trimedfolder_map_x128}\{x_numbering},{z_numbering}.png";
            string trimedmap_x256 = $@"{trimedfolder_map_x256}\{x_numbering},{z_numbering}.png";

            Call.Device(uploadedmap, trimedmap);

            Graphic.Resize_Own(trimedmap, 384);
            Graphic.Resize_Copy(trimedmap, trimedmap_x128, 128);
            Graphic.Resize_Copy(trimedmap, trimedmap_x256, 256);

            await Context.Channel.SendSuccessAsync("完了", "アップロードされた画像を指定された位置の地図として定義したよ！");
        }
    }
}
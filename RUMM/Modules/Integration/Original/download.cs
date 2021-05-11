using Discord.Commands;
using RUMM.Common;
using RUMM.Method;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration.Original
{
    public class download : ModuleBase<SocketCommandContext>
    {
        [Command("download", RunMode = RunMode.Async)]

        public async Task Download(int scale)
        {
            //サーバーID等の変数の宣言
            string serverfolder = $@"R:\Project\RUMM.warehouse\{Context.Guild.Id}";

            string trimedfolder = $@"{serverfolder}\Trimed";
            string trimedfolder_map = $@"{trimedfolder}\TrimedMap";

            string completedfolder = $@"{serverfolder}\Completed";
            string completedfolder_map = $@"{completedfolder}\CompletedMap";
            string completedfolder_map_backup = $@"{completedfolder}\CompletedMap[Backup]";

            string compmap_path = $@"{completedfolder_map}\completedmap.png";
            string compmap_path_withdate = $@"{completedfolder_map}\completedmap[{DateTime.Now.ToString("yyyyMMdd")}].png";
            string compmap_backup_path = $@"{completedfolder_map_backup}\{DateTime.Now.ToString("yyyyMMdd")}.png";

            //特定のサーバーからこのコマンドが実行されないようにする
            if (Context.Guild.Id == 725704901652381718)
            {
                await Context.Channel.SendErrorAsync("エラー", $"このコマンドは`{Context.Guild.Name}`では使えないよ！");
                return;
            }

            //指定できる最大サイズを超えた指定をされた場合、拒否する
            if (scale > 384)
            {
                await Context.Channel.SendErrorAsync("エラー", "指定できるサイズは`384`以下だよ！もう実行試してみてね！");
                return;
            }

            //指定されたサイズで地図を統合 / 同時に、バックアップ用の統合地図を最大サイズで作る
            Integrate.Original(trimedfolder_map, compmap_path_withdate, scale);
            Integrate.Original(trimedfolder_map, compmap_backup_path, 384);

            //Discordで送信できる最大サイズである[8MB]を超えた場合、送信を拒否する
            FileInfo file = new FileInfo(compmap_path_withdate);
            if (file.Length < 8000000)
            {
                await Context.Channel.SendFileAsync(compmap_path_withdate);
                await Context.Channel.SendSuccessAsync("完了", "これが君たちの世界の地図だよ！");
            }
            else
            {
                await Context.Channel.SendErrorAsync("エラー", $"地図が8MBを超えていて送れないんだ...、今の地図の大きさは`{file.Length / 1048576}MB`だよ！" +
                                                               $"\r\n別のサイズで試してみてね！(※統合はできているよ！)");
            }

            File.Move(compmap_path_withdate, compmap_path, true);
        }
    }
}
using Discord.Commands;
using RUMM.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration.Original
{
    public class definemode : ModuleBase<SocketCommandContext>
    {
        [Command ("definemode")]

        public async Task DefineMode(string mode)
        {
            //サーバーID等の変数の宣言
            string serverfolder = $@"R:\Project\RUMM.warehouse\{Context.Guild.Id}";

            string datafolder = $@"{serverfolder}\Data";
            string datafolder_definemode = $@"{datafolder}\Definemode";
            string definemode_txt = $@"{datafolder_definemode}\definemode.txt";

            string definemode, currentmode;

            switch (mode)
            {
                case "0":
                case "座標":
                    definemode = "0";
                    currentmode = "座標";
                    break;

                case "1":
                case "ナンバリング":
                    definemode = "1";
                    currentmode = "ナンバリング";
                    break;

                default:
                    definemode = null;
                    currentmode = null;
                    await Context.Channel.SendErrorAsync("エラー", "入力されたモードは対応していないよ！`座標`か`ナンバリング`のどちらかを選択してね！");
                    return;
            }

            if (File.Exists(definemode_txt))
            {
                File.WriteAllText(definemode_txt, definemode);

                await Context.Channel.SendSuccessAsync("完了", $"定義モードを`{currentmode}`モードに変更したよ！");
            }
            else
            {
                await Context.Channel.SendErrorAsync("エラー", "まだセットアップが完了していないね..、`r.setup`で設定してみよ！");
            }
        }
    }
}

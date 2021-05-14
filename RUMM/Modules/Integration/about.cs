using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration
{
    public class about : ModuleBase<SocketCommandContext>
    {
        [Command("about")]

        public async Task About(string command)
        {
            ulong serverid = Context.Guild.Id;

            string name, description;

            var builder = new EmbedBuilder();
            if (command == "setup")
            {
                name = "setup│サーバー用の地図設定";
                description = "私がアップデートされてバージョンが変更されたら、このコマンドを打ってね！";

                builder.WithAuthor(author =>
                {
                    author
                .WithName(name);
                })
                .WithDescription(description)
                .WithColor(new Color(22, 194, 242))

                .AddField("使用例", "`r.setup`");
            }
            if (command == "recenter")
            {
                name = "recenter│中心座標の変更";
                description = "統合地図の中心座標を移動させて、別の地図を真ん中に置きたい！と思ったら使ってみてね！" +
                              "\r\n※切り取りモードが【座標】のサーバーのみで適用できるよ！";

                builder.WithAuthor(author =>
                {
                    author
                .WithName(name);
                })
                .WithDescription(description)
                .WithColor(new Discord.Color(22, 194, 242))

                .AddField("使用例", "`r.recenter {x} {z}`");
            }
            if (command == "trimmode")
            {
                name = "trimmode│切り取りモードの変更";
                description = "切り取りモードを変更するときに使うコマンドだよ！" +
                              "\r\n※このコマンドはまだ整備中なんだ💦まだ使えないよ！";

                builder.WithAuthor(author =>
                {
                    author
                .WithName(name);
                })
                .WithDescription(description)
                .WithColor(new Discord.Color(22, 194, 242))

                .AddField("使用例", "`r.trimmode {modenumber}`");
            }
            if (command == "define")
            {
                name = "define│地図の定義";
                description = "地図を新しく定義するときに使えるコマンドだよ！" +
                              "\r\n類似度が高くなくて地図の更新ができないときもこれを使ってね！";

                builder.WithAuthor(author =>
                {
                    author
                .WithName(name);
                })
                .WithDescription(description)
                .WithColor(new Discord.Color(22, 194, 242))

                .AddField("使用例", "`r.define {x} {z}`");
            }
            if (command == "update")
            {
                name = "update│地図の更新";
                description = "既に定義されている地図を更新する際に使えるコマンドだよ！";

                builder.WithAuthor(author =>
                {
                    author
                .WithName(name);
                })
                .WithDescription(description)
                .WithColor(new Discord.Color(22, 194, 242))

                .AddField("使用例", "`r.update`");
            }
            if (command == "download" || command == "rdownload")
            {
                description = "定義した地図を統合して、完成した地図をチャンネルに送るよ！";

                if (serverid == 1522532374 || serverid == 1006895134)
                {
                    name = "rdownload│地図の統合";
                }
                else
                {
                    name = "download│地図の統合";
                }

                builder.WithAuthor(author =>
                {
                    author
                .WithName(name);
                })
                .WithDescription(description)
                .WithColor(new Discord.Color(22, 194, 242))
                .AddField("使用例", "`r." + command + " {size}`");
            }
            if (command == "downloadeach")
            {
                name = "downloadeach│最小地図のダウンロード";
                description = "既に定義されている地図を確認するときや、ダウンロードしたいときに使ってね！";

                builder.WithAuthor(author =>
                {
                    author
                .WithName(name);
                })
                .WithDescription(description)
                .WithColor(new Discord.Color(22, 194, 242))

                .AddField("使用例", "`r.downloadeach {x} {z}`");
            }
            if (command == "deleteeach")
            {
                name = "deleteeach│最小地図の削除";
                description = "地図を間違えた場所に定義してしまったり、間違えたものを定義してしまった場合に使ってね！";

                builder.WithAuthor(author =>
                {
                    author
                .WithName(name);
                })
                .WithDescription(description)
                .WithColor(new Discord.Color(22, 194, 242))

                .AddField("使用例", "`r.deleteeach {x} {z}`");
            }
            if (command == "area" || command == "rarea")
            {
                description = "エリア(地域)の登録や削除、ダウンロードとかエリア関連で使えるコマンドだよ！詳細は↓を確認してね！";

                if (serverid == 1522532374 || serverid == 1006895134)
                {
                    name = "rarea│エリア(地域)関連コマンド";
                }
                else
                {
                    name = "area│エリア(地域)関連コマンド";
                }

                builder.WithAuthor(author =>
                {
                    author
                .WithName(name);
                })
                .WithDescription(description)
                .WithColor(new Discord.Color(22, 194, 242))
                .AddField("エリアの登録・削除等", "◦エリアの登録 `r." + command + " add {areaname}`" +
                                                  "\r\n◦エリアの削除 `r." + command + " delete {areaname}`" +
                                                  "\r\n◦エリアのダウンロード `r." + command + " download {areaname}`")
                .AddField("エリアに登録されている地図の確認", "◦座標一覧表 `r." + command + " {areaname} list text`" +
                                                              "\r\n◦座標付きエリア地図 `r." + command + " {areaname} list map`")
                .AddField("地図のエリア登録・削除等", "◦エリアへの地図登録 `r." + command + " {areaname} add {x} {z}`" +
                                                      "\r\n◦エリアから地図削除 `r." + command + " {areaname} delete {x} {z}`");
            }

            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}

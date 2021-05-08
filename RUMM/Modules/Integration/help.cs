using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration
{
    public class help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]

        public async Task Help()
        {
            ulong oserverid = (Context.Guild as SocketGuild).Id;
            int serverid = (int)oserverid;

            string define, download, area;

            if (serverid == 1522532374 || serverid == 1006895134)
            {
                define = "define";
                download = "rdownload";
                area = "rarea";
            }
            else
            {
                define = "define";
                download = "download";
                area = "area";
            }

            string name = "ヘルプ";
            string description = "RUMMを動かす為のプレフィックスは全て `r.` だよ！" +
                                 "\r\nコマンドの詳細は、`r.about` の後に対応しているコマンド名を入力すると見れるよ！";

            var builder = new EmbedBuilder()
                .WithAuthor(author =>
                {
                    author
                    .WithName(name);
                })
                .WithDescription(description)
                .WithColor(new Discord.Color(22, 194, 242))
                .AddField("設定", "`setup`,`recenter`,`trimmode`")
                .AddField("地図作成支援", $"`{define}`,`update`,`{download}`,`downloadeach`,`deleteeach`,`{area}`", true);

            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

    }
}
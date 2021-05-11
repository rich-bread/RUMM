using Discord.Addons.Interactive;
using Discord.Commands;
using RUMM.Common;
using System.IO;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration.Original
{
    public class deleteeach : InteractiveBase<SocketCommandContext>
    {
        [Command("deleteeach", RunMode = RunMode.Async)]

        public async Task DeleteEach(string x, string z)
        {
            //サーバーID等の変数の宣言
            string serverfolder = $@"R:\Project\RUMM.warehouse\{Context.Guild.Id}";
            string trimedfolder = $@"{serverfolder}\Trimed";
            string trimed_map = $@"{trimedfolder}\TrimedMap\{x},{z}.png";

            var msg_trimedmap = await Context.Channel.SendFileAsync(trimed_map);
            var msg_verification = await Context.Channel.SendVerifyAsync("確認", "本当にこのエリアの情報を削除しても大丈夫？`はい`か`いいえ`で答えてね！");
            var answer = await NextMessageAsync();

            switch (answer.Content)
            {
                case "はい":
                    break;
                case "いいえ":
                    await msg_trimedmap.DeleteAsync();
                    await Context.Channel.DeleteMessageAsync(msg_verification);
                    await Context.Channel.SendErrorAsync("エラー", "削除をやめたよ！本当に消したいときは`はい`を答えてね！");
                    return;
                default:
                    await msg_trimedmap.DeleteAsync();
                    await Context.Channel.DeleteMessageAsync(msg_verification);
                    await Context.Channel.SendErrorAsync("エラー", "`はい`か`いいえ`で答えてね！");
                    return;
            }

            File.Delete(trimed_map);

            await Context.Channel.SendSuccessAsync("完了", $"指定された地図「{x},{z}」を削除したよ！");
        }
    }
}
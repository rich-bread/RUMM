using Discord.Addons.Interactive;
using Discord.Commands;
using RUMM.Common;
using RUMM.Method;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUMM.Modules.Integration.Original
{
    public class area : InteractiveBase<SocketCommandContext>
    {
        [Command("area")]

        public async Task AreaList(string option)
        {
            switch (option)
            {
                case "list":
                    
                    break;
            }
        }

        [Command ("area", RunMode = RunMode.Async)]

        public async Task AreaOption(string option, string areaname)
        {
            //サーバーID等の変数の宣言
            string serverfolder = $@"R:\Project\RUMM.warehouse\{Context.Guild.Id}";

            string datafolder = $@"{serverfolder}\Data";
            string datafolder_area = $@"{datafolder}\Area";
            string area_datafolder = $@"{datafolder_area}\{areaname}";
            string area_text = $@"{area_datafolder}\{areaname}.txt";

            string trimedfolder = $@"{serverfolder}\Trimed";
            string trimedfolder_map = $@"{trimedfolder}\TrimedMap";

            string completedfolder = $@"{serverfolder}\Completed";
            string areafolder = $@"{completedfolder}\AreaMap\{areaname}";
            string areafolder_backup = $@"{completedfolder}\AreaMap\{areaname}\{areaname}[Backup]";
            string area_completedmap = $@"{areafolder}\{areaname}.png";
            string area_completedmap_backup = $@"{areafolder_backup}\{DateTime.Now.ToString("yyyyMMdd")}.png";

            switch (option)
            {
                case "add":
                    if (!Directory.Exists(area_datafolder))
                    {
                        Safe.CreateDirectory(area_datafolder);
                        Safe.CreateDirectory(areafolder);
                        Safe.CreateDirectory(areafolder_backup);

                        using (var file = File.Create(area_text))
                            file.Close();

                        await Context.Channel.SendSuccessAsync("完了", "エリアを追加したよ！");
                    }
                    else
                        await Context.Channel.SendErrorAsync("エラー", "もうすでにこのエリアは追加されているよ！");
                    break;

                case "delete":
                    if (File.Exists(area_text))
                    {
                        var verification = await Context.Channel.SendVerifyAsync("確認", "本当にこのエリアの情報を削除しても大丈夫？`はい`か`いいえ`で答えてね！");
                        var answer = await NextMessageAsync();

                        switch (answer.Content)
                        {
                            case "はい":
                                await Context.Channel.DeleteMessageAsync(verification);
                                break;
                            case "いいえ":
                                await Context.Channel.DeleteMessageAsync(verification);
                                await Context.Channel.SendErrorAsync("エラー", "削除をやめたよ！本当に消したいときは`はい`を答えてね！");
                                return;
                            default:
                                await Context.Channel.DeleteMessageAsync(verification);
                                await Context.Channel.SendErrorAsync("エラー", "`はい`か`いいえ`で答えてね！");
                                return;
                        }

                        try
                        {
                            File.Delete(area_completedmap);
                            Directory.Delete(areafolder_backup);
                            Directory.Delete(areafolder);
                            File.Delete(area_text);
                        }
                        catch (Exception)
                        {
                        }

                        await Context.Channel.SendSuccessAsync("完了", $"{areaname}の情報を削除したよ！");
                    }
                    else
                        await Context.Channel.SendErrorAsync("エラー", $"そんな名前のエリアはないよ...? \r\n `r.area add {areaname}`でエリアを追加してみよう！");
                    break;

                case "download":
                    //特定のサーバーからこのコマンドが実行されないようにする
                    if (Context.Guild.Id == 725704901652381718)
                    {
                        await Context.Channel.SendErrorAsync("エラー", $"このコマンドは`{Context.Guild.Name}`では使えないよ！");
                        return;
                    }

                    if (File.Exists(area_text))
                    {
                        Integrate.Original_Area(area_text, trimedfolder_map, area_completedmap);
                        Integrate.Original_Area(area_text, trimedfolder_map, area_completedmap_backup);

                        await Context.Channel.SendFileAsync(area_completedmap);
                        await Context.Channel.SendSuccessAsync("完了", $"これが{areaname}の地図だよ！");
                    }
                    else
                        await Context.Channel.SendErrorAsync("エラー", $"そんな名前のエリアはないよ...? \r\n `r.area add {areaname}`でエリアを追加してみよ！");
                    break;

                //Realm3rd用引数
                case "rdownload":
                    if (File.Exists(area_text))
                    {
                        Integrate.Designed_Realm3rd_Area(area_text, trimedfolder_map, area_completedmap);
                        Integrate.Designed_Realm3rd_Area(area_text, trimedfolder_map, area_completedmap_backup);

                        await Context.Channel.SendFileAsync(area_completedmap);
                        await Context.Channel.SendSuccessAsync("完了", $"これが{areaname}の地図だよ！");
                    }
                    else
                        await Context.Channel.SendErrorAsync("エラー", $"そんな名前のエリアはないよ...? \r\n `r.area add {areaname}`でエリアを追加してみよ！");
                    break;

                default:
                    await Context.Channel.SendErrorAsync("エラー", "引数が間違ってるよ！`r.about area`で詳しい使い方を見てね！");
                    return;
            }
        }

        [Command("area")]

        public async Task AreaListOption(string areaname, string option, string choice)
        {
            //サーバーID等の変数の宣言
            string serverfolder = $@"R:\Project\RUMM.warehouse\{Context.Guild.Id}";

            string datafolder = $@"{serverfolder}\Data";
            string datafolder_area = $@"{datafolder}\Area";
            string datafolder_areaname = $@"{datafolder_area}\{areaname}";
            string area_text = $@"{datafolder_areaname}\{areaname}.txt";

            string trimedfolder = $@"{serverfolder}\Trimed";
            string trimedfolder_map = $@"{trimedfolder}\TrimedMap";

            string completedfolder = $@"{serverfolder}\Completed";
            string areafolder = $@"{completedfolder}\AreaMap\{areaname}";
            string area_completedmap_list = $@"{areafolder}\{areaname}[list].png";

            switch (option)
            {
                case "list":
                    switch (choice)
                    {
                        case "text":
                            if (File.Exists(area_text))
                            {
                                await Context.Channel.SendFileAsync(area_text);
                                await Context.Channel.SendSuccessAsync("完了", $"{areaname}の座標リストだよ！");
                            }
                            else
                                await Context.Channel.SendErrorAsync("エラー", $"そんな名前のエリアはないよ...? \r\n `r.area add {areaname}`でエリアを追加してみよ！");
                            break;

                        case "map":
                            if (File.Exists(area_text))
                            {
                                Integrate.Original_Area_WithCoord(area_text, trimedfolder_map, area_completedmap_list);

                                await Context.Channel.SendFileAsync(area_completedmap_list);
                                await Context.Channel.SendSuccessAsync("完了", $"これが{areaname}の座標付き地図だよ！");
                            }
                            else
                                await Context.Channel.SendErrorAsync("エラー", $"そんな名前のエリアはないよ...? \r\n `r.area add {areaname}`でエリアを追加してみよう！");
                            break;
                    }
                    break;

                case "copy":
                    break;
            }
        }

        [Command ("area")]

        public async Task AreanameOption(string areaname, string option, int x, int z)
        {
            //サーバーID等の変数の宣言
            string serverfolder = $@"R:\Project\RUMM.warehouse\{Context.Guild.Id}";

            string datafolder = $@"{serverfolder}\Data";
            string datafolder_area = $@"{datafolder}\Area";
            string datafolder_areaname = $@"{datafolder_area}\{areaname}";
            string area_text = $@"{datafolder_areaname}\{areaname}.txt";

            switch (option)
            {
                case "add":
                    FileInfo fileInfo = new FileInfo(area_text);
                    string areamap_coord;

                    if (fileInfo.Length == 0)
                        areamap_coord = $"{x},{z}";
                    else
                        areamap_coord = $"\r\n{x},{z}";

                    File.AppendAllText(area_text, areamap_coord);

                    var lines_addmode = File.ReadAllLines(area_text).Where(arg => !string.IsNullOrWhiteSpace(arg));
                    File.WriteAllLines(area_text, lines_addmode);

                    await Context.Channel.SendSuccessAsync("完了", $"指定された地図をエリア「{areaname}」に追加したよ！");
                    break;

                case "delete":
                    string before = $"{x},{z}";
                    string after = "";

                    List<int> oldtextline = new List<int>();
                    List<int> newtextline = new List<int>();

                    string[] preoldlinenum = File.ReadAllLines(area_text);
                    int oldlinenum = preoldlinenum.Length;
                    oldtextline.Add(oldlinenum);

                    StringBuilder strread = new StringBuilder();
                    string[] strarray = File.ReadAllLines(area_text);
                    for (int i = 0; i < strarray.GetLength(0); i++)
                    {
                        if (strarray[i].Contains(before) == true)
                        {
                            strread.AppendLine(strarray[i].Replace(before, after));

                            await Context.Channel.SendSuccessAsync("完了", $"指定された地図をエリア「{areaname}」から削除したよ！");
                        }
                        else
                        {
                            strread.AppendLine(strarray[i]);
                        }

                        File.WriteAllText(area_text, strread.ToString());

                        var lines_deletemode = File.ReadAllLines(area_text).Where(arg => !string.IsNullOrWhiteSpace(arg));
                        File.WriteAllLines(area_text, lines_deletemode);
                    }

                    string[] prenewlinenum = File.ReadAllLines(area_text);
                    int newlinenum = prenewlinenum.Length;
                    newtextline.Add(newlinenum);

                    int foldlinenum = oldtextline.Max();
                    int fnewlinenum = newtextline.Max();

                    if (foldlinenum == fnewlinenum)
                    {
                        await Context.Channel.SendErrorAsync("エラー", $"指定された地図はエリア「{areaname}」に追加されてないよ？");
                    }
                    break;

                default:
                    await Context.Channel.SendErrorAsync("エラー", "引数が間違ってるよ！`r.about area`で詳しい使い方を見てね！");
                    return;
            }
        }
    }
}
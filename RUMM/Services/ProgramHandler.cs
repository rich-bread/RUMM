using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using RUMM.Common;
using RUMM.Method;

namespace RUMM.Services
{
    public class ProgramHandler : InitializedService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _config;

        public ProgramHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration config)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;
        }

        public override async Task InitializeAsync(CancellationToken cancellationToken)
        {
            _client.Ready += OnReady;
            _client.JoinedGuild += OnJoinedGuild;
            _client.LeftGuild += OnLeftGuild;
            _client.MessageReceived += OnMessageReceived;

            _service.CommandExecuted += OnCommandExecuted;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task OnJoinedGuild(SocketGuild arg)
        {   
            Setup.CreateSetupFolder(arg.Id);

            await _client.SetStatusAsync(UserStatus.Online);

            await _client.GetGuild(arg.Id).GetUser(_client.CurrentUser.Id).ModifyAsync(x =>
            {
                x.Nickname = "[r.] 要らむ";
            });
        }

        private async Task OnLeftGuild(SocketGuild arg)
        {
            Setup.DeleteSetupFolder(arg.Id);

            await _client.SetStatusAsync(UserStatus.Online);
        }

        private async Task OnReady()
        {
            await _client.SetGameAsync("v0.2.0", null, ActivityType.Playing);
        }

        private async Task OnMessageReceived(SocketMessage arg)
        {
            if (!(arg is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            var argPos = 0;
            if (!message.HasStringPrefix(_config["prefix"], ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;

            var context = new SocketCommandContext(_client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (command.IsSpecified && !result.IsSuccess) await (context.Channel as ISocketMessageChannel).SendErrorAsync("エラー", $"以下のエラーが起きちゃった..:\r{result}");
        }
    }
}
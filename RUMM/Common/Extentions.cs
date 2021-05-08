using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace RUMM.Common
{
    public static class Extentions
    {
        public static async Task<IMessage> SendSuccessAsync(this ISocketMessageChannel channel, string title, string description)
        {
            var embed = new EmbedBuilder()
                .WithColor(new Color(22, 194, 242))
                .WithDescription(description)
                .WithAuthor(author =>
                {
                    author
                    .WithIconUrl("https://www.suunto.com/contentassets/0b698f770c5e423ea2336ce649e0cabe/icon-success.png")
                    .WithName(title);
                })
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);
            return message;
        }

        public static async Task<IMessage> SendErrorAsync(this ISocketMessageChannel channel, string title, string description)
        {
            var embed = new EmbedBuilder()
                .WithColor(new Color(22, 194, 242))
                .WithDescription(description)
                .WithAuthor(author =>
                {
                    author
                    .WithIconUrl("http://www.clker.com/cliparts/T/4/V/L/G/Z/red-error-icon.svg.med.png")
                    .WithName(title);
                })
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);
            return message;
        }

        public static async Task<IMessage> SendAcceptAsync(this ISocketMessageChannel channel, string title, string description)
        {
            var embed = new EmbedBuilder()
                .WithColor(new Color(22, 194, 242))
                .WithDescription(description)
                .WithAuthor(author =>
                {
                    author
                    .WithIconUrl("https://lh3.googleusercontent.com/proxy/szHyiGXcbUMlghFTSoGmKAlSkA4LKYrx3ebk8zso5wcNi0YFWyOuARfksT95Utav3oeGsh2nfKuY9GlafJBfIxo")
                    .WithName(title);
                })
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);
            return message;
        }

        public static async Task<IMessage> SendVerifyAsync(this ISocketMessageChannel channel, string title, string description)
        {
            var embed = new EmbedBuilder()
                .WithColor(new Color(22, 194, 242))
                .WithDescription(description)
                .WithAuthor(author =>
                {
                    author
                    .WithIconUrl("https://www.fpsa.org/wp-content/uploads/icons8-ok-528.png")
                    .WithName(title);
                })
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);
            return message;
        }
    }
}

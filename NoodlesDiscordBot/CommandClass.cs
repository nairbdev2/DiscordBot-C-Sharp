using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Discord Libraries
using Discord;
using Discord.Commands;
using NoodlesDiscordBot;

namespace NoodlesDiscordBot
{
    public class CommandClass : ModuleBase
    {
        //Global 
        int delayTime = 8000; //Delay time in MS


        //Help
        [Command("help", RunMode = RunMode.Sync), Summary("Sends msg to server.")]
        private async Task helpAsync([Remainder] [Summary("Text Printed")] string def = "none")
        {

            string[] helpText =
            {
                "help             - Shows Commands\n" +
                "rules            - Rules of the Server\n" +
                "meme             - Shows Random memes\n" +
                "noodles          - Noodle Quotes\n" +
                "8ball            - Random Responses\n" +
                "se [sound]        - Play a sound (UNIMPLEMENTED) \n" +
                "plist            - Lists out all sounds in Noodle Bot (UNIMPLEMENTED) \n"
            };

            await Task.Delay(250);
            string botMsg = $":notepad_spiral: Command List :notepad_spiral: \n```{helpText[0]}```";
            await purgePrevMsg(botMsg, delayTime * 3);

        }

        //Rules of Server 
        [Command("rules", RunMode = RunMode.Sync), Summary("Sends msg to server.")]
        private async Task rulesAsync([Remainder] [Summary("Txt printed")] string def = "none")
        {
            string[] rulesText =
            {
                "1. Be Respectful to everyone and all Admins\n\n" +
                "2. Don't post spam, malicious or pirated content. This includes Instructions to obtain or utilize such content as well.\n\n" +
                "3. Don't flood channels. This includes posting excecessively to the text channels as well as drowning the voice channels with annoying sounds.\n\n" +
                "4. Partcipation is a privilege, not an entitlement. If your presence is deemed disruptive by moderation, you may be banned.\n\n" +
                "5. Don't antagonize other users. This includes perpetuating disruptive conversation which may include racial, sexual, political, or otherwise provocative topics unrelated to gaming.\n\n" +
                "6. No Advertisng (Unless given permission by Admins)" +
                "7. If a user is being annoying or abusive, simply right-click on their name and use Discord's \"Block\" feature. \n\n" +
                "Well That's about it for now so go ahead and Relax and Enjoy yourselves!"
            };

            await Task.Delay(250);
            string botMsg = $":warning: The Rules of the Server :warning: \n ```{rulesText[0]}```";
            await purgePrevMsg(botMsg, delayTime * 4);
        }

        //8ball
        [Command("8ball", RunMode = RunMode.Sync), Summary("Sends msg to server")]
        private async Task randoBall([Remainder] [Summary("text printed")] string def = "none")
        {                                                       //C# list = Java ArrayList
            //List<String> msg = new List<String>(5);  //.Add = .add       .Insert = .set       .ElementAt = .get
            string[] quote = { "It is certain.",
                               "It is decidedly so.",
                               "Without a doubt.",
                               "Yes definitely.",
                               "You may rely on it.",
                               "As I see it, yes.",
                               "Most likely.",
                               "Outlook good.",
                               "Yes.",
                               "Signs point to yes.",
                               "Reply hazy try again.",
                               "Ask again later.",
                               "Better not tell you now.",
                               "Cannot predict now.",
                               "Concentrate and ask again.",
                               "Don't Count on it.",
                               "My reply is no.",
                               "My sources say no.",
                               "Outlook not so good.",
                               "Very doubtful."
                                };

            int val = new Random().Next(0, quote.Length);
            await Task.Delay(250);
            //string botMsg = $"Sorry, the functionality is currently disabled.";
            string botMsg = $"{quote[val]}";

            await Context.Channel.SendMessageAsync(botMsg);
            //await purgePrevMsg(botMsg, delayTime * 3);
        }

        //Noodles Command
        [Command("noodles", RunMode = RunMode.Sync), Summary("Sends msg to server")]
        private async Task noodlesC([Remainder] [Summary("Txt printed")] string def = "none")
        {

            var builder = new EmbedBuilder();

            string[] noodzMsg = { $"Hey it's your Noodle doctor here. Remember to always have a daily intake of ramen noodles because they are high in nutrients. They are only $0.99! Go buy them now.", //0
                                   "I have the sudden urge to take over all Discord Servers~! Mwhahaha.",  //1
                                   "Reported to rito games",
                                   "I'm going to have to call the FBI on you.", //3
                                   "FBI is coming now.",                        //4
                                   "OPEN UP IT'S THE FBI~!",                   //5
                                   "lolololololol",
                                   "gg fam",
                                   "y u do dis fam",
                                   "Omae wa mou shindeiru.",
                                   "Nani~?",
                                   "What is 0xFF in Binary?\nEasy it's 1111 1111.\nYou learn something everyday."
                                    };

            builder.WithTitle($"Noodles Announcement: ");
            builder.WithCurrentTimestamp();

            //Random Announcements
            int val = new Random().Next(0, noodzMsg.Length);
            builder.WithDescription(noodzMsg[val]);

            builder.WithColor(Color.LightOrange);
            await Task.Delay(250);
            await Context.Channel.SendMessageAsync("", false, builder);

            string fileName = "";
            //Quotes with Pictures
            if (val == 0) {
                fileName = "noodleQ/kappu.jpg";
                await Context.Channel.SendFileAsync(fileName);
            } else if (val == 1) {
                fileName = "noodleQ/tanya.png";
                await Context.Channel.SendFileAsync(fileName);
            } else if (val == 3) {
                fileName = "noodleQ/fbi_call.png";
                await Context.Channel.SendFileAsync(fileName);
            } else if (val == 4) {
                fileName = "noodleQ/FBI_car.png";
                await Context.Channel.SendFileAsync(fileName);
            } else if (val == 5) {
                fileName = "noodleQ/fbi.png";
                await Context.Channel.SendFileAsync(fileName);
            }

        }

        //Meme Command
        [Command("meme", RunMode = RunMode.Sync), Summary("Sends msg to server")]
        private async Task memeC([Remainder] [Summary("txt printed")] string def = "none")
        {

            int randoPic = new Random().Next(0, 2);
            string jpegF = "*.jpg";
            string pngF = "*.png";
            string gifF = "*.gif";

            string picExt = "";

            if (randoPic == 0) {
                picExt = jpegF;
            } else if (randoPic == 1) {
                picExt = pngF;
            } else if (randoPic == 2) {
                picExt = gifF;
            }

            var fileName = Directory.GetFiles("C:\\Users\\Administrator\\Dropbox\\EXE files\\NoodlesBot(Prod)\\meme", picExt);
            //Path to AmazonRD: C:\Users\Administrator\Dropbox\EXE files\NoodlesBot(Prod)\meme
            //Path to Windows PC: C:\\Users\\DrNoodles\\source\\repos\\NoodlesDiscordBot\\NoodlesDiscordBot\\meme

            int val = new Random().Next(0, fileName.Length);

            await Task.Delay(250);
            await Context.Channel.SendFileAsync(fileName[val]);

            // botMsg = $"Sorry, the functionality is currently disabled.";
            //await purgePrevMsg(botMsg, delayTime * 3);
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~WORK IN PROGRESS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //se plays a sound effect
        [Command("se", RunMode = RunMode.Sync), Summary("Sends msg to server")]
        private async Task pAsync([Remainder] [Summary("txt printed")] string def = "none") {

            await Task.Delay(250);
            string botMsg = $"Sorry, the functionality is currently disabled.";
            await purgePrevMsg(botMsg, delayTime * 3);
        }

        //plist shows the sounds directory
        [Command("plist", RunMode = RunMode.Sync), Summary("sends msg to server")]
        private async Task plistAsync([Remainder] [Summary("txt printed")] string def = "none")
        {
            await Task.Delay(250);
            string botMsg = $"Sorry, the functionality is currently disabled.";
            await purgePrevMsg(botMsg, delayTime * 3);
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        //Purge Previous Msg 
        private async Task purgePrevMsg(string msg, int delayMsgTime)
        {
            //Hold's User's msg
            var message = Context.Message;

            //Holds Bot's msg
            var botMessage = await Context.Channel.SendMessageAsync(msg);

            //Delay time
            await Task.Delay(delayMsgTime);

            //Delete Bot & User's msg
            await botMessage.DeleteAsync();
            await Task.Delay(500);
            await message.DeleteAsync();
        }

    }
}



//Admin Commands
public class DiscordAdminCommands : ModuleBase
{
    [Command("purge", RunMode = RunMode.Sync), Summary("Clears an amount of msg in the channel")]
    [RequireUserPermission(GuildPermission.Administrator)]

    private async Task purgeMsg([Remainder] int x = 0)
    {
        try
        {
            if (x <= 100)
            {
                var msgToDelete = await Context.Channel.GetMessagesAsync(x + 1).Flatten();
                await Task.Delay(1000); //1 second delay
                await Context.Channel.DeleteMessagesAsync(msgToDelete);
                Console.WriteLine($"\n> {Context.User.Username} deleted {x} message(s)");
            }
            else
            {
                await Context.Channel.SendMessageAsync("```You cannot delete more than 100 messages```");

            }
        }
        catch (Exception e)
        {
            Console.WriteLine("\n> Error: " + e);
        }

    }


    [Command("bye", RunMode = RunMode.Async), Summary("Sends msg to server.")]
    [RequireUserPermission(GuildPermission.Administrator)]
    private async Task byeAsync([Remainder] string def = "none")
    {
        var uMsg = Context.Message;
        await uMsg.DeleteAsync();

        if (Context.User.Id.ToString() == secrets.userID)
        {
            var msg = await Context.Channel.SendMessageAsync("Goodbye~!");
            Console.WriteLine($"\n> ALERT: {Context.User.Username} has shutdown the bot.");
            await Task.Delay(1500);
            for (int i = 5; i > 0; i--)
            {
                Console.WriteLine($"\n> Bot shutting off in {i}");
                await Task.Delay(1000);
            }
            await msg.DeleteAsync();
            System.Environment.Exit(1);
        }
        else
        {
            Console.WriteLine($"\n> WARNING: {Context.User.Username} attempted to shutdown the bot.");
            var msgD = await Context.Channel.SendMessageAsync(":warning: You are not Authorized to shut me down!\n");
            await Task.Delay(8000);
            await msgD.DeleteAsync();
        }

    }

    //Say command
    [Command("say", RunMode = RunMode.Sync), Summary("Sends msg to server")]
    private async Task sayAsync([Remainder] [Summary("text printed")] string input)
    {
        //int rando = new Random().Next(1, 30);   //1..29
        if (Context.User.Id.ToString() == secrets.userID)
        {
            var msg = Context.Message;
            await msg.DeleteAsync();

            //string[] token = input.Split('-');
            //var items = from s in token select s;
            //int c = items.Count();

            // builder = new EmbedBuilder();

            //builder.WithTitle($"Msg: {input}");

            //builder.WithColor(Color.Magenta);

            await Task.Delay(250);
            await Context.Channel.SendMessageAsync(input);
            //await Context.Channel.SendMessageAsync("", false, builder);
        }

    }

}


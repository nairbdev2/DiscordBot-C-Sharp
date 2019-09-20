using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Discord Libraries
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using NoodlesDiscordBot;


namespace NoodlesDiscordBot
{
    class Program
    {
        //Globals
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private string token;

        //Interchangable from test / prod. 
        bool testStage = true;

        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();


        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            await _client.SetGameAsync("Cook Da Noodles");

            _client.Log += Log;

            if (testStage == false)
            {
                token = secrets.key;
            }
            else
            {
                token = secrets.testKey;
            }

            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();

            await InstallCommandsAsync();
            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();
            await Task.Delay(-1);
        }


        public async Task InstallCommandsAsync()
        {
            //Hook the msg to event handler to our cmd handler
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());

        }


        public async Task HandleCommandAsync(SocketMessage msgParam)
        {
            //Don't process cmd if it was a system msg
            var msg = msgParam as SocketUserMessage;

            if (msg == null) return;

            //Num of Args.
            int argPos = 0;

            //Production uses '*' as command read
            if (testStage == false)
            {
                //Determine if msg is cmd
                if (!(msg.HasCharPrefix('-', ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
            }
            else
            {

                //Test uses '=' as command read
                if (!(msg.HasCharPrefix('=', ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
            }


            //Command Context
            var context = new SocketCommandContext(_client, msg);

            //Exec cmd
            var result = await _commands.ExecuteAsync(context, argPos, _services);

            if (!result.IsSuccess)
                Console.WriteLine(result.ErrorReason);

        }


        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());

            return Task.CompletedTask;

        }






    }
}

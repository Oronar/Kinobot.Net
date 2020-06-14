using AutoMapper;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kinobot.Net.Configuration;
using Kinobot.Net.Repositories;
using Kinobot.Net.Repositories.Contracts;
using Kinobot.Net.Services;
using Kinobot.Net.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using TMDbLib.Client;

namespace Kinobot.Net
{
	public class Startup
	{
		private readonly IConfiguration Configuration;

		public Startup()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();

			if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.Equals("Development", StringComparison.OrdinalIgnoreCase) ?? false)
			{
				builder.AddUserSecrets<SecretsConfiguration>();
			}

			Configuration = builder.Build();
		}

		public async Task RunAsync()
		{
			var services = new ServiceCollection();
			ConfigureServices(services);

			var provider = services.BuildServiceProvider();

			var socketClient = provider.GetRequiredService<DiscordSocketClient>();
			var commandService = provider.GetRequiredService<CommandService>();
			var commandHandler = provider.GetRequiredService<ICommandHandlerService>();
			provider.GetRequiredService<ILoggingService>();

			commandHandler.InjectCommandHandler();

			await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), provider);

			await socketClient.LoginAsync(TokenType.Bot, Configuration.GetSection("DiscordBotToken").Value);
			await socketClient.StartAsync();

			await Task.Delay(-1);
		}

		private void ConfigureServices(IServiceCollection services)
		{
			// Bot service setup
			services.AddSingleton(Configuration);
			services.AddSingleton<ICommandHandlerService, CommandHandlerService>();
			services.AddSingleton<CommandService>();
			services.AddSingleton<DiscordSocketClient>();
			services.AddSingleton<ILoggingService, ConsoleLoggingService>();

			// Mapper service setup
			services.AddAutoMapper(typeof(Startup));

			// API key setup
			services.AddSingleton(new TMDbClient(Configuration.GetSection("TMDbApiKey").Value));

			services.AddScoped<IMovieRepository, TMDbMovieRepository>();
			services.AddTransient<ITVRepository, TMDbTVRepository>();
			services.AddTransient<IMovieService, MovieService>();
			services.AddTransient<ITVService, TVService>();
		}
	}
}
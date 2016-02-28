﻿using System;
using System.Reflection;
using NLog;
using TwitchTally.IRC;
using TwitchTally.Logging;
using TwitchTally.Queueing;

namespace TwitchTally {
	class Program {
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// The main entry function for the application.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		static void Main(string[] args) {
			//Console.SetBufferSize(250, 20000);
			//Console.SetWindowSize(250, 50);
			Logger.Info("TwitchTally v" + Assembly.GetExecutingAssembly().GetName().Version + " started.");
			OutgoingQueue outgoingQueue = new OutgoingQueue();

			//StartMasterServer();
			//ConnectToIRC();
			Boolean exitApplication = false;
			while (!exitApplication) {
				Console.Write("> ");
				exitApplication = ParseCommand(Console.ReadLine());
			}
			IrcLog.CloseLog();
		}

		static void ConnectToIrc() {
			Server mainConnection = new Server {
													Hostname = Properties.Settings.Default.IRCServer,
													Port = Properties.Settings.Default.IRCPort,
													Nick = Properties.Settings.Default.IRCUsername,
													AltNick = Properties.Settings.Default.IRCUsernameAlt,
													Pass = Properties.Settings.Default.IRCPassword
												};
			mainConnection.Connect();
		}

		static void StartMasterServer() {
			//MasterListener masterListener = new MasterListener();
			//masterListener.StartListening();
		}

		static Boolean ParseCommand(String input) {
			Logger.Info("Input: {0}", input);
			String[] cmdSplit = input.Split(' ');
			switch (cmdSplit[0].ToUpper()) {
				case "EXIT":
					return true;
				case "LOG":
					if (cmdSplit.Length > 2) {
						switch (cmdSplit[2].ToUpper()) {
							case "LIST":
								Logger.Info("Lists all log files matching (PATTERN). Includes filename, size,");
								Logger.Info("and format.");
								// Do things here.
								break;
							case "REPLAY":
								Logger.Info("Reads and plays logs matching (PATTERN) to active worker nodes. This");
								Logger.Info("is generally only good to do after clearing the database.");
								// Do things here.
								break;
						}
					} else {
						Logger.Info(" Invalid command. See HELP LOG for usage information.");
					}
					break;
				case "WORKER":
					if (cmdSplit.Length > 2) {
						switch (cmdSplit[2].ToUpper()) {
							case "LIST":
								Logger.Info("Lists the currently connected workers and high level info.");
								// Do things here.
								break;
							case "DETAIL":
								Logger.Info("Shows detailed information about a specific worker node.");
								// Do things here.
								break;
						}
					} else {
						Logger.Info(" Invalid command. See HELP LOG for usage information.");
					}
					break;
				case "HELP":
					Logger.Info("TwitchTally Help:");
					Logger.Info("");
					Logger.Info("Prefix any command with HELP for in-depth usage information.");
					Logger.Info("");
					if (cmdSplit.Length > 1) {
						switch (cmdSplit[1].ToUpper()) {
							case "LOG":
								if (cmdSplit.Length > 2) {
									switch (cmdSplit[2].ToUpper()) {
										case "LIST":
											Logger.Info("Lists all log files matching (PATTERN). Includes filename, size,");
											Logger.Info("and format.");
											Logger.Info("");
											Logger.Info(" Usage: LOG LIST (PATTERN)");
											Logger.Info("   PATTERN - Any valid path wildcard (IE: 201602*.log)");
											break;
										case "REPLAY":
											Logger.Info("Reads and plays logs matching (PATTERN) to active worker nodes. This");
											Logger.Info("is generally only good to do after clearing the database.");
											Logger.Info("");
											Logger.Info(" Usage: LOG REPLAY (PATTERN)");
											Logger.Info("   PATTERN - Any valid path wildcard (IE: 201602*.log)");
											break;
									}
								} else {
									Logger.Info("    LOG LIST - Lists logs and current format.");
									Logger.Info("  LOG REPLAY - Replays logs to the worker nodes.");
								}
								break;
							case "WORKER":
								if (cmdSplit.Length > 2) {
									switch (cmdSplit[2].ToUpper()) {
										case "LIST":
											Logger.Info("Lists the currently connected workers and high level info.");
											Logger.Info("");
											Logger.Info(" Usage: WORKER LIST");
											break;
										case "DETAIL":
											Logger.Info("Shows detailed information about a specific worker node.");
											Logger.Info("");
											Logger.Info(" Usage: WORKER DETAIL (WORKER-ID)");
											Logger.Info("   WORKER-ID - ID of a worker (See: WORKER LIST)");
											break;
									}
								} else {
									Logger.Info("    WORKER LIST - Lists the currently connected workers and high level info.");
									Logger.Info("  WORKER DETAIL - Shows detailed information about a specific worker node.");
								}
								break;

						}
					} else {
						Logger.Info("     HELP - Displays this list of commands.");
						Logger.Info("      LOG - Allows IRC log replay and manipulation.");
						Logger.Info("   WORKER - Shows status information of the worker nodes.");
					}
					Logger.Info("");
					break;
			}
			return false;
		}

	}
}

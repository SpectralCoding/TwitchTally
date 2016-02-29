﻿// <copyright file="Program.cs" company="SpectralCoding.com">
//     Copyright (c) 2016 SpectralCoding
// </copyright>
// <license>
// This file is part of TwitchTally.
// 
// TwitchTally is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// TwitchTally is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with TwitchTally.  If not, see <http://www.gnu.org/licenses/>.
// </license>
// <author>Caesar Kabalan</author>

using System;
using NLog;
using TwitchTallyWorker.Queueing;

namespace TwitchTallyWorker {
	class Program {
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		// ReSharper disable once UnusedParameter.Local
		static void Main(String[] args) {
			//Master master = new Master();
			//master.Connect();
			//WorkerConnection workerConnection = new WorkerConnection();
			//workerConnection.Connect();
			IncommingQueue incommingQueue = new IncommingQueue();

			Logger.Info("Waiting for User Input before exiting.");
			Console.ReadLine();
		}
	}
}

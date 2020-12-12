﻿// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampSharp.Core.Communication.Clients
{
    /// <summary>
    ///     Contains the methods a of a communication client used by the SampSharp client to communicate with the server.
    /// </summary>
    [Obsolete("Multi-process mode is deprecated and will be removed in a future release.")]
    public interface ICommunicationClient : IDisposable
    {
        /// <summary>
        ///     Connects this client to the server.
        /// </summary>
        /// <returns></returns>
        Task Connect();

        /// <summary>
        ///     Disconnects this client from the server.
        /// </summary>
        void Disconnect();

        /// <summary>
        ///     Sends the specified command to the server.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="data">The data.</param>
        void Send(ServerCommand command, IEnumerable<byte> data);

        /// <summary>
        ///     Waits for the next command sent by the server.
        /// </summary>
        /// <returns>The command sent by the server.</returns>
        Task<ServerCommandData> ReceiveAsync();
    }
}
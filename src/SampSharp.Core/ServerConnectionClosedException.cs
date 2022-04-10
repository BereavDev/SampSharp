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

namespace SampSharp.Core
{
    /// <summary>
    /// Represents errors that occur when the connection between an <see cref="IGameModeClient"/> instance and the server disappeared.
    /// </summary>
    [Obsolete("Multi-process mode is deprecated and will be removed in a future release.")]
    public class ServerConnectionClosedException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerConnectionClosedException" /> class.
        /// </summary>
        public ServerConnectionClosedException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerConnectionClosedException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServerConnectionClosedException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerConnectionClosedException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The inner exception.</param>
        public ServerConnectionClosedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
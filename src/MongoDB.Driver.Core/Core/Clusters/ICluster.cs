/* Copyright 2013-present MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver.Core.Bindings;
using MongoDB.Driver.Core.Clusters.ServerSelectors;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core.Servers;
using MongoDB.Libmongocrypt;

namespace MongoDB.Driver.Core.Clusters
{
    /// <summary>
    /// Represents a MongoDB cluster.
    /// </summary>
    public interface ICluster : IDisposable
    {
        // events
        /// <summary>
        /// Occurs when the cluster description has changed.
        /// </summary>
        event EventHandler<ClusterDescriptionChangedEventArgs> DescriptionChanged;

        // properties
        /// <summary>
        /// Gets the cluster identifier.
        /// </summary>
        /// <value>
        /// The cluster identifier.
        /// </value>
        ClusterId ClusterId { get; }

        /// <summary>
        /// Gets the cluster description.
        /// </summary>
        /// <value>
        /// The cluster description.
        /// </value>
        ClusterDescription Description { get; }

        /// <summary>
        /// Gets the cluster settings.
        /// </summary>
        /// <value>
        /// The cluster settings.
        /// </value>
        ClusterSettings Settings { get; }

        // methods
        /// <summary>
        /// Acquires a core server session.
        /// </summary>
        /// <returns>A core server session.</returns>
        ICoreServerSession AcquireServerSession();

        /// <summary>
        /// Gets the crypt client.
        /// </summary>
        /// <returns>A crypt client.</returns>
#pragma warning disable CS3003
        CryptClient CryptClient { get; }
#pragma warning restore

        /// <summary>
        /// Initializes the cluster.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Selects a server from the cluster.
        /// </summary>
        /// <param name="selector">The server selector.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The selected server.</returns>
        IServer SelectServer(IServerSelector selector, CancellationToken cancellationToken);

        /// <summary>
        /// Selects a server from the cluster.
        /// </summary>
        /// <param name="selector">The server selector.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task representing the operation. The result of the Task is the selected server.</returns>
        Task<IServer> SelectServerAsync(IServerSelector selector, CancellationToken cancellationToken);

        /// <summary>
        /// Starts a session.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A session.
        /// </returns>
        ICoreSessionHandle StartSession(CoreSessionOptions options = null);
    }
}

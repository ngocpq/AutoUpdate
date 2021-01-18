using System;
using System.Net;
using System.Text;

namespace Bingo.Update
{
    public interface IAuthentication
    {
        /// <summary>
        ///     Apply the authentication to webclient.
        /// </summary>
        /// <param name="webClient">WebClient for which you want to use this authentication method.</param>
        void Apply(ref MyWebClient webClient);
    }

    /// <summary>
    ///     Provides Basic Authentication header for web request.
    /// </summary>
    public class BasicAuthentication : IAuthentication
    {
        private string Username { get; }

        private string Password { get; }

        /// <summary>
        ///     Initializes credentials for Basic Authentication.
        /// </summary>
        /// <param name="username">Username to use for Basic Authentication</param>
        /// <param name="password">Password to use for Basic Authentication</param>
        public BasicAuthentication(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var token = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{Password}"));
            return $"Basic {token}";
        }

        /// <inheritdoc />
        public void Apply(ref MyWebClient webClient)
        {
            webClient.Headers[HttpRequestHeader.Authorization] = ToString();
            webClient.Headers["Username"] = Username;
        }
    }

    /// <summary>
    ///     Provides credentials for Network Authentication.
    /// </summary>
    public class NetworkAuthentication : IAuthentication
    {
        private string Username { get; }

        private string Password { get; }

        /// <summary>
        ///     Initializes credentials for Network Authentication.
        /// </summary>
        /// <param name="username">Username to use for Network Authentication</param>
        /// <param name="password">Password to use for Network Authentication</param>
        public NetworkAuthentication(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <inheritdoc />
        public void Apply(ref MyWebClient webClient)
        {
            webClient.Credentials = new NetworkCredential(Username, Password);
        }
    }
}
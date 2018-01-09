#region Copyright 2018 Atif Aziz. All rights reserved.
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
//
#endregion

namespace Builders
{
    using System;
    using System.Net;

    public static class HttpWebRequestBuilder
    {
        public static Builder<HttpWebRequest> Accept(string value) =>
            req => req.Accept = value;

        public static Builder<HttpWebRequest> AllowAutoRedirect(bool value) =>
            req => req.AllowAutoRedirect = value;

        public static Builder<HttpWebRequest> AllowReadStreamBuffering(bool value) =>
            req => req.AllowReadStreamBuffering = value;

        public static Builder<HttpWebRequest> AllowWriteStreamBuffering(bool value) =>
            req => req.AllowWriteStreamBuffering = value;

        // TODO AuthenticationLevel

        public static Builder<HttpWebRequest> AutomaticDecompression(DecompressionMethods value) =>
            req => req.AutomaticDecompression = value;

        // TODO CachePolicy
        // TODO ClientCertificates
        // TODO Connection
        // TODO ConnectionGroupName
        // TODO ContentLength

        public static Builder<HttpWebRequest> ContentType(string value) =>
            req => req.ContentType = value;

        // TODO ContinueDelegate
        // TODO ContinueTimeout

        public static Builder<HttpWebRequest> UseCookies() =>
            new Builder<HttpWebRequest>(req => req.CookieContainer = new CookieContainer())
                .When(req => req.CookieContainer == null);

        public static Builder<HttpWebRequest> Credentials(ICredentials value) =>
            req => req.Credentials = value;

        // TODO Date
        // TODO Expect

        public static Builder<HttpWebRequest> SetHeader(HttpRequestHeader header, string value) =>
            req => req.Headers[header] = value;

        public static Builder<HttpWebRequest> SetHeader(string name, string value) =>
            req => req.Headers[name] = value;

        public static Builder<HttpWebRequest> AddHeader(string name, string value) =>
            req => req.Headers.Add(name, value);

        public static Builder<HttpWebRequest> AddHeader(HttpRequestHeader header, string value) =>
            req => req.Headers.Add(header, value);

        // TODO Host
        // TODO IfModifiedSince
        // TODO ImpersonationLevel
        // TODO KeepAlive
        // TODO MaximumAutomaticRedirections
        // TODO MaximumResponseHeadersLength
        // TODO MediaType

        public static readonly Builder<HttpWebRequest> MethodGet     = Method("GET");
        public static readonly Builder<HttpWebRequest> MethodHead    = Method("HEAD");
        public static readonly Builder<HttpWebRequest> MethodPost    = Method("POST");
        public static readonly Builder<HttpWebRequest> MethodPut     = Method("PUT");
        public static readonly Builder<HttpWebRequest> MethodDelete  = Method("DELETE");
        public static readonly Builder<HttpWebRequest> MethodConnect = Method("CONNECT");
        public static readonly Builder<HttpWebRequest> MethodOptions = Method("OPTIONS");
        public static readonly Builder<HttpWebRequest> MethodTrace   = Method("TRACE");
        public static readonly Builder<HttpWebRequest> MethodPatch   = Method("PATCH");

        public static Builder<HttpWebRequest> Method(string value) =>
            req => req.Method = value;

        public static Builder<HttpWebRequest> GetVerb() => Method("GET");

        // TODO Pipelined

        public static Builder<HttpWebRequest> PreAuthenticate(bool value) =>
            req => req.PreAuthenticate = value;

        // TODO ProtocolVersion
        // TODO Proxy

        public static Builder<HttpWebRequest> ReadWriteTimeout(TimeSpan value) =>
            req => req.ReadWriteTimeout = Milliseconds(value);

        public static Builder<HttpWebRequest> Referer(string value) =>
            req => req.Referer = value;

        // TODO SendChunked
        // TODO ServerCertificateValidationCallback

        public static Builder<HttpWebRequest> Timeout(TimeSpan value) =>
            req => req.Timeout = Milliseconds(value);

        // TODO TransferEncoding
        // TODO UnsafeAuthenticatedConnectionSharing

        public static Builder<HttpWebRequest> UseDefaultCredentials(bool value) =>
            req => req.UseDefaultCredentials = value;

        public static Builder<HttpWebRequest> UserAgent(string value) =>
            req => req.UserAgent = value;

        static int Milliseconds(TimeSpan value) => (int) value.TotalMilliseconds;
    }
}

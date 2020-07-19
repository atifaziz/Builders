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
    using System.Net.Cache;
    using Builder = System.Action<System.Net.HttpWebRequest>;

    public static class HttpWebRequestBuilder
    {
        public static Builder Accept(string value) =>
            req => req.Accept = value;

        public static Builder AllowAutoRedirect(bool value) =>
            req => req.AllowAutoRedirect = value;

        public static Builder AllowReadStreamBuffering(bool value) =>
            req => req.AllowReadStreamBuffering = value;

        public static Builder AllowWriteStreamBuffering(bool value) =>
            req => req.AllowWriteStreamBuffering = value;

        // TODO AuthenticationLevel

        public static Builder AutomaticDecompression(DecompressionMethods value) =>
            req => req.AutomaticDecompression = value;

        public static Builder CachePolicy(RequestCachePolicy value) =>
            req => req.CachePolicy = value;

        // TODO ClientCertificates
        // TODO Connection
        // TODO ConnectionGroupName
        // TODO ContentLength

        public static Builder ContentType(string value) =>
            req => req.ContentType = value;

        // TODO ContinueDelegate
        // TODO ContinueTimeout

        public static Builder UseCookies() =>
            req =>
            {
                if (req.CookieContainer == null)
                    req.CookieContainer = new CookieContainer();
            };

        public static Builder Credentials(ICredentials value) =>
            req => req.Credentials = value;

        // TODO Date
        // TODO Expect

        public static Builder SetHeader(HttpRequestHeader header, string value) =>
            req => req.Headers[header] = value;

        public static Builder SetHeader(string name, string value) =>
            req => req.Headers[name] = value;

        public static Builder AddHeader(string name, string value) =>
            req => req.Headers.Add(name, value);

        public static Builder AddHeader(HttpRequestHeader header, string value) =>
            req => req.Headers.Add(header, value);

        // TODO Host
        // TODO IfModifiedSince
        // TODO ImpersonationLevel
        // TODO KeepAlive
        // TODO MaximumAutomaticRedirections
        // TODO MaximumResponseHeadersLength
        // TODO MediaType

        public static readonly Builder MethodGet     = Method("GET");
        public static readonly Builder MethodHead    = Method("HEAD");
        public static readonly Builder MethodPost    = Method("POST");
        public static readonly Builder MethodPut     = Method("PUT");
        public static readonly Builder MethodDelete  = Method("DELETE");
        public static readonly Builder MethodConnect = Method("CONNECT");
        public static readonly Builder MethodOptions = Method("OPTIONS");
        public static readonly Builder MethodTrace   = Method("TRACE");
        public static readonly Builder MethodPatch   = Method("PATCH");

        public static Builder Method(string value) =>
            req => req.Method = value;

        public static Builder GetVerb() => Method("GET");

        // TODO Pipelined

        public static Builder PreAuthenticate(bool value) =>
            req => req.PreAuthenticate = value;

        // TODO ProtocolVersion
        // TODO Proxy

        public static Builder ReadWriteTimeout(TimeSpan value) =>
            req => req.ReadWriteTimeout = Milliseconds(value);

        public static Builder Referer(string value) =>
            req => req.Referer = value;

        // TODO SendChunked
        // TODO ServerCertificateValidationCallback

        public static Builder Timeout(TimeSpan value) =>
            req => req.Timeout = Milliseconds(value);

        // TODO TransferEncoding
        // TODO UnsafeAuthenticatedConnectionSharing

        public static Builder UseDefaultCredentials(bool value) =>
            req => req.UseDefaultCredentials = value;

        public static Builder UserAgent(string value) =>
            req => req.UserAgent = value;

        static int Milliseconds(TimeSpan value) => (int) value.TotalMilliseconds;
    }
}

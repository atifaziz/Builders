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

namespace Builders.Tests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Cache;
    using System.Reflection;
    using Xunit;

    public sealed class HttpWebRequestBuilderTests
    {
        static HttpWebRequest CreateExampleRequest() =>
            WebRequest.CreateHttp("http://www.example.com/");

        static void Test<T>(T value,
                            Func<T, Action<HttpWebRequest>> builderFactory,
                            Func<HttpWebRequest, T> selector)
        {
            var request = CreateExampleRequest();
            var @default = selector(request);
            Assert.NotEqual(value, @default);
            builderFactory(value)(request);
            Assert.Equal(value, selector(request));
        }

        [Theory, InlineData("application/json")]
        public void Accept(string value) =>
            Test(value, HttpWebRequestBuilder.Accept, request => request.Accept);

        [Theory, InlineData(false)]
        public void AllowAutoRedirect(bool value) =>
            Test(value, HttpWebRequestBuilder.AllowAutoRedirect,
                 request => request.AllowAutoRedirect);

        [Theory, InlineData(true)]
        public void AllowReadStreamBuffering(bool value) =>
            Test(value, HttpWebRequestBuilder.AllowReadStreamBuffering,
                 request => request.AllowReadStreamBuffering);

        [Theory, InlineData(false)]
        public void AllowWriteStreamBuffering(bool value) =>
            Test(value, HttpWebRequestBuilder.AllowWriteStreamBuffering,
                 request => request.AllowWriteStreamBuffering);

        // TODO AuthenticationLevel

        [Theory, InlineData(DecompressionMethods.Deflate)]
        public void AutomaticDecompression(DecompressionMethods value) =>
            Test(value, HttpWebRequestBuilder.AutomaticDecompression,
                 request => request.AutomaticDecompression);

        [Theory, InlineData(RequestCacheLevel.BypassCache)]
        public void CachePolicy(RequestCacheLevel value) =>
            Test(new RequestCachePolicy(value), HttpWebRequestBuilder.CachePolicy,
                 request => request.CachePolicy);

        // TODO ClientCertificates
        // TODO Connection
        // TODO ConnectionGroupName
        // TODO ContentLength

        [Theory, InlineData("application/json")]
        public void ContentType(string value) =>
            Test(value, HttpWebRequestBuilder.ContentType, request => request.ContentType);

        // TODO ContinueDelegate
        // TODO ContinueTimeout

        [Fact]
        public void UseCookiesInitializesCookieContainerUnlessInitialized()
        {
            var request = CreateExampleRequest();
            Assert.Null(request.CookieContainer);
            var builder = HttpWebRequestBuilder.UseCookies();
            builder(request);
            var cookieContainer = request.CookieContainer;
            Assert.NotNull(cookieContainer);
            builder(request);
            Assert.Same(cookieContainer, request.CookieContainer);
        }

        [Fact]
        public void Credentials()
        {
            var request = CreateExampleRequest();
            Assert.Null(request.Credentials);
            var credentials = new NetworkCredential("john.doe@example.com", "password");
            var builder = HttpWebRequestBuilder.Credentials(credentials);
            builder(request);
            Assert.Same(credentials, request.Credentials);
        }

        // TODO Date
        // TODO Expect

        [Theory, InlineData("X-Foo", "Bar")]
        public void SetHeaderByName(string name, string value)
        {
            var request = CreateExampleRequest();
            var headers = request.Headers;
            Assert.NotEqual(value, headers[name]);
            HttpWebRequestBuilder.SetHeader(name, value)(request);
            Assert.Equal(value, headers[name]);
        }

        [Theory, InlineData(HttpRequestHeader.ContentType, "application/json")]
        public void SetHeaderByHttpRequestHeader(HttpRequestHeader header, string value)
        {
            var request = CreateExampleRequest();
            var headers = request.Headers;
            Assert.NotEqual(value, headers[header]);
            HttpWebRequestBuilder.SetHeader(header, value)(request);
            Assert.Equal(value, headers[header]);
        }

        [Theory, InlineData("X-Foo", "Bar")]
        public void AddHeaderByName(string name, string value)
        {
            var request = CreateExampleRequest();
            var headers = request.Headers;
            Assert.NotEqual(value, headers[name]);
            var builder = HttpWebRequestBuilder.AddHeader(name, value);
            builder(request);
            Assert.Equal(value, headers[name]);
            builder(request);
            Assert.Equal(string.Join(",", Enumerable.Repeat(value, 2)), headers[name]);
        }

        [Theory, InlineData(HttpRequestHeader.AcceptLanguage, "ur-PK")]
        public void AddHeaderByHttpRequestHeader(HttpRequestHeader header, string value)
        {
            var request = CreateExampleRequest();
            var headers = request.Headers;
            Assert.NotEqual(value, headers[header]);
            var builder = HttpWebRequestBuilder.AddHeader(header, value);
            builder(request);
            Assert.Equal(value, headers[header]);
            builder(request);
            Assert.Equal(string.Join(",", Enumerable.Repeat(value, 2)), headers[header]);
        }

        // TODO Host
        // TODO IfModifiedSince
        // TODO ImpersonationLevel
        // TODO KeepAlive
        // TODO MaximumAutomaticRedirections
        // TODO MaximumResponseHeadersLength
        // TODO MediaType

        [Theory, InlineData("POST")]
        public void Method(string value) =>
            Test(value, HttpWebRequestBuilder.Method, request => request.Method);

        [Theory]
        [InlineData("GET"    , nameof(HttpWebRequestBuilder.MethodGet    ))]
        [InlineData("HEAD"   , nameof(HttpWebRequestBuilder.MethodHead   ))]
        [InlineData("POST"   , nameof(HttpWebRequestBuilder.MethodPost   ))]
        [InlineData("PUT"    , nameof(HttpWebRequestBuilder.MethodPut    ))]
        [InlineData("DELETE" , nameof(HttpWebRequestBuilder.MethodDelete ))]
        [InlineData("CONNECT", nameof(HttpWebRequestBuilder.MethodConnect))]
        [InlineData("OPTIONS", nameof(HttpWebRequestBuilder.MethodOptions))]
        [InlineData("TRACE"  , nameof(HttpWebRequestBuilder.MethodTrace  ))]
        [InlineData("PATCH"  , nameof(HttpWebRequestBuilder.MethodPatch  ))]
        public void MethodVerb(string value, string name)
        {
            var property = typeof(HttpWebRequestBuilder).GetField(name, BindingFlags.Static | BindingFlags.Public);
            Assert.NotNull(property);
            var builder = (Action<HttpWebRequest>) property.GetValue(null);

            var request = CreateExampleRequest();
            builder(request);

            Assert.Equal(value, request.Method);
        }

        // TODO Pipelined

        [Theory, InlineData(true)]
        public void PreAuthenticate(bool value) =>
            Test(value, HttpWebRequestBuilder.PreAuthenticate,
                 request => request.PreAuthenticate);

        // TODO ProtocolVersion

        [Theory, InlineData(12.5)]
        public void ReadWriteTimeout(double seconds) =>
            Test(TimeSpan.FromSeconds(seconds), HttpWebRequestBuilder.ReadWriteTimeout,
                 request => TimeSpan.FromMilliseconds(request.ReadWriteTimeout));

        [Theory, InlineData("http://www.example.org/")]
        public void Referer(string value) =>
            Test(value, HttpWebRequestBuilder.Referer, request => request.Referer);

        // TODO SendChunked
        // TODO ServerCertificateValidationCallback

        [Theory, InlineData(12.5)]
        public void Timeout(double seconds) =>
            Test(TimeSpan.FromSeconds(seconds), HttpWebRequestBuilder.Timeout,
                 request => TimeSpan.FromMilliseconds(request.Timeout));

        // TODO TransferEncoding
        // TODO UnsafeAuthenticatedConnectionSharing

        [Theory, InlineData(true)]
        public void UseDefaultCredentials(bool value) =>
            Test(value, HttpWebRequestBuilder.UseDefaultCredentials,
                 request => request.UseDefaultCredentials);

        [Theory, InlineData("Test/0.1")]
        public void UserAgent(string value) =>
            Test(value, HttpWebRequestBuilder.UserAgent, request => request.UserAgent);
    }
}

﻿namespace Nancy.AspNetCore.Http
{
    using System.IO;
    using Nancy.Core.Http;

    internal sealed class AspNetHttpRequest : HttpRequest
    {
        private readonly Microsoft.AspNetCore.Http.HttpRequest request;

        private readonly AspNetUrl aspNetUrl;

        public AspNetHttpRequest(HttpContext context, Microsoft.AspNetCore.Http.HttpRequest request)
        {
            this.Context = context;
            this.request = request;
            this.aspNetUrl = new AspNetUrl(request);
            this.Headers = new AspNetHeaderDictionary(request.Headers);
        }

        public override HttpContext Context { get; }

        public override HttpMethod Method
        {
            get { return this.request.Method; }
            set { this.request.Method = value.Value; }
        }

        public override Url Url
        {
            get { return this.aspNetUrl; }
            set { this.aspNetUrl.CopyFrom(value); }
        }

        public override string Protocol
        {
            get { return this.request.Protocol; }
            set { this.request.Protocol = value; }
        }

        public override IHeaderDictionary Headers { get; }

        public override long? ContentLength
        {
            get { return this.request.ContentLength; }
            set { this.request.ContentLength = value; }
        }

        public override MediaRange ContentType
        {
            get { return this.request.ContentType; }
            set { this.request.ContentType = value.ToString(); }
        }

        public override Stream Body
        {
            get { return this.request.Body; }
            set { this.request.Body = value; }
        }
    }
}

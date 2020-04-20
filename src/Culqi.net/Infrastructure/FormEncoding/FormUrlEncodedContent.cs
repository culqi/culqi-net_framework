using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Culqi.Infrastructure.FormEncoding
{
    public class FormUrlEncodedContent : ByteArrayContent
    {
        public FormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
            : base(CreateContentByteArray(nameValueCollection))
        {
            this.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            this.Headers.ContentType.CharSet = "utf-8";
        }

        private static byte[] CreateContentByteArray(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            if (nameValueCollection == null)
            {
                throw new ArgumentNullException(nameof(nameValueCollection));
            }

            return Encoding.UTF8.GetBytes(FormEncoder.CreateQueryString(nameValueCollection));
        }
    }
}

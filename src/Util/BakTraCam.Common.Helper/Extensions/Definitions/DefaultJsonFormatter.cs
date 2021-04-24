using System;
using Newtonsoft.Json;

namespace BakTraCam.Common.Helper.Extensions.Definitions
{
    public class DefaultJsonFormatter : IJsonFormatter
    {
        private Exception Exception { get; }

        public DefaultJsonFormatter(Exception exception)
        {
            Exception = exception;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(Exception.Message);
        }

    }
}

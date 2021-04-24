using System.Net;

namespace BakTraCam.Common.Helper.Extensions.Definitions
{
    public class DefaultStatusCodeProvider : IStatusCodeProvider
    {
        public int StatusCode => (int)HttpStatusCode.InternalServerError;
    }
}
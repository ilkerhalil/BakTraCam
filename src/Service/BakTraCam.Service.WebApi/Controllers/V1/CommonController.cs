using System.Threading.Tasks;
using BakTraCam.Core.Business.Application.Common;
using BakTraCam.Service.DataContract;
using Microsoft.AspNetCore.Mvc;

namespace BakTraCam.Service.WebApi.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CommonController : ControllerBase
    {
        private readonly ICommonApplication _commonApp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commonApp"></param>
        public CommonController(ICommonApplication commonApp)
        {
            _commonApp = commonApp;
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> KullaniciListesiniGetir()
        {
            var kullaniciListesiGetirAsync = await _commonApp.KullaniciListesiGetirAsync();
            return Ok(kullaniciListesiGetirAsync);
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> DuyuruListesiniGetir()
        {
            var duyuruListesiGetirAsync = await _commonApp.DuyuruListesiGetirAsync();
            return Ok(duyuruListesiGetirAsync);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> KaydetDuyuru(NoticeModel duyuruModel)
        {
            var result = await _commonApp.KaydetDuyuruAsync(duyuruModel);
            return Ok(result);
            //return result != null ? Success(result) : Fail();
        }
        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> SilDuyuru(int id)
        {
            var key = await _commonApp.SilDuyuruAsync(id);
            return Ok(key);
            //return key > 0 ? Success(key) : Fail();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> DuyuruGetir(int id)
        {
            var duyuruGetirAsync = await _commonApp.DuyuruGetirAsync(id);
            return Ok(duyuruGetirAsync);
        }
    }
}

using System.Threading.Tasks;
using BakTraCam.Core.Business.Application.Ortak;
using BakTraCam.Service.DataContract;
using Microsoft.AspNetCore.Mvc;

namespace BakTraCam.Service.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OrtakController : ControllerBase
    {
        private IOrtakApplication _ortakApp;
        public OrtakController(IOrtakApplication ortakApp)
        {
            _ortakApp = ortakApp;
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> KullaniciListesiniGetir()
        {
            var kullaniciListesiGetirAsync = await _ortakApp.KullaniciListesiGetirAsync();
            return Ok(kullaniciListesiGetirAsync);
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> DuyuruListesiniGetir()
        {
            var duyuruListesiGetirAsync = await _ortakApp.DuyuruListesiGetirAsync();
            return Ok(duyuruListesiGetirAsync);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> KaydetDuyuru(DuyuruModel duyuruModel)
        {
            var result = await _ortakApp.KaydetDuyuruAsync(duyuruModel);
            return Ok(result);
            //return result != null ? Success(result) : Fail();
        }
        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> SilDuyuru(int id)
        {
            var key = await _ortakApp.SilDuyuruAsync(id);
            return Ok(key);
            //return key > 0 ? Success(key) : Fail();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> DuyuruGetir(int id)
        {
            var duyuruGetirAsync = await _ortakApp.DuyuruGetirAsync(id);
            return Ok(duyuruGetirAsync);
        }
    }
}

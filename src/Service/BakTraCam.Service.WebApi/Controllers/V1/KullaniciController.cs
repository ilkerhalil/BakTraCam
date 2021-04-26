using System.Threading.Tasks;
using BakTraCam.Core.Business.Application.Kullanici;
using BakTraCam.Service.DataContract;
using Microsoft.AspNetCore.Mvc;

namespace BakTraCam.Service.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciApplication _kullaniciApp;
        public KullaniciController(IKullaniciApplication kullaniciApp) 
        {
            _kullaniciApp = kullaniciApp;

        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> KullaniciListesiniGetir()
        {
            var kullaniciListesiGetirAsync = await _kullaniciApp.KullaniciListesiGetirAsync();
            return Ok(kullaniciListesiGetirAsync);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> KaydetKullanici(KullaniciModel kullaniciModel)
        {
            var result = await _kullaniciApp.KaydetKullaniciAsync(kullaniciModel);
            return Ok(result);
            //return result != null ? Success(result) : Fail();
        }
        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> SilKullanici(int id)
        {
            var key = await _kullaniciApp.SilKullaniciAsync(id);
            return Ok(key);
            //return key > 0 ? Success(key) : Fail();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> KullaniciGetir(int id)
        {
            var kullaniciGetirAsync = await _kullaniciApp.KullaniciGetirAsync(id);
            return Ok(kullaniciGetirAsync);
        }
    }
}

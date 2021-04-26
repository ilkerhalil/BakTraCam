using System.Threading.Tasks;
using BakTraCam.Core.Business.Application.Bakim;
using BakTraCam.Service.DataContract;
using Microsoft.AspNetCore.Mvc;

namespace BakTraCam.Service.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BakimController : ControllerBase
    {
        private readonly IBakimApplication _bakimApp;

        public BakimController(IBakimApplication bakimApp)
        {
            _bakimApp = bakimApp;
        }
        

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> BakimListesiniGetir()
        {
            var bakimlistesiGetirAsync = await _bakimApp.BakimlistesiGetirAsync();
            return Ok(bakimlistesiGetirAsync);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> OnBesGunYaklasanBakimlariGetir()
        {
            var onBesGunYaklasanBakimlariGetirAsync = await _bakimApp.OnBesGunYaklasanBakimlariGetirAsync();
            return Ok(onBesGunYaklasanBakimlariGetirAsync);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> KaydetBakim(BakimModel bakimModel)
        {
            var result = await _bakimApp.KaydetBakimAsync(bakimModel);
            return Ok(result);
            //return result != null ? Success(result) : Fail();
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> SilBakim(int id)
        {
            var key = await _bakimApp.SilBakimAsync(id);
            return Ok(key);
            //return key >0 ? Success(key) : Fail();
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetirBakim(int id)
        {
            var getirBakimAsync = await _bakimApp.GetirBakimAsync(id);
            return Ok(getirBakimAsync);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetirBakimListesiTipFiltreli(int tip)
        {
            var getirBakimListesiTipFiltreliAsync = await _bakimApp.GetirBakimListesiTipFiltreliAsync(tip);
            return Ok(getirBakimListesiTipFiltreliAsync);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetirBakimListesiDurumFiltreli(int durum)
        {
            var getirBakimListesiDurumFiltreliAsync = await _bakimApp.GetirBakimListesiDurumFiltreliAsync(durum);
            return Ok(getirBakimListesiDurumFiltreliAsync);
        }

        
    }
}

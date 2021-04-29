using System.Threading.Tasks;
using BakTraCam.Core.Business.Application.Maintenance;
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
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceApplication _maintenanceApp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maintenanceApp"></param>
        public MaintenanceController(IMaintenanceApplication maintenanceApp)
        {
            _maintenanceApp = maintenanceApp;
        }
        

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> BakimListesiniGetir()
        {
            var bakimlistesiGetirAsync = await _maintenanceApp.BakimlistesiGetirAsync();
            return Ok(bakimlistesiGetirAsync);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> OnBesGunYaklasanBakimlariGetir()
        {
            var onBesGunYaklasanBakimlariGetirAsync = await _maintenanceApp.OnBesGunYaklasanBakimlariGetirAsync();
            return Ok(onBesGunYaklasanBakimlariGetirAsync);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> KaydetBakim(MaintenanceModel bakimModel)
        {
            var result = await _maintenanceApp.KaydetBakimAsync(bakimModel);
            return Ok(result);
            //return result != null ? Success(result) : Fail();
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> SilBakim(int id)
        {
            var key = await _maintenanceApp.SilBakimAsync(id);
            return Ok(key);
            //return key >0 ? Success(key) : Fail();
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetirBakim(int id)
        {
            var getirBakimAsync = await _maintenanceApp.GetirBakimAsync(id);
            return Ok(getirBakimAsync);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetirBakimListesiTipFiltreli(int tip)
        {
            var getirBakimListesiTipFiltreliAsync = await _maintenanceApp.GetirBakimListesiTipFiltreliAsync(tip);
            return Ok(getirBakimListesiTipFiltreliAsync);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetirBakimListesiDurumFiltreli(int durum)
        {
            var getirBakimListesiDurumFiltreliAsync = await _maintenanceApp.GetirBakimListesiDurumFiltreliAsync(durum);
            return Ok(getirBakimListesiDurumFiltreliAsync);
        }

        
    }
}

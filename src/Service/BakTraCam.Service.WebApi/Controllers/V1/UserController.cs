using System.Threading.Tasks;
using BakTraCam.Core.Business.Application.User;
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
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApp;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userApp"></param>
        public UserController(IUserApplication userApp) 
        {
            _userApp = userApp;

        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> KullaniciListesiniGetir()
        {
            var kullaniciListesiGetirAsync = await _userApp.KullaniciListesiGetirAsync();
            return Ok(kullaniciListesiGetirAsync);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> KaydetKullanici(UserModel kullaniciModel)
        {
            var result = await _userApp.KaydetKullaniciAsync(kullaniciModel);
            return Ok(result);
            //return result != null ? Success(result) : Fail();
        }
        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> SilKullanici(int id)
        {
            var key = await _userApp.SilKullaniciAsync(id);
            return Ok(key);
            //return key > 0 ? Success(key) : Fail();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> KullaniciGetir(int id)
        {
            var kullaniciGetirAsync = await _userApp.KullaniciGetirAsync(id);
            return Ok(kullaniciGetirAsync);
        }
    }
}

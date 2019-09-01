
using Contracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication12.Controllers
{
    public class ValuesController : Controller
    {
        public ILogic _Ilogic;
        public ValuesController(ILogic logic)
        {
            _Ilogic = logic;
        }

        // GET api/values

        public async Task<ActionResult> GetActors()
        {
           
            return Json(await _Ilogic.GetAllActorsAsync(), JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> RemoveActorAsync(Actor actor) {
            await _Ilogic.RemoveActorAsync(actor.name);
            return Json(await _Ilogic.GetAllActorsAsync());
        }

        public async Task<ActionResult> ResetAsync()
        {
            await _Ilogic.ResetAsync();
            return Json(await _Ilogic.GetAllActorsAsync());
        }
    }
}

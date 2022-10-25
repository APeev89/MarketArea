using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketArea.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
       
    }
}

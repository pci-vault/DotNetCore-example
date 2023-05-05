using System.Threading.Tasks;
using DotNetExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetExample.Controllers
{
    public class PciVaultController : Controller
    {
        private readonly PciVaultService _pciVaultService;

        public PciVaultController(PciVaultService pciVaultService)
        {
            _pciVaultService = pciVaultService;
        }

        public async Task<IActionResult> Index()
        {
            var ccForm = await _pciVaultService.GetCreditCardForm();
            return View(ccForm);
        }
    }
}
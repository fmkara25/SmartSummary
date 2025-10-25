using Microsoft.AspNetCore.Mvc;
using SmartSummary.Models;
using SmartSummary.Services;

namespace SmartSummary.Controllers
{
    public class SummaryController : Controller
    {
        private readonly IOpenAiService _ai;

        public SummaryController(IOpenAiService ai)
        {
            _ai = ai;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SummaryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SummaryViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.InputText))
            {
                vm.Error = "Lütfen özetlenecek metni giriniz.";
                return View(vm);
            }

            try
            {
                vm.OutputSummary = await _ai.SummarizeAsync(vm.InputText!, vm.SentenceCount, vm.Language);
            }
            catch (Exception ex)
            {
                vm.Error = "Özetleme sırasında bir hata oluştu: " + ex.Message;
            }

            return View(vm);
        }
    }
}

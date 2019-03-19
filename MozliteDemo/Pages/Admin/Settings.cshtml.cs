using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mozlite.Extensions.Security.Events;
using Mozlite.Extensions.Settings;
using Mozlite.Extensions.Storages;
using MozliteDemo.Extensions;

namespace MozliteDemo.Pages.Admin
{
    public class SettingsModel : ModelBase
    {
        private readonly ISettingsManager _settingsManager;

        public SettingsModel(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        [BindProperty]
        public SiteSettings Input { get; set; }

        public void OnGet()
        {
            Input = _settingsManager.GetSettings<SiteSettings>();
        }

        public IActionResult OnPost()
        {
            var valid = true;
            if (string.IsNullOrEmpty(Input.SiteName))
            {
                valid = false;
                ModelState.AddModelError("Input.SiteName", "��վ���Ʋ���Ϊ�գ�");
            }

            if (valid)
            {
                if (_settingsManager.SaveSettings(Input))
                {
                    EventLogger.LogCore("��������վ������Ϣ��");
                    return RedirectToSuccessPage("���Ѿ��ɹ���������վ������Ϣ��");
                }
                return ErrorPage("������վ��Ϣ���ô���");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            var mediaDirectory = GetRequiredService<IMediaDirectory>();
            var result = await mediaDirectory.UploadAsync(file, "core");
            return Json(result);
        }
    }
}
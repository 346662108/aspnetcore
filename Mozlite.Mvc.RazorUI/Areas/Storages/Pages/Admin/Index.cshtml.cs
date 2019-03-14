using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mozlite.Extensions.Storages;

namespace Mozlite.Mvc.RazorUI.Areas.Storages.Pages.Admin
{
    public class IndexModel : ModelBase
    {
        private readonly IMediaDirectory _mediaDirectory;

        public IndexModel(IMediaDirectory mediaDirectory)
        {
            _mediaDirectory = mediaDirectory;
        }

        [BindProperty(SupportsGet = true)]
        public MediaQuery Query { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Query = await _mediaDirectory.LoadAsync(Query);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string[] ids)
        {
            if (ids == null || ids.Length == 0)
                return Error("��ѡ���ļ����ٽ���ɾ��������");
            return Success("���Ѿ��ɹ�ɾ���ļ���");
        }

        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            if (file?.Length <= 0)
                return Error("��ѡ��ǿ��ļ������ϴ���");
            var result = await _mediaDirectory.UploadAsync(file, "core");
            return Json(result);
        }
    }
}
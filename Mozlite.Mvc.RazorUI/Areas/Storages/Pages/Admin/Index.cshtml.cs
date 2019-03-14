using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mozlite.Extensions.Storages;
using System;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnPostDeleteAsync(Guid[] ids)
        {
            if (ids == null || ids.Length == 0)
                return Error("��ѡ���ļ����ٽ���ɾ��������");
            foreach (var id in ids)
            {
                await _mediaDirectory.DeleteAsync(id);
            }
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
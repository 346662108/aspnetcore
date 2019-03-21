using Microsoft.AspNetCore.Mvc;
using Mozlite.Extensions.Security.Events;
using Mozlite.Extensions.Settings;
using System.Linq;

namespace Mozlite.Mvc.RazorUI.Areas.Core.Pages.Admin.Settings
{
    public class IndexModel : AdminModelBase
    {
        private readonly ISettingDictionaryManager _settingManager;
        public IndexModel(ISettingDictionaryManager settingManager)
        {
            _settingManager = settingManager;
        }

        public SettingDictionary Current { get; private set; }

        public void OnGet(int id = 0)
        {
            Current = _settingManager.Find(id);
        }

        public IActionResult OnPostDelete(int[] ids, int pid)
        {
            if (ids == null || ids.Length == 0)
                return Error("��ѡ��ʵ�����ٽ���ɾ��������");
            var settings = _settingManager.Find(pid).Children.Where(x => ids.Contains(x.Id)).ToList();
            foreach (var setting in settings)
            {
                if (setting.Count > 0)
                    return Error($"{setting.Value} ������ֵ�ʵ����Ϊ�գ���Ҫ�����������ܽ���ɾ��������");
            }

            var result = _settingManager.Delete(ids);
            if (result)
            {
                EventLogger.LogCore("ɾ�����ֵ�ʵ����{0}", string.Join(",", settings.Select(x => x.Value)));
            }

            return Json(result, "�ֵ�ʵ��");
        }
    }
}
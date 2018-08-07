﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mozlite.Extensions.Storages;
using Mozlite.Mvc;
using Mozlite.Mvc.Generators;
using Newtonsoft.Json;

namespace Mozlite.Extensions.Documents.Html
{
    /// <summary>
    /// 模板管理接口。
    /// </summary>
    public class TemplateManager : ITemplateManager
    {
        private readonly IStorageDirectory _storageDirectory;
        private readonly IMemoryCache _cache;
        private readonly ILogger<TemplateManager> _logger;
        private readonly IServiceProvider _serviceProvider;
        private const string DirectoryName = "templates";

        /// <summary>
        /// 初始化类<see cref="TemplateManager"/>。
        /// </summary>
        /// <param name="storageDirectory">存储文件夹接口。</param>
        /// <param name="cache">缓存接口。</param>
        /// <param name="logger">日志接口。</param>
        /// <param name="serviceProvider">服务提供者接口。</param>
        public TemplateManager(IStorageDirectory storageDirectory, IMemoryCache cache, ILogger<TemplateManager> logger, IServiceProvider serviceProvider)
        {
            _storageDirectory = storageDirectory;
            _cache = cache;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 安装模板，使用zip压缩。
        /// </summary>
        /// <param name="file">上传得文件实例。</param>
        /// <returns>返回状态。</returns>
        public virtual async Task<TemplateStatus> SetupAsync(IFormFile file)
        {
            var info = await _storageDirectory.SaveToTempAsync(file);
            var code = info.ComputeHash();
            var path = _storageDirectory.GetTempPath($"{info.FullName}.temp");
            ZipFile.ExtractToDirectory(info.FullName, path, Encoding.UTF8);
            var configFile = Path.Combine(path, "config.json");
            var newConfig = await LoadConfigAsync(configFile);
            if (newConfig == null)
                return TemplateStatus.ConfigMissing;
            try
            {
                var dir = _storageDirectory.GetPhysicalPath(newConfig.Id.ToString("N"));
                var configs = await LoadConfigsAsync();
                if (configs.TryGetValue(newConfig.Id, out var oldConfig))
                {
                    //已经最新
                    if (oldConfig.Code.Equals(code, StringComparison.OrdinalIgnoreCase))
                        return TemplateStatus.Succeeded;
                    Directory.Move(dir, dir + ".bak");
                }
                newConfig.Code = code;
                using (var fs = new FileStream(configFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var writer = new StreamWriter(fs, Encoding.UTF8))
                    await writer.WriteAsync(JsonConvert.SerializeObject(oldConfig));
                Directory.Move(path, dir);
                if (Directory.Exists(dir + ".bak"))
                    Directory.Delete(dir + ".bak");
                return TemplateStatus.Succeeded;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "安装模板失败：{0}", exception.Message);
                return TemplateStatus.Failured;
            }
        }

        /// <summary>
        /// 获取配置实例。
        /// </summary>
        /// <returns>返回配置实例。</returns>
        public async Task<IEnumerable<TemplateConfiguration>> LoadTemplatesAsync()
        {
            var templates = await LoadConfigsAsync();
            return templates.Values;
        }

        /// <summary>
        /// 获取配置实例。
        /// </summary>
        /// <returns>返回配置实例。</returns>
        public async Task<TemplateConfiguration> GetTemplateAsync(Guid id)
        {
            var configs = await LoadConfigsAsync();
            configs.TryGetValue(id, out var config);
            return config;
        }

        /// <summary>
        /// 获取模板物理路径。
        /// </summary>
        /// <param name="id">模板Id。</param>
        /// <returns>返回当前目录物理路径。</returns>
        public string GetTemplatePath(Guid id)
        {
            return _storageDirectory.GetPhysicalPath($"{DirectoryName}\\{id:N}");
        }

        /// <summary>
        /// 生成HTML并保存到文件中。
        /// </summary>
        /// <param name="path">路径。</param>
        /// <param name="configuration">配置实例。</param>
        /// <param name="outputTemplate">保存文件路径模板，可以用<paramref name="routeData"/>键来配置模板，如：{page}等。</param>
        /// <param name="routeData">路由实例。</param>
        public async Task SaveGeneratorAsync(string path, TemplateConfiguration configuration, string outputTemplate = null, IDictionary<string, object> routeData = null)
        {
            outputTemplate = (outputTemplate ?? Path.GetFileNameWithoutExtension(path)) + ".html";
            var generator = _serviceProvider.GetRequiredService<ITemplateGenerator>();
            var data = new RouteData();
            if (routeData != null)
            {
                foreach (var key in routeData.Keys)
                {
                    data.Values[key] = routeData[key];
                    outputTemplate = outputTemplate.Replace($"{{{key}}}", routeData[key]?.ToString());
                }
            }
            var code = await generator.GenerateAsync(path, configuration.ToTempData());
            var savePath = _storageDirectory.GetPhysicalPath(Path.Combine("html", outputTemplate));
            var dir = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (var fs = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            using (var writer = new StreamWriter(fs, Encoding.UTF8))
                await writer.WriteAsync(code);
        }

        private async Task<TemplateConfiguration> LoadConfigAsync(string file)
        {
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fs, Encoding.UTF8))
            {
                try
                {
                    return JsonConvert.DeserializeObject<TemplateConfiguration>(await reader.ReadToEndAsync());
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"读取{file}得配置文件出现错误：{exception.Message}");
                }
            }
            return null;
        }

        private async Task<IDictionary<Guid, TemplateConfiguration>> LoadConfigsAsync()
        {
            return await _cache.GetOrCreateAsync(typeof(TemplateConfiguration), async ctx =>
            {
                ctx.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                var templates = new List<TemplateConfiguration>();
                var root = new DirectoryInfo(_storageDirectory.GetPhysicalPath(DirectoryName));
                foreach (var info in root.GetDirectories("*", SearchOption.TopDirectoryOnly))
                {
                    var template = await LoadConfigAsync(Path.Combine(info.FullName, "config.json"));
                    if (template != null)
                        templates.Add(template);
                }
                return templates.ToDictionary(x => x.Id);
            });
        }
    }
}
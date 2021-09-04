using DMS.EFCore.Contracts;
using DMS.EFCore.Repository.Models;
using DMSN.Common.Extensions;
using DMSN.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DMS.EFCore.Api.Controllers
{
    /// <summary>
    /// Values控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ISysJobLogService _sysJobLogService;

        private readonly ISysJobLogMysqlService _sysJobLogMysqlService;

        private readonly ISysJobLogMysql2Service _sysJobLogMysql2Service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysJobLogService"></param>
        /// <param name="sysJobLogMysqlService"></param>
        /// <param name="sysJobLogMysql2Service"></param>
        public ValuesController(ISysJobLogService sysJobLogService, ISysJobLogMysqlService sysJobLogMysqlService, ISysJobLogMysql2Service sysJobLogMysql2Service)
        {
            _sysJobLogService = sysJobLogService;
            _sysJobLogMysqlService = sysJobLogMysqlService;
            _sysJobLogMysql2Service = sysJobLogMysql2Service;
        }

        /// <summary>
        /// 测试IOC容器
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestIoc")]
        public async Task<SysJobLog> TestIoc()
        {
            SysJobLog sysJobLog = await _sysJobLogService.GetEntityAsync(1);
            var add = await _sysJobLogService.AddAsync();


            DMS.EFCore.Repository.Mysql.Models.SysJobLog sysJobLog0 = await _sysJobLogMysqlService.GetEntityAsync(1);
            var addmysql = await _sysJobLogMysqlService.AddAsync();

            DMS.EFCore.Repository.Mysql2.Models.SysJobLog sysJobLog2 = await _sysJobLogMysql2Service.GetEntityAsync(1);
            var addmysql2 = await _sysJobLogMysql2Service.AddAsync();
            return sysJobLog;
        }

        #region GetLog4net
        /// <summary>
        /// 日志处理
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetLog4net")]
        public ActionResult GetLog4net()
        {
            CompareByByteArry("","");
            //DMS.Log4net.Logger.Info("这是log4net的日志");
            //DMS.Log4net.Logger.Error("这是log4net的异常日志");

            //DMS.NLogs.Logger.Debug("这是nlog的Debug日志");
            //DMS.NLogs.Logger.Info("这是nlog的日志");
            //DMS.NLogs.Logger.Error("这是nlog的异常日志");
            var result = new
            {
                data = "成功"
            };
            return Ok(result);
        }


        private static bool CompareByByteArry(string file1, string file2)
        {

            file1 = $"D:\\1.txt";
            file2 = $"D:\\2.txt";

            const int BYTES_TO_READ = 1024 * 10;

            using (FileStream fs1 = System.IO.File.Open(file1, FileMode.Open))
            using (FileStream fs2 = System.IO.File.Open(file2, FileMode.Open))
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];
                while (true)
                {
                    int len1 = fs1.Read(one, 0, BYTES_TO_READ);
                    int len2 = fs2.Read(two, 0, BYTES_TO_READ);
                    int index = 0;
                    if (len1 != len2) return false;
                    while (index < len1)
                    {
                        if (one[index] != two[index]) return false;
                        index++;
                    }
                    if (len1 == 0) break;
                }
            }

            return true;
        }


        #endregion

        /// <summary>
        /// 读取appsettings.json和自定义配置文件
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAppConfig")]
        public ActionResult GetAppConfig()
        {
            string memberApi = DMSN.Common.AppConfig.GetVaule<string>("MemberUrl");
            memberApi = DMSN.Common.AppConfig.GetVaule("MemberUrl");
            var ip = $"获取IP：{IPHelper.GetWebClientIp()}";
            var dev = DMSN.Common.AppConfig.GetVaule("dev");
            var redisOption = DMS.Redis.AppConfig.RedisOption;


            var result = new
            {
                memberApi,
                ip,
                dev,
                redisOption
            };
            return Ok(result);
        }

        /// <summary>
        /// StringConvertAll
        /// </summary>
        /// <returns></returns>
        [HttpGet("StringConvertAll")]
        public ActionResult StringConvertAll()
        {
            //List转字符串
            List<string> List = new List<string>();
            string strArray = string.Join(",", List);

            //字符串转List
            string str = "2,4,4,4";
            List = new List<string>(str.Split(','));

            //字符数组转Int数组
            int[] list = Array.ConvertAll<string, int>(str.Split(','), s => int.Parse(s));
            long[] cartIds = Array.ConvertAll<string, long>(str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), q => q.ToLong());
            string[] arr = str.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);

            //List<string>字符串转Int数组
            List = new List<string>();
            strArray = string.Join(",", List);
            list = Array.ConvertAll<string, int>(strArray.Split(','), s => int.Parse(s));

            return Ok();
        }
    }
}

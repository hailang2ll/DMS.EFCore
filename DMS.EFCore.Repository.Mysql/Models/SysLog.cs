using System;
using System.Collections.Generic;

#nullable disable

namespace DMS.EFCore.Repository.Mysql.Models
{
    public partial class SysLog
    {
        public string MemberName { get; set; }
        public int LogId { get; set; }
        public int? SubSysId { get; set; }
        public string SubSysName { get; set; }
        public string Ip { get; set; }
        public string Url { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public int? LogType { get; set; }
        public string Exception { get; set; }
        public bool? DeleteFlag { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}

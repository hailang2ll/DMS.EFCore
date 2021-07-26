using System;
using System.Collections.Generic;

#nullable disable

namespace DMS.EFCore.Repository.Mysql.Models
{
    public partial class SysJobLog
    {
        public int JobLogId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Message { get; set; }
        public int? TaskLogType { get; set; }
        public int? JobLogType { get; set; }
        public string ServerIp { get; set; }
    }
}

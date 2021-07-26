using DMS.Common.BaseResult;
using DMS.EFCore.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMS.EFCore.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysJobLogService
    {
        /// <summary>
        /// 同步新增
        /// </summary>
        /// <returns></returns>
        ResponseResult Add();

        /// <summary>
        /// 异步新增
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult> AddAsync();

        /// <summary>
        /// 同步查询示例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysJobLog GetEntity(int id);

        /// <summary>
        /// 异步查询示例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SysJobLog> GetEntityAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="searchText2"></param>
        /// <returns></returns>
        List<SysJobLog> GetList(string searchText, string searchText2);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="searchText2"></param>
        /// <returns></returns>
        Task<List<SysJobLog>> GetListAsync(string searchText, string searchText2);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="searchText2"></param>
        /// <returns></returns>
        List<SysJobLog> GetListExt(string searchText, string searchText2);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="searchText2"></param>
        /// <returns></returns>
        Task<List<SysJobLog>> GetListExtAsync(string searchText, string searchText2);

        /// <summary>
        /// 同步分页查询示例
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        ResponsePageResult<SysJobLog> GetPageList(int pageIndex, int pageSize, string searchText);

        /// <summary>
        /// 异步分页查询示例
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        Task<ResponsePageResult<SysJobLog>> GetPageListAsync(int pageIndex, int pageSize, string searchText);

        //Task<SysJobLog> GetFromSql();
    }
}

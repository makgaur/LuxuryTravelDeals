using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Data.Azure
{
    public interface IAuditTableHandler<T> : ITableHandler<T> where T : ITableEntity, new()
    {

    }

    public interface ICachedAuditTableHandler<T> : IAuditTableHandler<T> where T : ITableEntity, new()
    {
    }
}

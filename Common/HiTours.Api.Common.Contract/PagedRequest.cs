using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Contract
{
    public class PagedRequest
    {
        /// <summary>
        /// The pag number
        /// </summary>
        public int RequestPage { get; set; }

        /// <summary>
        /// Number of items to display per page
        /// </summary>
        public int ItemsPerPage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Common.Logging.Contract
{
    public class ChangeTypeValues
    {
        /// <summary>
        /// An item has been created
        /// </summary>
        public const string Create = "Create";

        /// <summary>
        /// An item has been modified
        /// </summary>
        public const string Modify = "Modify";

        /// <summary>
        /// An item has been deleted
        /// </summary>
        public const string Delete = "Delete";
    }
}
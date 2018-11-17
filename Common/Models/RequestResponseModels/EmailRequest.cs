using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.RequestResponseModels
{
    public class EmailRequest : RequestBase
    {
        public Order Order { get; set; }
    }
}

using System.Collections.Generic;

namespace Common.Models
{
    public abstract class ResponseBase
    {
        /// <summary>
        /// Process Time in milliseconds
        /// </summary>
        public int ProcessTime { get; set; }

        public List<Error> Errors { get; set; }
    }
}

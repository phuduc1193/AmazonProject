namespace Common.Models
{
    public abstract class ResponseBase
    {
        /// <summary>
        /// Process Time in milliseconds
        /// </summary>
        public int ProcessTime { get; set; }

        public Error Errors { get; set; }
    }
}

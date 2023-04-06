using System.Collections.Generic;

namespace Models
{
    public class Result<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}

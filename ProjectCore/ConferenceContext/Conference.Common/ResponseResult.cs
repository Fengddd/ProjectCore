namespace Conference.Common
{
    /// <summary>
    /// 返回参数
    /// </summary>
    public class ResponseResult<T>
    {
        public ResponseResult()
        {          
            IsSucceed = false;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// 信息通知
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的类型
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public string StatusCode { get; set; }

    }
}

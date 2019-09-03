using System;

namespace Conference.Common
{
    /// <summary>
    /// 领域层业务异常
    /// </summary>
    public class DomainException:Exception
   {
       public DomainException(string msg) : base(msg)
       {

       }
   }
}

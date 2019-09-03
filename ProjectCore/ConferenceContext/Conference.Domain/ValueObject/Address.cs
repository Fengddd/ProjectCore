
namespace Conference.Domain.ValueObject
{
    /// <summary>
    /// 地址
    /// </summary>
    public class Address : IValueObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="province">省</param>
        /// <param name="city">市</param>
        /// <param name="county">镇</param>
        /// <param name="addressDetails">详细地址</param>
        public Address(string province, string city, string county, string addressDetails)
        {
            Province = province;
            City = city;
            County = county;
            AddressDetails = addressDetails;
        }
        /// <summary>
        /// 无参构造
        /// </summary>
        public Address()
        {

        }

        #region 地址属性

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// 市县
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// 镇
        /// </summary>
        public string County { get; private set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string AddressDetails { get; private set; }
        #endregion
    }
}

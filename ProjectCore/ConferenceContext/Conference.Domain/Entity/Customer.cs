using System;
using Conference.Domain.ValueObject;

namespace Conference.Domain.Entity
{
   public class Customer
    {   
        public Guid Id { get; set; }
        /// <summary>
        /// 客户唯一标识
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户年龄
        /// </summary>
        public string CustomerAge { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerPhone { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public Address CustomerAddress { get; set; }

        public Customer()
        {
        }

        /// <summary>
        /// 验证实体，以及创建客户
        /// </summary>
        /// <param name="code">标识</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="customerAge">客户年龄</param>
        /// <param name="customerPhone">客户电话</param>
        /// <param name="customerAddrDto">客户地址</param>
        public Customer CreateCustomer(string code, string customerName, string customerAge, string customerPhone, Address customerAddrDto)
        {
            if (!string.IsNullOrEmpty(customerName))
            {
                throw new ArgumentException("客户ID为空！");
            }
            this.Code = code;
            this.CustomerAddress = new Address(customerAddrDto.Province, customerAddrDto.City, customerAddrDto.County, customerAddrDto.AddressDetails);
            this.CustomerAge = customerAge;
            this.CustomerName = customerName;
            this.CustomerPhone = customerPhone;
            return this;
        }
       

    }
}

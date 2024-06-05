using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LeaderboardDemo
{
    public class Customer
    {        
        /// <summary>
        /// 客户ID
        /// </summary>
        public UInt64 CustomerID { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 排名
        /// </summary>
        public uint Rank { get; set; }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace CustomerDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        /// <summary>
        /// 更新或者新增排行榜客户，如果客户存在则更新该客户的score，否则添加新客户，并对客户数据进行排序,
        /// 排序规则：score降序，customerid升序，同时更新rank字段值。
        /// </summary>
        /// <param name="customerid">客户ID</param>
        /// <param name="score">客户分数</param>
        /// <returns></returns>
        [HttpPost("/customer/{customerid}/score/{score}")]
        public async Task<IActionResult> Post(UInt64 customerid, decimal score)
        {
            return await Task.Run(() => {
                try
                {
                    var customer = Customer.Leaderboard.FirstOrDefault(c => c.CustomerID.Equals(customerid));
                    if (customer == null)
                    {
                        var newCustomer = new Customer() { CustomerID = customerid, Score = score };
                        Customer.Leaderboard.Add(newCustomer);
                        customer = newCustomer;
                    }
                    else
                    {
                        customer.Score += score;
                    }
                    Customer.Reorder();
                    return Ok(customer) as ActionResult;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            });
        }

        /// <summary>
        /// 根据排序值的范围返回排行榜中对应客户数据
        /// </summary>
        /// <param name="start">最小排序值</param>
        /// <param name="end">最大排序值</param>
        /// <returns></returns>
        [HttpGet("/leaderboard/start={start}&end={end}")]
        public async Task<IActionResult> Get(uint start, uint end)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (start > end) throw new ArgumentException("参数错误，start参数值必须小于end参数值。");
                    var customers = Customer.Leaderboard.Where(c => c.Rank >= start && c.Rank <= end).ToList();
                    return Ok(customers) as ActionResult;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            });
        }

        /// <summary>
        /// 查询排行榜中特定客户以及该客户相邻的客户数据并一起返回
        /// </summary>
        /// <param name="customerid">特定客户ID</param>
        /// <param name="high">前面相邻客户的数量</param>
        /// <param name="low">后面相邻的客户数量</param>
        /// <returns></returns>
        [HttpGet("/leaderboard/{customerid}/high={high}&low={low}")]           
        public async Task<IActionResult> Get(uint customerid, uint high, uint low)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var customer = Customer.Leaderboard.FirstOrDefault(c => c.CustomerID.Equals(customerid));
                    if (customer == null) throw new ArgumentException(string.Format("客户ID【{0}】错误，没有找到该客户。",customerid));
                    var highRank = (int)customer.Rank - (int)high;
                    var lowRank = (int)customer.Rank + (int)low;
                    var customers = Customer.Leaderboard.Where(c => c.Rank >= highRank && c.Rank <= lowRank);
                    return Ok(customers) as ActionResult;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            });
        }

        

        /// <summary>
        /// 查询出所有数据，用于检查数据排序的准确性。(测试用方法)
        /// </summary>
        /// <returns></returns>
        [HttpGet("/leaderboard")]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() => 
            {
                try
                {
                    return Ok(Customer.Leaderboard) as ActionResult;
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            });
        }
    }
}

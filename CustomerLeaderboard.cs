using System.Collections.Concurrent;
using System.Linq;

namespace LeaderboardDemo
{
    public class CustomerLeaderboard
    {
        static CustomerLeaderboard()
        {
            Leaderboard = new ConcurrentBag<Customer>();                
            InitLeaderboard();
        }
        /// <summary>
        /// 客户排行榜
        /// </summary>
        public static ConcurrentBag<Customer> Leaderboard { get; set; }
        /// <summary>
        /// 初始化测试数据
        /// </summary>
        private static void InitLeaderboard()
        {
            Leaderboard.Add(new Customer { CustomerID = 15514665, Score = 124, Rank = 1 });
            Leaderboard.Add(new Customer { CustomerID = 81546541, Score = 113, Rank = 2 });
            Leaderboard.Add(new Customer { CustomerID = 1745431, Score = 100, Rank = 3 });
            Leaderboard.Add(new Customer { CustomerID = 76786448, Score = 100, Rank = 4 });
            Leaderboard.Add(new Customer { CustomerID = 254814111, Score = 96, Rank = 5 });
            Leaderboard.Add(new Customer { CustomerID = 53274324, Score = 95, Rank = 6 });
            Leaderboard.Add(new Customer { CustomerID = 6144320, Score = 93, Rank = 7 });
            Leaderboard.Add(new Customer { CustomerID = 8009471, Score = 93, Rank = 8 });
            Leaderboard.Add(new Customer { CustomerID = 11028481, Score = 93, Rank = 9 });
            Leaderboard.Add(new Customer { CustomerID = 38819, Score = 92, Rank = 10 });
        }

        /// <summary>
        /// 对排行榜客户数据进行排序，排序规则先按照Score降序，然后按照CustomerID升序排列。
        /// 同时更新排行榜客户的排序字段Rank的值。
        /// </summary>        
        public static void Reorder()
        {
            if (Leaderboard == null) return;
            var sortedLboard = Leaderboard.OrderByDescending(o => o.Score).ThenBy(o => o.CustomerID);            
            uint rank = 1;            
            foreach (var customer in sortedLboard)
            {
                customer.Rank = rank;
                rank += 1;
            }
        }
    }
}

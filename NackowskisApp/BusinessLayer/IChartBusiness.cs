using NackowskisApp.Models;

namespace NackowskisApp.BusinessLayer
{
    public interface IChartBusiness
    {
        Chart GetValuesForChart(string userId, bool onlyOwnedAuctions, string month);
        int[] CalculateChart(int startingBids, int finalBids);
    }
}

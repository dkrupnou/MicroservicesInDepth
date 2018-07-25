using System.Threading.Tasks;

namespace BookingApp.LocalTrafficRouterService.BusinessLogicLayer
{
    public interface ILocalTrafficRouter
    {
        Task<string> Route(string serviceTag);
    }
}

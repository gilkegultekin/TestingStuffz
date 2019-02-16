using System.Threading.Tasks;

namespace Orleans.TutorialOne.GrainInterfaces
{
    public interface ILiveAgent : IGrainWithIntegerKey
    {
        Task ReceiveMessage(string message);

        Task SendMessage(string message);
    }
}
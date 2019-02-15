using System.Threading.Tasks;

namespace Orleans.TutorialOne.GrainInterfaces
{
    public interface IHello : IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }
}
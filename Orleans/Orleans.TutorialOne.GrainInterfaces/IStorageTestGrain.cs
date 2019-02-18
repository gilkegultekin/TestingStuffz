using System.Threading.Tasks;

namespace Orleans.TutorialOne.GrainInterfaces
{
    public interface IStorageTestGrain : IGrainWithIntegerKey
    {
        Task<string> ProvideValueFromStorage();

        Task WriteValueToStorage(string state);

        Task Subscribe(IValueObserver observer);
    }
}
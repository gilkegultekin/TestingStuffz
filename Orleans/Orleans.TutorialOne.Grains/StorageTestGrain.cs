using Orleans.Providers;
using Orleans.TutorialOne.GrainInterfaces;
using System;
using System.Threading.Tasks;

namespace Orleans.TutorialOne.Grains
{
    [StorageProvider(ProviderName = "SqlStorage")]
    public class StorageTestGrain : Grain<TestState>, IStorageTestGrain
    {
        //private ObserverSubscriptionManager<IValueObserver> _subsManager; obsolete olmuş

        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }

        public async Task<string> ProvideValueFromStorage()
        {
            await base.ReadStateAsync();
            return State.Value;
        }

        public Task WriteValueToStorage(string state)
        {
            State.Value = state;
            return base.WriteStateAsync();
        }
    }
}

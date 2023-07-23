using System;

namespace SmartSkus.Core
{
    //https://stackoverflow.com/questions/55775060/blazor-component-refresh-parent-when-model-is-updated-from-child-component
    public interface IMyService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }

    public class MyService : IMyService
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}

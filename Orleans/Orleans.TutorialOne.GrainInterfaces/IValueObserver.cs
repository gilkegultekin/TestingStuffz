namespace Orleans.TutorialOne.GrainInterfaces
{
    public interface IValueObserver : IGrainObserver
    {
        void ReceiveValue(string value);
    }
}
namespace ETD.Models.Services
{
    interface IObservable
    {
        void attach(IObserver ob);
    }
}

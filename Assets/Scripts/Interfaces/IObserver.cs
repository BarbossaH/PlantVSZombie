namespace Interfaces
{
    using Conf;
    public interface IObserver 
    {
        void OnDataChanged(EventTypeEnum type, object eventData=null); 
    }

}

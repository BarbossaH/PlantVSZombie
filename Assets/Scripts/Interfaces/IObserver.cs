namespace Interfaces
{
    using Conf;
    public interface IDataObserver 
    {
        void OnDataChanged(EventTypeEnum type, object eventData=null); 
    }

}

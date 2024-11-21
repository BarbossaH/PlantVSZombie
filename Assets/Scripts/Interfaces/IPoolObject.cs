namespace Interfaces
{
    public interface IPoolObject
    {
        //this interface is for checking the game object can be put into the pool, if the game object has no the component IPoolObject, which means it cannot be put into the pool.
        //But today I won't do this, because my current approach is ok, not perfect, but ok
        void Initialize();
        void ReturnObject();
    }
}
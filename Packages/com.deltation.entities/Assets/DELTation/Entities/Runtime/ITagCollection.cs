namespace DELTation.Entities
{
    public interface ITagCollection
    {
        bool Contains<T>();
        int GetCount<T>();

        void Add<T>();
        void AddMany<T>(int count);

        void Remove<T>();
        void RemoveMany<T>(int count);
        void RemoveAll<T>();

        void Clear();
    }
}
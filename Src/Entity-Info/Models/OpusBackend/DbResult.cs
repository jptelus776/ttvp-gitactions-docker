namespace EntityInfoService.Models.OpusBackend
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbResultList<T> : DbResultBase
    {
        public List<T> Records { get; set; } = new List<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DbResultDictionary<TKey, TValue> : DbResultBase where TKey : notnull
    {
        public Dictionary<TKey, TValue> Records { get; set; } = new Dictionary<TKey, TValue>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DBResultObject<T> : DbResultBase where T : new ()
    {
        public T Record { get; set; } = new T();
    }
}

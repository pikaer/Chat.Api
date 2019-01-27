namespace Infrastructure.Utility
{
    /// <summary>
    /// 单例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonProvider<T> where T : new()
    {
        public static T Instance
        {
            get { return SingletonCreator.instance; }
        }

        class SingletonCreator
        {
            internal static readonly T instance = new T();
        }
    }
}

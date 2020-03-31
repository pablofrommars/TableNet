using System.Collections.Generic;
using System.Threading.Tasks;

namespace Table.Net
{
    public static class Loader
    {
        public static Task<IList<T>> Data<T>(T[] data) => Task.FromResult(data as IList<T>);

        public static Task<IList<T>> Data<T>(List<T> data) => Task.FromResult(data as IList<T>);
    }
}

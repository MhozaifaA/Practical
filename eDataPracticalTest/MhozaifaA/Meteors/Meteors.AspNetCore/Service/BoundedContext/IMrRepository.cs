

namespace Meteors
{
    public interface IMrRepository { }
    public interface IMrRepository<TKey>: IMrRepository where TKey : struct, IEquatable<TKey> { }
}

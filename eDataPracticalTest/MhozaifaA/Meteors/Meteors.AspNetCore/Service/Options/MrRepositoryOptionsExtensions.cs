

namespace Meteors
{
    public static class MrRepositoryOptionsExtensions
    {
        public static void Sort(this MrRepositoryOptions MrRepositoryOptions, Sort sort = Meteors.Sort.Ascending)
        => MrRepositoryOptions.SortOptions.Sort = sort;

        public static MrRepositoryOptions OrderBy(this MrRepositoryOptions MrRepositoryOptions, string orderByColumn)
        {
            MrRepositoryOptions.Sort();
            MrRepositoryOptions.SortOptions.OrderByColumn = orderByColumn;
            return MrRepositoryOptions;
        }

        public static MrRepositoryOptions OrderByDescending(this MrRepositoryOptions MrRepositoryOptions, string orderByColumn)
        {
            MrRepositoryOptions.Sort(Meteors.Sort.Descending);
            MrRepositoryOptions.SortOptions.OrderByColumn = orderByColumn;
            return MrRepositoryOptions;
        }

        public static void ThenBy(this MrRepositoryOptions MrRepositoryOptions, string thenByColumn)
        => MrRepositoryOptions.SortOptions.ThenByColumn = thenByColumn;

 

        //private static string[] RepositoryDependencies => Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory).
        //   Where(x => Path.GetExtension(x) == FixedCommonValue.DllExtension).Select(x => Path.GetFileNameWithoutExtension(x))
        //    .Where(x => x.Contains(FixedCommonValue.Repository, StringComparison.OrdinalIgnoreCase)).ToArray();
    }
}

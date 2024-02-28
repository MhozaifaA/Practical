

namespace Meteors
{
    public class MrRepositoryOptions
    {
        internal SortOptions SortOptions { get; set; } = new ();
        //internal List<Type> Assemblies { get; set; } = new();
        //internal bool LoggingExceptionHandler { get; set; } = false;
       // internal bool Translator { get; set; } = false;


        //public void StopTranslator()
        //=> Translator = false;

        public void SortingNon()
         =>  SortOptions.Sort = Sort.Non;

        public void ChangeSorting(Sort sort)
        => SortOptions.Sort = sort;

        public void ChangeSortingColumn(string column)
        => SortOptions.OrderByColumn = column;

        public void ChangeSortingThenColumn(string column)
        => SortOptions.ThenByColumn= column;
    }
}

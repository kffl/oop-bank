namespace OOPBank.Classes.Filters
{
    public interface IFilterableElement
    {
        public IFilterableElement acceptFilter(IFilterVisitor filter);
    }
}
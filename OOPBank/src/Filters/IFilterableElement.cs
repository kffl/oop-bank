namespace OOPBank.Filters
{
    public interface IFilterableElement
    {
        public IFilterableElement acceptFilter(IFilterVisitor filter);
    }
}
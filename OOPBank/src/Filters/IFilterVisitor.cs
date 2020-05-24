namespace OOPBank.Filters
{
    public interface IFilterVisitor
    {
        public Operation VisitOperation(Operation operation);
    }
}
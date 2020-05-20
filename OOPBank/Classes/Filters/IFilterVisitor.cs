namespace OOPBank.Classes.Filters
{
    public interface IFilterVisitor
    {
        public Operation VisitOperation(Operation operation);
    }
}
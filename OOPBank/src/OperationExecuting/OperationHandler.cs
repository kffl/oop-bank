
namespace OOPBank.OperationExecuting
{
    public abstract class OperationHandler
    {
        private readonly OperationHandler _nextHandler;

        protected OperationHandler(OperationHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public void passToNext(Operation operation)
        {
            _nextHandler?.handle(operation);
        }

        /*
         * Must call passToNext() at the end of handling
         */
        public abstract void handle(Operation operation);
    }
}

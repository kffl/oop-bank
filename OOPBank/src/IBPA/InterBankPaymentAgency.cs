using System;
using System.Collections.Generic;
using OOPBank.Operations;

namespace OOPBank.IBPA
{
    public class InterBankPaymentAgency : IBankMediator
    {
        private class InterBankPayment
        {
            private static long IBPCounter = 0;
            public long ID { get; }
            private string fromAccountNumber;
            private IBankColleague fromBank;
            private string toAccountNumber;
            private IBankColleague toBank;
            private Money amount;
            public enum PaymentStatus
            {
                InTransfer,
                Failed,
                Completed
            }
            public PaymentStatus status { get; set; }
            private readonly Transfer transfer;

            public InterBankPayment(Transfer transfer, IBankColleague toBank)
            {
                this.transfer = transfer;
                this.toBank = toBank;
                status = PaymentStatus.InTransfer;
                ID = ++IBPCounter;
            }
            public void processPayment()
            {
                var toAccount = toBank.getAccounts().Find(a => a.AccountNumber == transfer.toAccountNumber);
                if (toAccount == null)
                {
                    transfer.setOperationStatus(Operation.OperationStatus.Rejected);
                    status = PaymentStatus.Failed;
                    (transfer.FromAccount as LocalAccount).increaseBalance(transfer.Money);
                }
                else
                {
                    toAccount.increaseBalance(transfer.Money);
                    toAccount.IncomingOperations.Add(transfer);
                    transfer.setOperationStatus(Operation.OperationStatus.Completed);
                    status = PaymentStatus.Completed;
                }
            }
        }

        private List<InterBankPayment> completedPayments = new List<InterBankPayment>();
        private Queue<InterBankPayment> queuedPayments = new Queue<InterBankPayment>();
        private static InterBankPaymentAgency Agency;
        private List<IBankColleague> banks = new List<IBankColleague>();

        //Singleton
        private InterBankPaymentAgency()
        {
        }

        public static InterBankPaymentAgency getInterBankPaymentAgency()
        {
            if (Agency == null) Agency = new InterBankPaymentAgency();
            return Agency;
        }

        public void registerBank(IBankColleague bank)
        {
            banks.Add(bank);
        }

        public void performInterBankTransfer(Transfer transfer)
        {
            var toBank = banks.Find(b => transfer.toAccountNumber.StartsWith(b.accountPrefix));
            if (toBank == null) throw new Exception("Recipients bank not found");
            var payment = new InterBankPayment(transfer, toBank);
            queuedPayments.Enqueue(payment);
        }

        public void processQueuedPayments()
        {
            while (queuedPayments.Count > 0)
            {
                var payment = queuedPayments.Dequeue();
                payment.processPayment();
                completedPayments.Add(payment);
            }
        }
    }
}

using System.Collections.Generic;
using System;

namespace OOPBank
{
    public class InterBankPaymentAgency
    {
        private class InterBankPayment
        {
            private static long IBPCounter = 0;
            public long ID { get; }
            private string fromAccountNumber;
            private Bank fromBank;
            private string toAccountNumber;
            private Bank toBank;
            private Money amount;
            public enum PaymentStatus
            {
                InTransfer,
                Failed
            }
            public PaymentStatus status { get; set; }
            public InterBankPayment(string fromAccountNumber, Bank fromBank, string toAccountNumber, Bank toBank, Money amount)
            {
                this.fromAccountNumber = fromAccountNumber;
                this.fromBank = fromBank;
                this.toAccountNumber = toAccountNumber;
                this.toBank = toBank;
                this.amount = amount;
                status = PaymentStatus.InTransfer;
                ID = ++IBPCounter;
            }
            public void processPayment() {
                var result = toBank.handleIncomingPayment(fromAccountNumber, toAccountNumber, amount);
                if (result)
                {
                    fromBank.handleConfirmation(ID);
                }
                else
                {
                    fromBank.handlePaymentFailure(ID);
                }
            }
        }
        private List<InterBankPayment> completedPayments = new List<InterBankPayment>();
        private Queue<InterBankPayment> queuedPayments = new Queue<InterBankPayment>();
        private static InterBankPaymentAgency Agency;
        private List<Bank> banks = new List<Bank>();

        //Singleton
        public static InterBankPaymentAgency getInterBankPaymentAgency()
        {
            if (Agency == null)
            {
                Agency = new InterBankPaymentAgency();
            }
            return Agency;
        }
        public void registerBank(Bank bank)
        {
            banks.Add(bank);
        }

        public long performInterBankTransfer(string fromAccountNumber, Bank fromBank, string toAccountNumber, Money amount)
        {
            var toBank = banks.Find(b => toAccountNumber.StartsWith(b.accountPrefix));
            if (toBank == null)
            {
                throw new Exception("Recipients bank not found");
            }
            var payment = new InterBankPayment(fromAccountNumber, fromBank, toAccountNumber, toBank, amount);
            queuedPayments.Enqueue(payment);
            return payment.ID;
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
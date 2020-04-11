using System.Collections.Generic;

namespace OOPBank
{
    public class InterBankPaymentAgency
    {
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
    }
}
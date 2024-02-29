using StudentCredit.Data.Banks;
using StudentCredit.Data.Common.Interfaces;

namespace StudentCredit.Data.Applications.ApplicationTwo
{
    public class ApplicationTwo : IEntity
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }

        // Дата на подаване на Приложение 2
        public DateTime CreationDate { get; set; }
        // Отчетен период
        public DateTime RecordDate { get; set; }

        // Общ размер на кредитите на банката
        public decimal TotalSum { get; set; }
        // Брой сключени договори за кредит
        public int CreditCount { get; set; }

        // Сключени кредити
        public List<RecordEntry> RecordEntries { get; set; }
    }
}

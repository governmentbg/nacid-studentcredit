using StudentCredit.Data.Common.Models;

namespace StudentCredit.Data.Banks
{
    public class Bank : Nomenclature
    {
        public string Bulstat { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public Terms Terms { get; set; }

        public List<BankContract> Contracts { get; set; } = new List<BankContract>();

        public void Update(string name, string address, string bulstat, string description)
        {
            this.Name = name;
            this.Address = address;
            this.Bulstat = bulstat;
            this.Description = description;
        }
    }
}
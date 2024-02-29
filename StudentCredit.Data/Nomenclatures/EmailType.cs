using StudentCredit.Data.Common.Models;

namespace StudentCredit.Data.Nomenclatures
{
    public class EmailType : Nomenclature
    {
        public string Subject { get; set; }

        public string Body { get; set; }
    }
}

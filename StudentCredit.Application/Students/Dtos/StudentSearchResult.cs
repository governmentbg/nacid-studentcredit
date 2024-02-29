namespace StudentCredit.Application.Students.Dtos
{
    public class StudentSearchResult
    {
        public string FullName { get; set; }

        public string Uan { get; set; }

        public string Uin { get; set; }
        public string ForeignerNumber { get; set; }
        public string IdnNumber { get; set; }
		public DateTime? BirthDate { get; set; }

		public List<StudentEducationDto> Universities { get; set; } = new List<StudentEducationDto>();

        public List<DoctoralEducationDto> Doctorals { get; set; } = new List<DoctoralEducationDto>();

        public List<StudentCreditDto> Credits { get; set; } = new List<StudentCreditDto>();
    }
}

namespace gwiBack.Application.DTOs
{
    public class FormationDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Institution { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Activity1 { get; set; }
        public string Activity2 { get; set; }
        public string Activity3 { get; set; }
    }
}

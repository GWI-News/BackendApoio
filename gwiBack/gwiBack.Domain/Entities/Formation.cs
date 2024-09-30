namespace gwiBack.Domain.Entities
{
    public class Formation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Institution { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Activity1 { get; set; }
        public string Activity2 { get; set; }
        public string Activity3 { get; set; }

        // Construtor que aceita 8 argumentos
        public Formation(Guid id, string name, string institution, DateTime startDate, DateTime endDate, string activity1, string activity2, string activity3)
        {
            Id = id;
            Name = name;
            Institution = institution;
            StartDate = startDate;
            EndDate = endDate;
            Activity1 = activity1;
            Activity2 = activity2;
            Activity3 = activity3;
        }

        // Construtor sem o Id para criação de novas formações
        public Formation(string name, string institution, DateTime startDate, DateTime endDate, string activity1, string activity2, string activity3)
        {
            Id = Guid.NewGuid(); // Gera um novo ID
            Name = name;
            Institution = institution;
            StartDate = startDate;
            EndDate = endDate;
            Activity1 = activity1;
            Activity2 = activity2;
            Activity3 = activity3;
        }
    }
}

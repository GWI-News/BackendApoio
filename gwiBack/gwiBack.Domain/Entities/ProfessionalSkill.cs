using gwiBack.Domain.Validation;

namespace gwiBack.Domain.Entities
{
    public class ProfessionalSkill
    {
        public Guid Id { get; private set; }
        public string? Skill1 { get; private set; }
        public string? Skill2 { get; private set; }
        public string? Skill3 { get; private set; }
        public string? Skill4 { get; private set; }

        // Construtor vazio para uso padrão
        public ProfessionalSkill() { }

        // Construtor para criar uma nova instância com validação
        public ProfessionalSkill(string skill1, string skill2, string skill3, string skill4)
        {
            ValidationDomain(skill1, skill2, skill3, skill4);
            Id = Guid.NewGuid(); // GUID gerado automaticamente
        }

        // Construtor para criar uma instância com Id específico (para updates)
        public ProfessionalSkill(Guid id, string skill1, string skill2, string skill3, string skill4)
        {
            ValidationDomain(skill1, skill2, skill3, skill4);
            Id = id;
        }

        // Método de validação central
        private void ValidationDomain(string skill1, string skill2, string skill3, string skill4)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(skill1), "A Skill1 é obrigatória.");
            DomainExceptionValidation.When(skill1.Length > 55, "A Skill1 não pode exceder 55 caracteres.");

            DomainExceptionValidation.When(!string.IsNullOrEmpty(skill2) && skill2.Length > 55, "A Skill2 não pode exceder 55 caracteres.");
            DomainExceptionValidation.When(!string.IsNullOrEmpty(skill3) && skill3.Length > 55, "A Skill3 não pode exceder 55 caracteres.");
            DomainExceptionValidation.When(!string.IsNullOrEmpty(skill4) && skill4.Length > 55, "A Skill4 não pode exceder 55 caracteres.");

            Skill1 = skill1;
            Skill2 = skill2;
            Skill3 = skill3;
            Skill4 = skill4;
        }

        // Método para atualizar os dados da entidade
        public void Update(Guid id, string skill1, string skill2, string skill3, string skill4)
        {
            ValidationDomain(skill1, skill2, skill3, skill4);
            Id = id;
        }
    }
}

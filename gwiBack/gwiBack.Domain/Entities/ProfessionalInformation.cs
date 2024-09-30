using gwiBack.Domain.Validation;

namespace gwiBack.Domain.Entities
{
    public class ProfessionalInformation
    {
        public Guid Id { get; private set; }
        public string? CompleteName { get; private set; }
        public string? Telefone { get; private set; }
        public string? Email { get; private set; }
        public string? Linkedin { get; private set; }
        public string? EnderecoCompleto { get; private set; }
        public string? Objetivo { get; private set; }
        public string? ImgUrl { get; private set; }

        // Construtor vazio para uso padrão
        public ProfessionalInformation() { }

        // Construtor para criar uma nova instância com validação
        public ProfessionalInformation(string completeName, string telefone, string email, string linkedin, string enderecoCompleto, string objetivo, string imgUrl)
        {
            ValidationDomain(completeName, telefone, email, linkedin, enderecoCompleto, objetivo, imgUrl);
            Id = Guid.NewGuid(); // GUID gerado automaticamente
        }

        // Construtor para criar uma instância com Id específico (para updates)
        public ProfessionalInformation(Guid id, string completeName, string telefone, string email, string linkedin, string enderecoCompleto, string objetivo, string imgUrl)
        {
            ValidationDomain(completeName, telefone, email, linkedin, enderecoCompleto, objetivo, imgUrl);
            Id = id;
        }

        // Método de validação central
        private void ValidationDomain(string completeName, string telefone, string email, string linkedin, string enderecoCompleto, string objetivo, string imgUrl)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(completeName), "O nome completo é obrigatório.");
            DomainExceptionValidation.When(completeName.Length > 255, "O nome completo não pode exceder 255 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(telefone), "O telefone é obrigatório.");
            DomainExceptionValidation.When(telefone.Length > 16, "O telefone não pode exceder 16 caracteres.");
            DomainExceptionValidation.When(!IsValidPhoneNumber(telefone), "O telefone fornecido não é válido.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "O email é obrigatório.");
            DomainExceptionValidation.When(email.Length > 255, "O email não pode exceder 255 caracteres.");
            DomainExceptionValidation.When(!IsValidEmail(email), "O email fornecido não é válido.");

            if (!string.IsNullOrEmpty(linkedin))
            {
                DomainExceptionValidation.When(linkedin.Length > 255, "A URL do LinkedIn não pode exceder 255 caracteres.");
                DomainExceptionValidation.When(!IsValidUrl(linkedin), "A URL do LinkedIn fornecida não é válida.");
            }

            if (!string.IsNullOrEmpty(enderecoCompleto))
            {
                DomainExceptionValidation.When(enderecoCompleto.Length > 510, "O endereço completo não pode exceder 510 caracteres.");
            }

            if (!string.IsNullOrEmpty(objetivo))
            {
                DomainExceptionValidation.When(objetivo.Length > 255, "O objetivo não pode exceder 255 caracteres.");
            }

            if (!string.IsNullOrEmpty(imgUrl))
            {
                DomainExceptionValidation.When(imgUrl.Length > 555, "A URL da imagem não pode exceder 555 caracteres.");
                DomainExceptionValidation.When(!IsValidUrl(imgUrl), "A URL da imagem fornecida não é válida.");
            }

            CompleteName = completeName;
            Telefone = telefone;
            Email = email;
            Linkedin = linkedin;
            EnderecoCompleto = enderecoCompleto;
            Objetivo = objetivo;
            ImgUrl = imgUrl;
        }

        // Métodos de validação de formato de dados
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidPhoneNumber(string telefone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(telefone, @"^\+?[0-9\s\-]{7,16}$");
        }

        private static bool IsValidUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        // Método para atualizar os dados da entidade
        public void Update(Guid id, string completeName, string telefone, string email, string linkedin, string enderecoCompleto, string objetivo, string imgUrl)
        {
            ValidationDomain(completeName, telefone, email, linkedin, enderecoCompleto, objetivo, imgUrl);
            Id = id;
        }
    }
}
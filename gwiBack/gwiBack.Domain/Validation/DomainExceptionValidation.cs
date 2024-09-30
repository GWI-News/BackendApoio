using System;
using gwiBack.Domain.Validation;

namespace gwiBack.Domain.Validation
{
    public class DomainExceptionValidation : Exception
    {
        // Construtor que chama o construtor base da classe Exception
        public DomainExceptionValidation(string error) : base(error)
        { }

        // Método estático para verificar a condição e lançar a exceção
        public static void When(bool condition, string errorMessage)
        {
            if (condition)
            {
                throw new DomainExceptionValidation(errorMessage);
            }
        }
    }
}

namespace Aquifer.Data.Exceptions;

public class TranslationPairKeyNotAllowedException : Exception
{
    public TranslationPairKeyNotAllowedException()
    {
    }

    public TranslationPairKeyNotAllowedException(string message) : base(message)
    {
    }

    public TranslationPairKeyNotAllowedException(string message, Exception inner) : base(message, inner)
    {
    }
}
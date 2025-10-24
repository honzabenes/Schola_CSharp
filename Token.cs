
namespace TextProcessing
{
    public enum TypeToken
    { 
        Word,
        EoF,
        EoL,
        EoP
    }

    public readonly record struct Token(TypeToken Type, string? Word);
}

namespace APICatalogo.Services;

public class MeuServico : IMeuServico
{
    public string Saudacao(string seuDescricao)
    {
        return $"Bem Vindo, {seuDescricao} \n \n {DateTime.UtcNow}";
    }
}

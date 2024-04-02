namespace AutenticacaoManfred.Application
{
    public class ValidaSenha
    {
        public bool Validar(string senha)
        {
            return (senha ?? "").Equals("Z9Z9Z9");
        }
    }
}

namespace AutenticacaoManfred.Application
{
    public class ValidaSenha
    {
        public bool Validar(string senha)
        {
            return (senha ?? "").Equals("a1b2c3");
        }
    }
}

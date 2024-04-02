using AutenticacaoManfred.Application;

namespace AutenticacaoManfred
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new Exception("Nenhum parâmetro informado como senha");
            }

            if (args.Length > 1)
            {
                throw new Exception("Mais de parâmetro informado como senha");
            }

            string senha = args.First();

            ValidatorManager validatorManager = new();
            ValidaSenha validaSenha = new();

            bool senhaValida = validaSenha.Validar(senha);

            if (senhaValida)
            {
                Console.WriteLine($"Você acertou a senha, arquivo com a senha criado na área de trabalho, {validatorManager.GetDesktopFilePath()}");
                validatorManager.CriaArquivoComSenha();
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
    }
}

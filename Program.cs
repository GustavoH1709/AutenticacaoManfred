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

            bool firstTime = validatorManager.IsFirstTime();
            bool senhaValida = validaSenha.Validar(senha);

            if (firstTime)
            {
                if (senhaValida)
                {
                    Console.WriteLine($"Você acertou a senha, arquivo com a senha criado na área de trabalho, {validatorManager.GetDesktopFilePath()}");
                    validatorManager.CriaArquivoComSenha();
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }

                Console.WriteLine($"Senha '{senha}' é incorreta");
                Thread.Sleep(1000);
                Environment.Exit(2);
            }

            bool validaCooldown = validatorManager.VerificaCooldown();

            if (!validaCooldown)
            {
                Console.WriteLine($"Espere o cooldown de 1 minuto...");
                Thread.Sleep(3000);
                Environment.Exit(1);
            }

            if (senhaValida)
            {
                Console.WriteLine($"Você acertou a senha, arquivo com a senha criado na área de trabalho, {validatorManager.GetDesktopFilePath()}");
                Thread.Sleep(3000);
                validatorManager.CriaArquivoComSenha();
                Environment.Exit(0);
            }

            Console.WriteLine($"Senha '{senha}' é incorreta");
            Environment.Exit(2);
        }
    }
}

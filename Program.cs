using AutenticacaoManfred.Application;

namespace AutenticacaoManfred
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Nenhum parâmetro informado como senha");
                Thread.Sleep(3000);
                return;
            }

            if (args.Length > 1)
            {
                Console.WriteLine("Mais de parâmetro informado como senha");
                Thread.Sleep(3000);
                return;
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
                    return;
                }

                Console.WriteLine($"Senha '{senha}' é incorreta");
                Thread.Sleep(1000);
                return;
            }

            bool validaCooldown = validatorManager.VerificaCooldown();

            if (!validaCooldown)
            {
                Console.WriteLine($"Espere o cooldown de 1 minuto...");
                Thread.Sleep(3000);
                return;
            }

            if (senhaValida)
            {
                Console.WriteLine($"Você acertou a senha, arquivo com a senha criado na área de trabalho, {validatorManager.GetDesktopFilePath()}");
                Thread.Sleep(3000);
                validatorManager.CriaArquivoComSenha();
                return;
            }

            Console.WriteLine($"Senha '{senha}' é incorreta");
            Thread.Sleep(3000);
        }
    }
}

using System.Text;

namespace AutenticacaoManfred.Application
{
    public class ValidatorManager
    {
        private readonly string _tempFilePath;
        private readonly string _desktopFilePath;

        public ValidatorManager()
        {
            _tempFilePath = $"{Path.GetTempPath()}bruteforcemanfred.txt";
            _desktopFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\bruteforcemanfred_{Guid.NewGuid()}.txt";
        }

        private string NowAsString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string GetDesktopFilePath()
        {
            return _desktopFilePath;
        }

        public bool IsFirstTime()
        {
            bool firstTime = !File.Exists(_tempFilePath);

            if (firstTime)
            {
                using (FileStream fs = File.Create(_tempFilePath))
                {
                    // Add some text to file
                    byte[] text = new UTF8Encoding(true).GetBytes(NowAsString());
                    fs.Write(text, 0, text.Length);
                    fs.Close();
                }
            }

            return firstTime;
        }

        public bool VerificaCooldown()
        {
            string value = File.ReadAllLines(_tempFilePath).First();
            DateTime data = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);

            TimeSpan diff = DateTime.Now - data;

            bool cooldownZerado = diff.TotalMinutes >= 1;

            if (cooldownZerado)
            {
                File.WriteAllText(_tempFilePath, string.Empty);

                using (FileStream fs = File.Create(_tempFilePath))
                {
                    // Add some text to file
                    byte[] text = new UTF8Encoding(true).GetBytes(NowAsString());
                    fs.Write(text, 0, text.Length);
                    fs.Close();
                }
            }

            return cooldownZerado;
        }

        public void CriaArquivoComSenha()
        {
            File.Delete(_tempFilePath);

            using (FileStream fs = File.Create(_desktopFilePath))
            {
                // Add some text to file
                byte[] text = new UTF8Encoding(true).GetBytes("a1b2c3");
                fs.Write(text, 0, text.Length);
                fs.Close();
            }
        }
    }
}

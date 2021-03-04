using System;

namespace Business.CSS
{
    public class DatabaseLogger : ILogger //logları dosyaya, uzak sunucuya, vt  mail atabilriim vs loglama yapabilrim
    {//birbirinin alternatifi olan şeyler interface ile iimplamente ediyorduk
        public void Log()
        {
            Console.WriteLine("dosyaya loglandı");

        }
    }
}

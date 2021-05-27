using System;
using System.Threading.Tasks;
using EmailProvider.Library;

namespace EmailProvider.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                ValidateArgs(args);

                var emailProviderType = await EmailProviderResolver.GetByEmailAsync(args[0]);

                Console.WriteLine(emailProviderType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ValidateArgs(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentNullException("email", "missing email argument");
            }
        }
    }
}

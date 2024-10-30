using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deutschland_Game.Service
{
    public class PersonagemService
    {

        public async Task<string> DownloadPersonagemImg64Async(string base64, string nome)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) // valida se tem internet
            {
                return null;
            }
            byte[] imageBytes = Convert.FromBase64String(base64);
            string fileName = $"{nome}_personagem.png";
            string localPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            await File.WriteAllBytesAsync(localPath, imageBytes);

            return localPath;
        }

    }
}

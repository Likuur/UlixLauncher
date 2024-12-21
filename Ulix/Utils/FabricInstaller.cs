
using System.Threading.Tasks;

namespace Ulix.Utils
{
    public static class FabricInstaller
    {
        public static async Task InstallAndLaunch(string version, string mcDir)
        {
            //Ожидает обновления
            await Task.CompletedTask;
            return;
        }
    }
}
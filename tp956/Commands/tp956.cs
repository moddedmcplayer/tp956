namespace tp956.Commands
{
    using System;
    using System.Linq;
    using CommandSystem;
    using Mirror;
    using PluginAPI.Core;
    using RemoteAdmin;
    using Object = UnityEngine.Object;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class tp956 : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.FacilityManagement))
            {
                response = "Missing permission: FacilityManagement";
                return false;
            }

            var pinata = Object.FindObjectOfType<Scp956Pinata>();
            if(pinata == null)
            {
                response = "No SCP-956 pinata found.";
                return false;
            }

            var pos = ((PlayerCommandSender)sender).ReferenceHub.gameObject.transform.position;
            pinata.Network_syncPos = pos;
            pinata._spawnPos = pos;
            pinata.Network_spawned = true;
            pinata.Network_flying = false;
            pinata._respawnTimer = pinata._huntingTime;
            response = "Brought SCP-956 pinata to your position.";
            return true;
        }

        public string Command { get; } = "tp956";
        public string[] Aliases { get; } = new []{"tppinata", "bring956"};
        public string Description { get; } = "Bring 956 to you";
    }
}
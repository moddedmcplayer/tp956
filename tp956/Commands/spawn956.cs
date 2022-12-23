namespace tp956.Commands
{
    using System;
    using System.Linq;
    using Christmas;
    using CommandSystem;
    using Mirror;
    using RemoteAdmin;
    using UnityEngine;
    using Object = UnityEngine.Object;

    //[CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class spawn956 : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.FacilityManagement))
            {
                response = "Missing permission: FacilityManagement";
                return false;
            }
            
            var pos = ((PlayerCommandSender)sender).ReferenceHub.gameObject.transform.position;
            GameObject pinataObj;
            NetworkServer.Spawn(pinataObj = Object.Instantiate(Object.FindObjectOfType<Scp956Pinata>().gameObject, pos, Quaternion.Euler(0, 0, 0)));
            var pinata = pinataObj.GetComponent<Scp956Pinata>();
            pinata.Network_syncPos = pos;
            pinata._spawnPos = pos;
            pinata.Network_spawned = true;
            pinata.Network_flying = false;
            pinata._respawnTimer = pinata._huntingTime;
            response = "Spawned SCP-956";
            
            //it no work :(
            return true;
        }

        public string Command { get; } = "spawn956";
        public string[] Aliases { get; } = new string[] { "spawnpinata" };
        public string Description { get; } = "Spawns the pinata";
    }
}
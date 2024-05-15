﻿using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using CSPracc.DataModules.Constants;
using CSPracc.Managers.BaseManagers;
using CSPracc.Managers.BaseManagers.CommandManagerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPracc.Managers.PracticeManagers
{
    /// <summary>
    /// Countdown manager
    /// </summary>
    public class CountdownManager : BaseManager
    {
        /// <summary>
        /// Constructor registering the commands
        /// </summary>
        public CountdownManager() : base()
        { 
            Commands.Add(PRACC_COMMAND.countdown, new DataModules.PlayerCommand(PRACC_COMMAND.countdown, "", CountdownCommandHandler, null, null));
        }
        private bool CountdownCommandHandler(CCSPlayerController playerController, PlayerCommandArgument args)
        {
            if (args.ArgumentCount == 1)
            {
                if (int.TryParse(args.ArgumentString, out int time))
                {
                    AddCountdown(playerController, time);
                    return true;
                }
            }
            playerController.ChatMessage($"{ChatColors.Red}Could not parse parameter");
            return false;
        }
        private void AddCountdown(CCSPlayerController player, int countdown)
        {
            GuiManager.Instance.StartCountdown(player, countdown);
        }
    }
}
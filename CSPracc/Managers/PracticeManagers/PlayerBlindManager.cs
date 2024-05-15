﻿using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using CSPracc.DataModules.Constants;
using CSPracc.Managers.BaseManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CounterStrikeSharp.API.Core.BasePlugin;

namespace CSPracc.Managers.PracticeManagers
{
    /// <summary>
    /// Player blind manager
    /// </summary>
    public class PlayerBlindManager : BaseManager
    {
        ProjectileManager ProjectileManager;
        /// <summary>
        /// Constructor for the player blind manager
        /// </summary>
        /// <param name="projectileManager">projectile manager instance</param>
        public PlayerBlindManager(ref ProjectileManager projectileManager) : base()
        {  
            ProjectileManager = projectileManager;
            CSPraccPlugin.Instance.RegisterEventHandler<EventPlayerBlind>(OnPlayerBlind, HookMode.Post);
        }
        /// <summary>
        /// Dispose object
        /// </summary>
        public new void Dispose()
        {
            GameEventHandler<EventPlayerBlind> playerblind = OnPlayerBlind;
            CSPraccPlugin.Instance.DeregisterEventHandler("player_blind", playerblind, false);
            base.Dispose();
        }
        private HookResult OnPlayerBlind(EventPlayerBlind @event, GameEventInfo info)
        {
            Methods.MsgToServer($" {ChatColors.Red}{@event.Attacker.PlayerName}{ChatColors.White} flashed {ChatColors.Blue}{@event.Userid.PlayerName}{ChatColors.White} for {ChatColors.Green}{@event.BlindDuration.ToString("0.00")}s");
            if (ProjectileManager.NoFlashList.Contains(@event.Userid.SteamID))
            {
                @event.Userid.PlayerPawn.Value!.FlashMaxAlpha = 0.5f;

            }
            return HookResult.Continue;
        }
    }
}
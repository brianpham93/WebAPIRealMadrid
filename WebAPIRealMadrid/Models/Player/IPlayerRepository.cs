using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIRealMadrid.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetPlayers();
        IEnumerable<String> GetPlayerNames();
        Player GetPlayerByID(String id);
        void InsertPlayer(Player player);
        void DeletePlayer(String id);
        void EditPlayer(Player player);
    }
}
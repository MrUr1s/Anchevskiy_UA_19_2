using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using System.Linq;
using System.Xml.Linq;

namespace Managers 
{
    public class PlayerManager : MonoBehaviour
    {
        private const string c_ConfigPath = "//Resources//PlayerSave.xml";

        public static PlayerManager Instance;
        [SerializeField]
        private List<Player> _players;
        public List<Player> Players
        {
            get
            {
                if(_players.Count == 0)
                {
                    LoadPlayer();
                }
                return _players;
            }
        }

        private void Awake()
        {
            Instance = this;
            LoadPlayer();
        }


        public void LoadPlayer()
        {
            var root = XDocument.Load(Application.dataPath + c_ConfigPath).Root;

            foreach (var player in root.Elements("Player"))
            {
                TypePlayer playerType = (TypePlayer)(int)player.Attribute("TypePlayer");
                uint heroID = (uint)player.Element("heroID");
                int countCards = (int)player.Element("countCards");
                List<uint> poolCardsID = new();
                foreach (var cardID in player.Element("poolCardsID").Elements("id"))
                {
                   poolCardsID.Add((uint)cardID);
                }

                _players.Add(new(playerType, heroID, poolCardsID, countCards));
                _players=_players.OrderBy(t => t.PlayerType).ToList();
            }

        }
        [ContextMenu("save")]
        public void SavePlayer()
        {
            XDocument xdoc = new XDocument();

            XElement Players = new("Players");
            foreach (var player in _players)
            {
                XElement XPlayer = new("Player");
                XAttribute playerType = new("TypePlayer", (int)player.PlayerType);
                XElement heroID = new("heroID", player.HeroID);
                XElement countCards = new("countCards", player.CountCards);
                XElement poolCardsID = new("poolCardsID");
                foreach (var cardID in player.PoolCardsID)
                {
                    poolCardsID.Add(new XElement("id", cardID));
                }
                XPlayer.Add(playerType, heroID, countCards, poolCardsID);
                Players.Add(XPlayer);
            }
            xdoc.Add(Players);
            xdoc.Save(Application.dataPath + c_ConfigPath);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager = Scripts.GameManagers.GameManager;

namespace Scripts.GameManagers
{
    [RequireComponent(typeof(BoardManager))]
    [RequireComponent(typeof(GameManager))]
    [RequireComponent(typeof(FightManager))]
    [RequireComponent(typeof(UIManager))]
    public class Managers : MonoBehaviour
    {
        public static BoardManager Board { get; private set; }
        public static GameManager Game { get; private set; }
        public static FightManager Fight { get; private set; }
        public static UIManager UI { get; private set; }

        private List<IManager> managers = new List<IManager>();

        private void Awake()
        {
            Board = GetComponent<BoardManager>();
            Game = GetComponent<GameManager>();
            Fight = GetComponent<FightManager>();
            UI = GetComponent<UIManager>();
            managers.Add(Board);
            managers.Add(Game);
            managers.Add(Fight);
            managers.Add(UI);
            StartCoroutine(StartAllManagers());
        }
       
        IEnumerator StartAllManagers()
        {
            foreach (IManager manager in managers)
                manager.StartManager();

            yield return null;

            int countOfReady = 0;

            while(countOfReady < managers.Count)
            {
                countOfReady = 0;

                foreach (IManager manager in managers)
                    if (manager.status == StatusOfManager.Started)
                        countOfReady++;

                yield return null;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Creature;

namespace Scripts.GameManagers
{
    public class GameManager : MonoBehaviour, IManager
    {
        public StatusOfManager status { get; private set; }
        public int GoldPoints { get; private set; }
        public int HealthPoints { get; private set; }
        public int EnergyPoints { get; private set; }

        [SerializeField] private GameObject playerPrehab;
        [SerializeField] private Sprite gold;
        [HideInInspector] public bool IsPlayerTurn = false;
        [HideInInspector] public bool IsPause = false;

        private int currentFloor = 0;
        private List<EnemyMove> enemies = new List<EnemyMove>();

        #region Init

        public void StartManager()
        {
            GoldPoints = 0;
            HealthPoints = 100;
            EnergyPoints = 100;

            status = StatusOfManager.Started;
        }

        public void SetupGameManager(Vector3 playerPosition)
        {
            if (PlayerMove.instance == null)
                PoolManager.GetFromPool(playerPrehab, playerPosition, Quaternion.identity, null);
            else
                PlayerMove.instance.transform.position = playerPosition;

            if (enemies != null)
                enemies.Clear();

            Managers.UI.ChangeFloor(currentFloor++);
        }

        public void AddEnemiesToList(EnemyMove enemy)
        {
            enemies.Add(enemy);
        }

        #endregion

        #region Game

        public void PlayEnemies()
        {
            StartCoroutine(MoveEnemies());
        }

        IEnumerator MoveEnemies()
        {
            yield return new WaitForSeconds(PlayerMove.instance.moveTime);

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].gameObject.activeInHierarchy)
                {
                    enemies[i].MoveEnemy();
                    yield return new WaitForSeconds(enemies[i].moveTime);
                }
            }

            while (IsPause)
            {
                yield return null;
            }


            IsPlayerTurn = true;
        }

        public void GameOver()
        {
            IsPlayerTurn = false;
            IsPause = true;
            PlayerMove.instance.Death();
        }

        public void DiplayGameOver()
        {
            Managers.UI.ActiveGameOverPanel(GoldPoints, currentFloor);
        }
        #endregion

        #region Scene
        public void RestartScene()
        {
            PoolManager.OnRestartLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        #endregion

        #region ChangeItems
        public void ChangeGold(int count)
        {
            GoldPoints += count;
            PlayerMove.instance.GetItem(gold);
            Managers.UI.ChangeGold(GoldPoints);
        }

        public void ChangeHealth(int count)
        {
            HealthPoints += count;

            if (HealthPoints > 100)
            {
                HealthPoints = 100;
            }
            else if (HealthPoints <= 0)
            {
                GameOver();
                return;
            }

            Managers.UI.ChangeHealth(HealthPoints);
        }

        public void ChangeEnergy(int count)
        {
            EnergyPoints += count;

            if (EnergyPoints >= 100)
            {
                EnergyPoints = 100;
            }
            else if (EnergyPoints <= 0)
            {
                IsPlayerTurn = false;
                PlayEnemies();
                Managers.UI.DisplayPopUpText("Отдых");
                EnergyPoints += 20;
            }

            Managers.UI.ChangeEnergy(EnergyPoints);
        }
        #endregion
    }
}
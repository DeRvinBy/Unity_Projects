using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Scripts.GameManagers
{
    public class BoardManager : MonoBehaviour, IManager
    {
        public StatusOfManager status { get; private set; }

        [Header("SettingForFloor")]
        public int priceOfFloor = 2;
        public int priceOfEnemy = 3;
        public int priceOfChest = 5;
        public int priceOfBank = 3;
        public int dimensionOfBoard = 8;
        public int offsetOfSpecialWalls = 2;

        [Header("Prehabs")]
        [SerializeField] private WallsObjects walls;
        [SerializeField] private Floors floors;
        [SerializeField] private ItemsOnScene items;
        [SerializeField] private GameObject[] enemies;

        private Transform boardHolder;
        private Cell[,] board;
        private List<Vector3> positionsObjects = new List<Vector3>();

        public void StartManager()
        {
            boardHolder = new GameObject().GetComponent<Transform>();
            boardHolder.name = "BoardHolder";
            SetupFloor();
            status = StatusOfManager.Started;
        }

        private void SetupFloor()
        {
            Managers.UI.ActiveFloorPanel();

            board = new Cell[dimensionOfBoard, dimensionOfBoard];
            positionsObjects.Clear();

            InitializeBoard();
            CreateBoard();

            Managers.Game.SetupGameManager(board[1, 1].Position);
            AddObjectsOnBoard();
        }

        private void InitializeBoard()
        {
            for(int y = 0; y < dimensionOfBoard; y++)
            {
                for(int x = 0; x < dimensionOfBoard; x++)
                {
                    Vector3 cellPosition = new Vector3(0.5f + x, 0.5f + y, (dimensionOfBoard + y) / 10f);

                    board[x, y] = new Cell(x, y, cellPosition);

                    if (x > 1 && y > 1 && x < (dimensionOfBoard - 2) && y < (dimensionOfBoard - 2))
                        positionsObjects.Add(cellPosition);
                }
            }
        }

        private void CreateBoard()
        {
            PoolManager.GetFromPool(walls.LeftTopCorner, board[0, dimensionOfBoard - 1].Position, Quaternion.identity, boardHolder);
            PoolManager.GetFromPool(walls.RightTopCorner, board[dimensionOfBoard - 1, dimensionOfBoard - 1].Position, Quaternion.identity, boardHolder);
            PoolManager.GetFromPool(walls.LeftDownCorner, board[0, 0].Position, Quaternion.identity, boardHolder);
            PoolManager.GetFromPool(walls.RightDownCorner, board[dimensionOfBoard - 1, 0].Position, Quaternion.identity, boardHolder);

            GameObject specialWall = walls.SpecialWalls[Random.Range(0, walls.SpecialWalls.Length)];

            for (int i = 1; i < dimensionOfBoard - 1; i++)
            {
                if (i == (offsetOfSpecialWalls + 1) || i == dimensionOfBoard - (offsetOfSpecialWalls + 2))
                    PoolManager.GetFromPool(specialWall, board[i, dimensionOfBoard - 1].Position - new Vector3(0,1,0), Quaternion.identity, boardHolder);
                else
                    PoolManager.GetFromPool(walls.HorizontalWalls[Random.Range(0, walls.HorizontalWalls.Length)], board[i, dimensionOfBoard - 1].Position, Quaternion.identity, boardHolder);

                PoolManager.GetFromPool(walls.HorizontalWalls[Random.Range(0, walls.HorizontalWalls.Length)], board[i, 0].Position, Quaternion.identity, boardHolder);
                PoolManager.GetFromPool(walls.VerticalLeftWall, board[dimensionOfBoard - 1, i].Position, Quaternion.identity, boardHolder);
                PoolManager.GetFromPool(walls.VerticalRightWall, board[0, i].Position, Quaternion.identity, boardHolder);
            }

            for(int y = 0; y < dimensionOfBoard; y++)
            {
                for(int x = 0; x < dimensionOfBoard; x++)
                {
                    if ((x == (offsetOfSpecialWalls + 1) || x == dimensionOfBoard - (offsetOfSpecialWalls + 2)) && (y == (dimensionOfBoard - 2)))
                        continue;

                    if ( x==0 || y==0 || x == (dimensionOfBoard - 1) || y == (dimensionOfBoard -1))
                    {
                        continue;
                    }

                    if( x == (dimensionOfBoard - 2) && y == (dimensionOfBoard - 2))
                    {
                        PoolManager.GetFromPool(floors.ExitFloor, board[x, y].Position, Quaternion.identity, boardHolder);
                        continue;
                    }

                    PoolManager.GetFromPool(floors.DefaultFloors[Random.Range(0, floors.DefaultFloors.Length)], board[x, y].Position, Quaternion.identity, boardHolder);
                }
            }
        }

        private Vector3 GetRandomPosition()
        {
            int randomIndex = Random.Range(0, positionsObjects.Count);
            Vector3 pos = positionsObjects[randomIndex];
            positionsObjects.RemoveAt(randomIndex);
            return pos;
        }

        private void AddObjectsOnBoard()
        {
            ArrangeObjects(items.Coin, 2, priceOfFloor + 1);
            ArrangeObjects(items.Box, 1, priceOfFloor);
            ArrangeObjects(items.Chest, priceOfFloor / priceOfChest);
            ArrangeObjects(items.Bank, priceOfFloor / priceOfBank);
            ArrangeObjects(enemies[Random.Range(0,enemies.Length)], priceOfFloor / priceOfEnemy);
        }

        private void ArrangeObjects(GameObject prehab, int minimumCount, int maximumCount)
        {
            int оbjectCount = Random.Range(minimumCount, maximumCount + 1);

            for(int i = 0; i < оbjectCount; i++)
            {
                PoolManager.GetFromPool(prehab, GetRandomPosition(), Quaternion.identity, boardHolder);
            }
        }

        private void ArrangeObjects(GameObject prehab, int count)
        {
            for (int i = 0; i < count; i++)
            {
                PoolManager.GetFromPool(prehab, GetRandomPosition(), Quaternion.identity, boardHolder);
            }
        }

        public void RestartFloor()
        {
            priceOfFloor += Mathf.CeilToInt((float)priceOfFloor / (float)priceOfEnemy);

            int countOfObjects = (2 * priceOfFloor + 1) + priceOfFloor / priceOfChest + priceOfFloor / priceOfBank + priceOfFloor / priceOfEnemy;

            dimensionOfBoard += countOfObjects / Mathf.FloorToInt(Mathf.Pow((dimensionOfBoard - 4), 2));

            for (int i = 0; i < boardHolder.childCount; i++)
                boardHolder.GetChild(i).gameObject.SetActive(false);

            SetupFloor();
        }
    }

    [Serializable]
    public class WallsObjects
    {
        public GameObject[] HorizontalWalls;
        public GameObject[] SpecialWalls;
        public GameObject VerticalLeftWall;
        public GameObject VerticalRightWall;
        public GameObject LeftTopCorner;
        public GameObject RightTopCorner;
        public GameObject LeftDownCorner;
        public GameObject RightDownCorner;
    }
    [Serializable]
    public class Floors
    {
        public GameObject[] DefaultFloors;
        public GameObject ExitFloor;
    }
    [Serializable]
    public class ItemsOnScene
    {
        public GameObject Box;
        public GameObject Chest;
        public GameObject Bank;
        public GameObject Coin;
    }
    class Cell
    {
        public int X;
        public int Y;
        public Vector3 Position;
        public Cell(int x, int y, Vector3 pos)
        {
            X = x;
            Y = y;
            Position = pos;
        }
    }
}
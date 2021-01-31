using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Pool;

namespace Scripts.Game.GameObjects
{
    public class UIManager : MonoBehaviour
    {
        public void SetPause()
        {
            Time.timeScale = 0;
        }

        public void Continue()
        {
            Time.timeScale = 1;
        }

        public void RestartScene()
        {
            Time.timeScale = 1;
            PoolManager.OnRestartLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitToMenu()
        {
            PoolManager.OnRestartLevel();
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

}
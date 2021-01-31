using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Save
{
    public class SaveManager : MonoBehaviour
    {
        public static PlayerSave Save = new PlayerSave();

        public static void LoadSave()
        {
            if (PlayerPrefs.HasKey("PlayerSave"))
            {
                Save = JsonUtility.FromJson<PlayerSave>(PlayerPrefs.GetString("PlayerSave"));
            }
            else
            {
                Save.SpaceStone = 0;
                Save.IsGreenPlanetActive = true;
                Save.IsRedPlanetActive = false;
            }
        }

        public static void SaveSave()
        {
            PlayerPrefs.SetString("PlayerSave", JsonUtility.ToJson(Save));
        }
    }

    public class PlayerSave
    {
        public int SpaceStone;
        public bool IsGreenPlanetActive;
        public bool IsRedPlanetActive;
    }
}
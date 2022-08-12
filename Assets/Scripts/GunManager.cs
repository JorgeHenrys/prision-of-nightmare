using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GunManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> guns = new List<Sprite>();
    public int selectedGun = 0;
    public GameObject playerGun;


    void Start()
    {
    }

    public void NextOption()
    {
        selectedGun = selectedGun + 1;
        if (selectedGun == guns.Count)
        {
            selectedGun = 0;
        }
        sr.sprite = guns[selectedGun];
    }

    public void BackOption()
    {
        selectedGun = selectedGun - 1;
        if (selectedGun < 0)
        {
            selectedGun = guns.Count - 1;
        }
        sr.sprite = guns[selectedGun];
    }

    public void ExitComputer()
    {
        PrefabUtility.SaveAsPrefabAsset(playerGun, "Assets/Prefabs/SelectedGun.prefab");
        SceneManager.LoadScene("RoomBob");
    }
}

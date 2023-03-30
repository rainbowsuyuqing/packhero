using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

public class StartGame : MonoBehaviour
{
   
    public void OnStartGame(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        Platformer.Gameplay.PlayerSpawn.lastCheckPointPos = new Vector2(-124.5f,-2);
    }

}

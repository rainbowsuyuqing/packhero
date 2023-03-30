using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
namespace Platformer.Gameplay
{
public class checkPoint : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.transform.tag == "Player")
    {
      
      PlayerSpawn.lastCheckPointPos = transform.position;
      
    }
  }
}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
   public List<Manager> managers = new List<Manager>();

   private void Start()
   {
      Application.targetFrameRate = 60;
      foreach (var manager in managers)
      {
         manager.Initialize();
      }
   }
}

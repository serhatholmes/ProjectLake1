using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum Scene
    {
        Game,
    }

   public static void Load(Scene scene)
    {

        SceneManager.LoadScene(scene.ToString());

    }


}

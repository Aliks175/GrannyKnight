using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading
{
    //public void LoadGame(ListScene listScene)
    //{
    //    LoadSingle(ListScene.Game);

    //}


    public AsyncOperation LoadAdditive(ListScene listScene)
    {
        return SceneManager.LoadSceneAsync((int)listScene, LoadSceneMode.Additive);
    }

    //public void LoadSingle(ListScene listScene)
    //{
    //    SceneManager.LoadSceneAsync((int)listScene, LoadSceneMode.Additive);
    //}

    public AsyncOperation UnLoadAdditive(ListScene listScene)
    {
        return SceneManager.UnloadSceneAsync((int)listScene);
    }

    public AsyncOperation LoadSingle(ListScene listScene)
    {
        return SceneManager.LoadSceneAsync((int)listScene);
    }
}
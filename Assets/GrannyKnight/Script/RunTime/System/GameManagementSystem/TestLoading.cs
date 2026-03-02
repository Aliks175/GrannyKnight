using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoading 
{
    public void LoadAdditive(ListScene listScene)
    {
        SceneManager.LoadSceneAsync((int)listScene, LoadSceneMode.Additive);
    }
}

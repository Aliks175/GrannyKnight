using UnityEngine.SceneManagement;

public class Loading
{
    public void LoadAdditive(ListScene listScene)
    {
        SceneManager.LoadSceneAsync((int)listScene, LoadSceneMode.Additive);
    }

    public void LoadSingle(ListScene listScene)
    {
        SceneManager.LoadSceneAsync((int)listScene, LoadSceneMode.Additive);
    }
}
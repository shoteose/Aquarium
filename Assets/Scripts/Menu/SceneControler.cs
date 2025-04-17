using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void MudaCenaMundo()
    {
        SceneManager.LoadScene("SimuladorAquarium");

    }
}

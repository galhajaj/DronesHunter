using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CheckEscapeKey : MonoBehaviour
{
    public enum EscapeResult
    {
        EXIT_GAME,
        MOVE_TO_MAIN_SCENE
    }

    [SerializeField]
    private EscapeResult _result = EscapeResult.MOVE_TO_MAIN_SCENE;
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_result == EscapeResult.EXIT_GAME)
                Application.Quit();
            else if (_result == EscapeResult.MOVE_TO_MAIN_SCENE)
                SceneManager.LoadScene("Main");
        }
	}
}

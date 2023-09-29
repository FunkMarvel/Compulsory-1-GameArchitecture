// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////
// //FileName: MainMenu.cs
// //FileType: Visual C# Source file
// //Author : Anders P. Åsbø
// //Created On : 29/09/2023
// //Last Modified On : 29/09/2023
// //Copy Rights : Anders P. Åsbø
// //Description :
// //////////////////////////////////////////////////////////////////////////
// //////////////////////////////

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameLevel;

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene(gameLevel);
    }
}
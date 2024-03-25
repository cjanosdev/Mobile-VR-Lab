using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.SkipTutorial
{
    public class ViewTutorialButton : MonoBehaviour
    {
        public void OnViewTutorialButtonClick()
        {
            SceneManager.LoadScene("GestureTutorialScene");
        }
    }
}
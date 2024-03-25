using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.SkipTutorial
{
    public class ViewTutorial : MonoBehaviour
    {
        public void OnViewTutorialButtonClick()
        {
            SceneManager.LoadScene("GestureTutorialScene");
        }
    }
}
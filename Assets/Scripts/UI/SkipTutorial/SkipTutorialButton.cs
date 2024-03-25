using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.SkipTutorial
{
    public class SkipTutorialButton : MonoBehaviour
    {
        public void OnSkipTutorialButtonClick()
        {
            SceneManager.LoadScene("SampleScene0");
        }
    }
}
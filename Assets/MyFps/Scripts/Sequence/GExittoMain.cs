using UnityEngine;

namespace MyFps {
    public class GExittoMain : MonoBehaviour {
        #region Variables
        [SerializeField] SceneFader sceneFader;
        [SerializeField] string sceneName = "MainMenu";
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                //씬 페이드 시작
                sceneFader.FadeTo("MainMenu");
            }
        }
        #endregion

        #region Custom Method
        #endregion
    }

}

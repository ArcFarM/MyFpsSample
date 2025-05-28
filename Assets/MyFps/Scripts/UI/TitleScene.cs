using System.Collections;
using TMPro;
using UnityEngine;

namespace MyFps {
    public class TitleScene : MonoBehaviour {
        #region Variables
        //any key text 보여주기
        public TextMeshProUGUI titleText;
        [SerializeField] float textShowDelay = 2f; // 텍스트 표시 지연 시간
        [SerializeField] float forceGo = 25f; // 강제 이동 시간
        //장면 전환
        public SceneFader sceneFader;
        [SerializeField] string nextSceneName = "MainMenu"; // 다음 씬 이름
        bool flag = false;
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        void Start() {
            titleText.gameObject.SetActive(false); // 시작 시 텍스트 비활성화
            sceneFader.FadeStart(); // 페이드 인 시작
            StartCoroutine(ShowTitleText());
        }

        private void Update() {
            if (flag && Input.GetKeyDown(KeyCode.Space)) {
                // 게임 시작 로직
                sceneFader.FadeTo(nextSceneName);
            }
        }
        #endregion

        #region Custom Method
        IEnumerator ShowTitleText() {
            AudioManager.Instance.Play("TitleMusic"); // 타이틀 음악 재생
            yield return new WaitForSeconds(textShowDelay);
            titleText.gameObject.SetActive(true);
            flag = true;
            yield return new WaitForSeconds(forceGo);
            sceneFader.FadeTo(nextSceneName); // 강제 이동
        }
        #endregion
    }

}

using UnityEngine;

namespace MyFps {
    public class MainMenu : MonoBehaviour {
        #region Variables
        //씬 불러오기
        public string sceneName = "Scene1"; //게임 씬 이름
        public SceneFader sceneFader;
        //
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        private void Start() {
            sceneFader.FadeStart(0f); //게임 시작시 페이드 효과
        }
        #endregion

        #region Custom Method
        public void StartGame() {
            sceneFader.FadeTo(sceneName); //씬 전환
        }
        public void LoadGame() {
            //게임 로드 기능은 아직 구현되지 않았습니다.
            Debug.Log("Load Game - 기능 구현 필요"); //콘솔에 로드 메시지 출력
        }
        public void OpenOptions() {
            //옵션 메뉴 열기 기능은 아직 구현되지 않았습니다.
            Debug.Log("Open Options - 기능 구현 필요"); //콘솔에 옵션 메시지 출력
        }
        public void OpenCredits() {
            //크레딧 메뉴 열기 기능은 아직 구현되지 않았습니다.
            Debug.Log("Open Credits - 기능 구현 필요"); //콘솔에 크레딧 메시지 출력
        }
        public void QuitGame() {
            Application.Quit(); //게임 종료
            Debug.Log("Quit Game"); //콘솔에 게임 종료 메시지 출력
        }
        #endregion
    }

}

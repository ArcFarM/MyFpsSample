using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFps {
    public class Pause : MonoBehaviour {
        #region Variables
        [SerializeField] GameObject pauseUI; //Pause UI 오브젝트
        #endregion
        #region Properties
        #endregion
        #region Unity Event Method
        //esc키 입력 및 재개 버튼에 반응할 함수
        public void OnPause(InputAction.CallbackContext ctx) {
            if(ctx.performed) {
                TogglePause();
            }
        }
        #endregion
        #region Custom Method
        //토글 방식을 통해 esc키를 누르면 UI가 활성화/비활성화 된다
        public void TogglePause() {
            //커서 제어 변경
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? 
                CursorLockMode.None : CursorLockMode.Locked;
            //Timescale 설정
            Time.timeScale = Time.timeScale == 0 ?
                1 : 0;
            //UI 활성화/비활성화
            pauseUI.SetActive(!pauseUI.activeSelf);
        }
        #endregion
    }

}

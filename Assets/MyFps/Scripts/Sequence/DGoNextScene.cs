using UnityEngine;

namespace MyFps {
    public class DGoNextScene : Interactive {
        #region Variables
        //트리거를 발생시킬 오브젝트
        public GameObject triggerObject;
        //문 여는 소리 및 애니메이션
        public AudioSource doorBang;
        public Animator doorAnimator;
        string isOpen = "IsOpen"; //문 여는 애니메이션 파라미터
        //배경음
        public AudioSource bgm; //배경음
        //씬 전환
        public SceneFader sceneFader;
        public string sceneName = "NextScene"; //다음 씬 이름

        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other) {
            triggerObject.GetComponent<BoxCollider>().enabled = false; //트리거 발생 전 넘어가기 방지용 콜라이더 비활성화
            DoAction(); //트리거에 플레이어가 들어오면 DoAction 실행
        }
        #endregion

        #region Custom Method
        protected override void DoAction() {
            GetComponent<BoxCollider>().enabled = false; //트리거 콜라이더 비활성화
            doorAnimator.SetBool(isOpen, true); //문 여는 애니메이션 실행
            doorBang.Play(); //문 여는 소리 재생
            bgm.Stop(); //배경음 정지
            triggerObject.SetActive(false); //트리거 오브젝트 비활성화
            sceneFader.FadeTo(sceneName); //씬 전환
        }
        #endregion
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFps {
    public class LockedDoor : Interactive {
        #region Variables
        bool isLocked = true; //문이 잠겨있는지 여부
        [SerializeField] GameObject door; //문 오브젝트
        string originalMessage = ""; //원래 메시지
        [SerializeField] string lockedSound = "DoorLocked"; //잠긴 문 소리
        [SerializeField] string lockedMessage = "It is locked.\n I should find a key."; //잠긴 문 메시지
        [SerializeField] string unlockedMessage = "The door is now open."; //잠금 해제 메시지
        [SerializeField] string animParam = "IsOpen"; //애니메이션 파라미터 이름
        [SerializeField] Animator animator; //문 애니메이터
        [SerializeField] PlayerController playerInput; //플레이어 입력
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        private void Start() {
            originalMessage = action; //원래 메시지를 저장
        }

        #endregion

        #region Custom Method
        protected override void DoAction() {
            //열쇠 검사
            if (PlayerManager.Instance.HasKey(KeyType.KEY_ROOM2)) {
                isLocked = false; //열쇠를 가지고 있다면 문이 잠금 해제됨
            }
            //문이 잠겨있을 경우
            if (isLocked) {
                StartCoroutine(LockedDoorInteract()); //잠긴 문 코루틴 실행
            } else {
                StartCoroutine(OpenedDoor()); //문이 열렸을 경우 코루틴 실행
            }
        }

        IEnumerator LockedDoorInteract() {
            action = lockedMessage; //잠긴 문 메시지 표시
            playerInput.enabled = false; //플레이어 입력 비활성화
            AudioManager.Instance.Play(lockedSound); //잠긴 문 소리 재생
            ShowActionUI(); //액션 UI 표시
            yield return new WaitForSeconds(2f); //2초 대기
            playerInput.enabled = true; //플레이어 입력 활성화
            action = originalMessage; //원래 메시지로 복원
        }

        IEnumerator OpenedDoor() {
            playerInput.enabled = false; //플레이어 입력 비활성화
            action = unlockedMessage; //잠금 해제 메시지 표시
            ShowActionUI(); //액션 UI 표시
            GetComponent<BoxCollider>().enabled = false; //트리거 비활성화
            door.GetComponent<BoxCollider>().enabled = false; //문 충돌체 비활성화
            yield return new WaitForSeconds(2f); //1초 대기
            animator.SetBool(animParam, true); //문 애니메이션 실행
            AudioManager.Instance.Play("DoorOpen"); //문 열리는 소리 재생
            playerInput.enabled = true; //플레이어 입력 활성화
            action = originalMessage; //원래 메시지로 복원
        }
        #endregion
    }

}

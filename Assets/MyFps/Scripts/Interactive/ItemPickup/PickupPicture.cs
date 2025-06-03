using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyFps {
    public class PickupPicture : Interactive {
        #region Variables
        [SerializeField] KeyType key;
        [SerializeField] GameObject pictureObject; //오브젝트 비활성화용
        float waitTime = 2f; //대기 시간 
        //별도의 UI로 획득을 표시
        [SerializeField] GameObject gotItemUI; //획득 메시지 표시용 UI
        [SerializeField] TextMeshProUGUI gotItemText; //획득 메시지 표시용 텍스트
        //이미지들
        [SerializeField] Image uiImage; //그림이 걸릴 이미지
        [SerializeField] Sprite leftHalfPicture; //왼쪽 반쪽 그림
        [SerializeField] Sprite rightHalfPicture; //오른쪽 반쪽 그림
        //플레이어
        [SerializeField] PlayerController playerController; //플레이어 컨트롤러 (필요시 사용)
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        #endregion

        #region Custom Method
        protected override void DoAction() {
            StartCoroutine(Action());
        }

        IEnumerator Action() {
            //콜라이더 제거
            GetComponent<Collider>().enabled = false;
            //열쇠 획득
            PlayerManager.Instance.GetKey(key);
            //오브젝트 비활성화
            pictureObject.SetActive(false);
            //UI를 활성화 하여 획득 표시
            //그림과 대사 설정
            if (key.Equals(KeyType.KEY_LEFTPIC)) {
                uiImage.sprite = leftHalfPicture;
                gotItemText.text = "Left Picture Piece Acquired!";
            }
            else if (key.Equals(KeyType.KEY_RIGHTPIC)) {
                uiImage.sprite = rightHalfPicture;
                gotItemText.text = "Right Picture Piece Acquired!";
            }
            gotItemUI.SetActive(true);
            gotItemText.gameObject.SetActive(true);

            //UI 활성화 된 동안 대기 + 입력 비활성화
            playerController.enabled = false; //플레이어 컨트롤러 비활성화
            //Debug.Log("Wait for some seconds...");
            yield return new WaitForSeconds(waitTime);
            //Debug.Log("Wait finished.");
            playerController.enabled = true; //플레이어 컨트롤러 활성화
            //UI 비활성화
            gotItemUI.SetActive(false);
            gotItemText.gameObject.SetActive(false);
            HideActionUI(); //액션 UI 숨기기
            Destroy(gameObject, 1f); //오브젝트 제거
            //Debug.Log(PlayerManager.Instance.HasKey(KeyType.KEY_LEFTPIC));
            //Debug.Log(PlayerManager.Instance.HasKey(KeyType.KEY_RIGHTPIC));
        }
        #endregion
    }

}
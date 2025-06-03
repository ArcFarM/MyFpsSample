using UnityEngine;

namespace MyFps {
    public class ExitDoorCheck : Interactive {
        #region Variables
        //상호작용할 대상
        [SerializeField] GameObject itemFrame;
        [SerializeField] GameObject fullPicture;
        //열어줄 문
        [SerializeField] GameObject exitDoor;
        [SerializeField] GameObject secretRoomDoor;
        //두 개의 조각
        bool lKey, rKey;
        //마우스 갖다 댈 시 출력할 문구
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        #endregion

        #region Custom Method
        protected override void DoAction() {
            //두 개의 조각을 모두 가지고 있다면
            lKey = PlayerManager.Instance.HasKey(KeyType.KEY_LEFTPIC);
            rKey = PlayerManager.Instance.HasKey(KeyType.KEY_RIGHTPIC);

            if(lKey && rKey) {
                //액션 UI 숨기기
                HideActionUI();
                //아이템 프레임 비활성화
                itemFrame.SetActive(false);
                //전체 그림 활성화
                fullPicture.SetActive(true);
                //문 열기
                StartCoroutine(secretRoomDoor.GetComponent<HiddenDoor>().DoCheck());
                StartCoroutine(exitDoor.GetComponent<HiddenDoor>().DoCheck());
            }
            else {
                //두 조각이 모두 있어야 문을 열 수 있다
                action = "You need both picture pieces to open this door!";
                HideActionUI();
            }
        }
        #endregion
    }

}

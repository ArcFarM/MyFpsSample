using MyFps;
using UnityEngine;
using System.Collections;

namespace MyFps {
    public class FS2Room2 : MonoBehaviour {
        #region Variables
        //날아갈 물체
        [SerializeField] GameObject flyingObject; //날아갈 물체를 위해 활성화할 GameObject
        [SerializeField] PlayerController pc; //플레이어 컨트롤러
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter(Collider other) {
            StartCoroutine(SequencePlay(other.gameObject)); //트리거에 들어오면 SequencePlay 코루틴 실행
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlay(GameObject go) {
            if (go.CompareTag("Player")) {
                pc.enabled = false; //플레이어 컨트롤러 비활성화
                                    //재 활성화 방지
                GetComponent<BoxCollider>().enabled = false; //트리거 비활성화
                flyingObject.SetActive(true); //플레이어가 트리거에 들어오면 날아갈 물체 활성화
                yield return new WaitForSeconds(1f); //1초 대기
                flyingObject.SetActive(false); //물체 날아갔으니 비활성화
                pc.enabled = true; //플레이어 컨트롤러 재활성화
            }
            #endregion
            else yield break;
        }
    }
}

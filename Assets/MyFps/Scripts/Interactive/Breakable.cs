using System.Collections;
using UnityEngine;

namespace MyFps {
    public class Breakable : MonoBehaviour, IDamagable {
        #region Variables
        [SerializeField] GameObject baseObject;
        [SerializeField] GameObject brokenObject;
        //연출용 오브젝트
        public GameObject fragment;
        //파손시 생성될 아이템(있다면)
        [SerializeField] GameObject dropItem;
        float yOffset = 0.5f;
        //파괴를 위한 변수들
        public float health = 1f;
        bool isDead = false; //파괴 여부
        public string soundName = "PotterySmash";
        [SerializeField] bool unbreakable = false;

        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        #endregion

        #region Custom Method
        public void TakeDamage(float damage) {
            health -= damage; //데미지를 입음
            //Debug.Log($"항아리 체력 : {health}");
            if (health <= 0) {
                Die();
            }
        }

        void Die() {
            StartCoroutine(Break()); //파괴 코루틴 시작
        }

        IEnumerator Break() {
            if (isDead) yield break;
            else isDead = true; //파괴 상태로 변경
            AudioManager.Instance.Play(soundName);
            gameObject.GetComponent<BoxCollider>().enabled = false; //충돌체 비활성화
            if (baseObject.activeSelf) {
                //기본 오브젝트가 활성화되어 있다면
                baseObject.SetActive(false); //기본 오브젝트 비활성화
                brokenObject.SetActive(true); //파손된 오브젝트 활성화
                if (fragment != null) {
                    //파편 오브젝트가 있다면
                    fragment.SetActive(true); //파편 오브젝트 활성화
                    yield return new WaitForSeconds(0.05f);
                    fragment.SetActive(false); //파편 오브젝트 활성화
                }
            }
            if(dropItem != null) {
                Vector3 dropPos = transform.position + Vector3.up * yOffset; //아이템 드롭 위치 설정
                dropItem.SetActive(true); //아이템 활성화
                dropItem.transform.position = dropPos; //아이템 위치 설정
            }
        }
        #endregion
    }

}

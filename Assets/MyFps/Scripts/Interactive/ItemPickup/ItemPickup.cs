using UnityEngine;

namespace MyFps {
    public class ItemPickup : MonoBehaviour {
        #region Variables
        public int amount = 8;
        protected float y_Pos = 0f;
        protected float init_y_Pos; //아이템의 초기 y축 위치
        protected float sin_val = 0f;
        public float sin_speed = 2f; //사인파 속도 조절 변수
        public float sin_scale = 0.3f; //사인파의 크기 조절 변수

        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        protected virtual void Start() {
            init_y_Pos = transform.position.y; //아이템의 초기 y축 위치 저장
        }
        protected virtual void Update() {
            //제자리에서 상승하강 이동
            sin_val += Time.deltaTime * sin_speed; //사인파 값 업데이트
            y_Pos = Mathf.Sin(sin_val) * sin_scale;
            transform.position = new Vector3(transform.position.x, init_y_Pos + y_Pos, transform.position.z); //y축 위치 업데이트
            //좌우 회전
            transform.Rotate(Vector3.up, 60f * Time.deltaTime, Space.World); //좌우 회전
        }

        protected virtual void OnTriggerEnter(Collider other) {
            //플레이어가 아이템을 획득했을 때
            if (other.CompareTag("Player")) {
                //아이템 획득
                gameObject.SetActive(false);
                Destroy(gameObject, 1f); //1초 후에 아이템 오브젝트 삭제
            }
        }
        #endregion

        #region Custom Method
        protected virtual bool CheckCondition() {
            //아이템 획득 조건을 확인하는 메소드
            return true;
        }
        
        #endregion
    }

}

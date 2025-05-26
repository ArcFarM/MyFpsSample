using UnityEngine;

namespace MyFps {
    public class GetAmmo : Interactive {
        #region Variables
        //획득할 탄약 수량
        public int ammoAmount = 8;
        #endregion
        #region Unity Event Method
        private void Start() {
            this.gameObject.SetActive(false); //시작 시 오브젝트 비활성화
        }
        #endregion
        #region Custom Method
        protected override void DoAction() {
            PlayerManager.Instance.AddAmmo(ammoAmount); //플레이어에게 탄약 추가

            //효과 출력

            //오브젝트 비활성화 후 삭제
            this.gameObject.SetActive(false); //오브젝트 비활성화
            Destroy(this.gameObject, 3f); //오브젝트 삭제
        }
        #endregion
    }

}

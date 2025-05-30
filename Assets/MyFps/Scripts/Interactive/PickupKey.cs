using UnityEngine;

namespace MyFps {
    public class PickupKey : Interactive {
        #region Variables
        [SerializeField] private KeyType keyType; //획득할 키 타입
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        #endregion

        #region Custom Method
        protected override void DoAction() {
            PlayerManager.Instance.GetKey(keyType); //플레이어에게 키 추가
            Destroy(gameObject); //키 오브젝트 제거
        }
        #endregion
    }

}

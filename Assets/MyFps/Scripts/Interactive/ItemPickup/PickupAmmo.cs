using UnityEngine;

namespace MyFps {
    public class PickupAmmo : ItemPickup {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Unity Event Method
        protected override void OnTriggerEnter(Collider other) {
            //플레이어가 아이템을 획득했을 때
            if (other.CompareTag("Player") && CheckCondition()) {
                //아이템 획득
                PlayerManager.Instance.AddAmmo(amount); //플레이어의 총알 수 증가
                gameObject.SetActive(false);
                Destroy(gameObject, 1f); //1초 후에 아이템 오브젝트 삭제
            }
        }
        #endregion

        #region Custom Method
        protected override bool CheckCondition() {
            //아이템 획득 조건을 확인하는 메소드
            //무기가 없으면 습득 불가
            if (PlayerManager.CurrentWeapon == WeaponType.None) {
                //플레이어가 무기를 가지고 있지 않은 경우 아이템 획득 불가
                return false;
            }
            /*if (PlayerManager.Instance.Ammo >= PlayerManager.Instance.MaxAmmo) {
                //플레이어의 총알이 최대치인 경우 아이템 획득 불가
                현재 플레이어 최대치 제한 없음
                return false;
            }*/
            return true;
        }
        
        #endregion
    }

}

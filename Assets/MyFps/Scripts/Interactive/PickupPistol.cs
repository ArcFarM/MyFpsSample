using TMPro;
using UnityEngine;

namespace MyFps
{
    //아이템(권총) 획득 인터랙티브 구현
    public class PickupPistol : Interactive
    {
        #region Variables        
        //인터랙티브 액션 연출
        public GameObject realPistol;
        public GameObject theArrow;
        public ShowAmmo ammoUI;
        //무기 집기 전에는 적 조우 트리거 비활성화 및 탄환과 상호작용 불가
        public GameObject enemyTrigger;
        public GameObject ammoTrigger;
        #endregion

        #region Custom Method  
        protected override void DoAction()
        {
            //무기획득, 충돌체 제거
            realPistol.SetActive(true);
            theArrow.SetActive(false);
            //무기 유형 설정
            PlayerManager.CurrentWeapon = WeaponType.Pistol;
            //무기 UI 활성화
            ammoUI.EnableAmmoDisplay();
            //트리거 활성화
            enemyTrigger.SetActive(true);
            ammoTrigger.SetActive(true);

            this.gameObject.SetActive(false);   //fake pistol 및 충돌체 제거                    
        }
        #endregion
    }
}

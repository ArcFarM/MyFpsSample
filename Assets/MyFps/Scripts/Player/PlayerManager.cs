using UnityEngine;

namespace MyFps {
    public enum WeaponType {
        None,
        Pistol,
    }

    public class PlayerManager : PersistanceSingleton <PlayerManager> {
        #region Variables
        //플레이어의 현재 무기
        private WeaponType currentWeapon;
        //플레이어의 현재 탄환
        int currentAmmo = 0;
        #endregion

        #region Properties
        //현재 무기 속성
        public static WeaponType CurrentWeapon {
            get; set;
        }
        public int Ammo {
            get { return currentAmmo; }
            set { currentAmmo = value; }
        }
        #endregion

        #region Unity Event Methods
        private void Start() {
            currentWeapon = WeaponType.None;
        }
        #endregion

        #region Custom Methods
        public bool IsAmmoEmpty(){
            return currentAmmo <= 0;
        }

        public void AddAmmo(int amount) {
            currentAmmo += amount;
        }

        public void UseAmmo(int amount) {
            if (currentAmmo >= amount) {
                currentAmmo -= amount;
            }
        }
        #endregion

    }

}

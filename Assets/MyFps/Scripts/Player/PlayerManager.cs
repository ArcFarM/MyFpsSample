using UnityEngine;

namespace MyFps {
    public enum WeaponType {
        None,
        Pistol,
    }

    public enum KeyType {
        KEY_ROOM2,
        MAX_KEY //최대 키 개수
    }

    public class PlayerManager : PersistanceSingleton <PlayerManager> {
        #region Variables
        //플레이어의 현재 무기
        private WeaponType currentWeapon;
        //열쇠 보유 현황
        bool[] havingKeys;
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
            havingKeys = new bool[(int)KeyType.MAX_KEY];
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

        public void GetKey(KeyType key) {
            havingKeys[(int)key] = true;
        }

        public bool HasKey(KeyType key) {
            havingKeys[(int)key] = true;
            return false;
        }
        #endregion

    }

}

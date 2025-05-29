using TMPro;
using UnityEngine;

namespace MyFps {
    public class ShowAmmo : MonoBehaviour {
        #region Variables
        //탄환 표시할 텍스트
        public TextMeshProUGUI ammoText;
        #endregion

        #region Properties
        #endregion

        #region Unity Event Methods
        private void Start() {
            if(PlayerManager.CurrentWeapon == WeaponType.None) DisableAmmoDisplay(); // 시작 시 탄환 표시 비활성화
        }
        // Update is called once per frame
        void Update() {
            ammoText.text = PlayerManager.Instance.Ammo.ToString();
        }
        #endregion
        #region Custom Methods
        // 현재 무기가 없을 때 탄환 표시를 비활성화
        public void DisableAmmoDisplay() {
            gameObject.SetActive(false);
        }
        // 현재 무기가 있을 때 탄환 표시를 활성화
        public void EnableAmmoDisplay() {
            gameObject.SetActive(true);
        }
        #endregion
    }

}

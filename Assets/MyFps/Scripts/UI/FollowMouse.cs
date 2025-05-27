using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    #region Variables
    //마우스가 가리키는 좌표 값
    private Vector3 mousePos;
    #endregion

    #region Properties
    #endregion

    #region Unity Event Method
    private void Update() {
        mousePos = GetPos(); //마우스 위치를 가져옴
        transform.LookAt(mousePos);
    }
    #endregion


    #region Custom Method
    //raycast를 이용하여 현재 마우스 포인터의 월드 좌표를 가져오기
    public Vector3 GetPos() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 위치를 월드 좌표로 변환
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            return hit.point; //충돌한 지점의 월드 좌표 반환
        }
        return Vector3.zero; //충돌하지 않은 경우 (0,0,0) 반환
    }
    //ScreenToworldPoint를 이용하여 현재 마우스 포인터의 월드 좌표를 가져오기
    /*public Vector3 GetPos() {
        //마우스 위치를 월드 좌표로 변환
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = (transform.position.z + Camera.main.nearClipPlane) / 2;
        return Camera.main.ScreenToWorldPoint(mousePos); //월드 좌표로 변환하여 반환
    }*/
    #endregion
}

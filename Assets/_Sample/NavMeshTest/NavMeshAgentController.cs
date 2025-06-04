using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    #region Variables
    NavMeshAgent agent;
    Vector3 targetPos;
    #endregion

    #region Properties
    #endregion

    #region Unity Event Method
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        targetPos = Vector3.zero;
    }

    private void Update() {
        //우클릭하면 목표 지점으로 설정
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                targetPos = hit.point;
            }
            agent.SetDestination(targetPos);
        }
    }
    #endregion

    #region Custom Method
    #endregion
}

using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public GameObject myPiece;
    Vector3 myPiecePos;
      // Update is called once per frame
    void Update()
    {
        myPiecePos = myPiece.transform.position;
        transform.LookAt(myPiecePos);
        transform.position = myPiecePos + 10.0f * Vector3.up - 10.0f * Vector3.forward;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("ray.origin " + ray.origin + " ray.direction " + ray.direction);
            Debug.DrawRay(ray.origin,ray.direction,Color.red);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                myPiece.transform.position = hit.point;
            }
        }
    }
}
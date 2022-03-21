
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private bool isMove;
    private Vector3 direction;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float progress;
    public float step;

    public string startPlaceTag;
    public string invisiblePlatformTag;
    public string buidingsGridTag;
    private BuildingsGrid buildingsGrid;
    public Vector2Int playerPosition;

    private void Start()
    {
        
        isMove = false;
        SetStartPosition();
        Physics.IgnoreCollision(this.GetComponent<Collider>(), GameObject.FindWithTag(invisiblePlatformTag).gameObject.GetComponent<Collider>());
        buildingsGrid = GameObject.FindGameObjectWithTag(buidingsGridTag).GetComponent<BuildingsGrid>();
    }

    private void Update()
    {
        PlayerMovement();
        playerPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        CheckPlatform();
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            progress += step;
            if (transform.position == endPosition)
            {
                isMove = false;
                progress = 0;
            }
        }
    }

    private void SetStartPosition()
    {
        transform.position = GameObject.FindGameObjectWithTag(startPlaceTag).gameObject.transform.position + new Vector3(0f, 0.5f, 0f);
    }

    private void PlayerMovement()
    {
        if (isMove)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) && buildingsGrid.CheakPlaceToMove(playerPosition + Vector2Int.left))
        {
            Move(Vector2Int.left);
        }
        if (Input.GetKeyDown(KeyCode.S) && buildingsGrid.CheakPlaceToMove(playerPosition + Vector2Int.right))
        {
            Move(Vector2Int.right);
        }
        if (Input.GetKeyDown(KeyCode.D) && buildingsGrid.CheakPlaceToMove(playerPosition + Vector2Int.up))
        {
            Move(Vector2Int.up);
        }
        if (Input.GetKeyDown(KeyCode.A) && buildingsGrid.CheakPlaceToMove(playerPosition + Vector2Int.down))
        {
            Move(Vector2Int.down);
        }
    }
    private void Move(Vector2Int direct)
    {
        direction = new Vector3(direct.x, 0, direct.y);
        startPosition = transform.position;
        endPosition = startPosition + direction;
        isMove = true;
    }

    private void CheckPlatform()
    {
        Ray ray = new Ray(transform.position,Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<Building>())
            {
                Building building = hit.collider.gameObject.GetComponent<Building>();
                building.Action();
            }
        }
    }
}

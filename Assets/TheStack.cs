using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour {

    private GameObject[] theStack;
    private int stackIndex;
    private int scoreCount = 0;
    private float tileTransition = 1.0f;
    private float tileSpeed = 2.5f;
    private bool isMovingOnX = true;
    private float alignPostiion;
    private Vector3 fixCamera;
    private Vector3 lastPosition;
    private float errorMargin = 0.1f;
    private int combo;
    private Vector2 stackBounds = new Vector2(3, 3);
    bool gameOver = false;
	// Use this for initialization
	void Start () {
        theStack = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            theStack[i] = transform.GetChild(i).gameObject;
        }
        stackIndex = transform.childCount - 1;
    }

	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            if (PlaceTile())
            {
                SpawnTile();
                scoreCount++;
            }
            else
            {
                EndGame();
            }
        }

        MoveTile();
        transform.position = Vector3.Lerp(fixCamera, transform.position, 5.0f * Time.deltaTime);
	}


    private void MoveTile()
    {
        if (gameOver)
            return;

        tileTransition += Time.deltaTime * tileSpeed;
        if(isMovingOnX)
        theStack[stackIndex].transform.localPosition = new Vector3(Mathf.Sin(tileTransition * 3.5f), scoreCount, alignPostiion);
        else
            theStack[stackIndex].transform.localPosition = new Vector3(alignPostiion, scoreCount, Mathf.Sin(tileTransition * 3.5f));
    }

    private void SpawnTile()
    {
        lastPosition = theStack[stackIndex].transform.localPosition;
        stackIndex--;
        if (stackIndex < 0)
            stackIndex = transform.childCount - 1;

        fixCamera = Vector3.down * scoreCount;
        theStack[stackIndex].transform.localPosition = new Vector3(0, scoreCount, 0);
    }
    private bool PlaceTile()
    {
        if (isMovingOnX)
        {
            float delta = lastPosition.x - theStack[stackIndex].transform.localPosition.x;
            if (Mathf.Abs(delta) > errorMargin)
            {
                combo = 0;
                stackBounds.x -= Mathf.Abs(delta);
                if (stackBounds.x <= 0)
                    return false;

                float middle = (lastPosition.x + theStack[stackIndex].transform.localPosition.x) / 2;
                var transform = theStack[stackIndex].transform;
                transform.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                transform.localPosition = new Vector3(middle - (lastPosition.x / 2), scoreCount, lastPosition.z);
            }
        }
        else
        {
            float delta = lastPosition.z - theStack[stackIndex].transform.localPosition.z;
            if (Mathf.Abs(delta) > errorMargin)
            {
                combo = 0;
                stackBounds.y -= Mathf.Abs(delta);
                if (stackBounds.x <= 0)
                    return false;

                float middle = (lastPosition.z + theStack[stackIndex].transform.localPosition.z) / 2;
                var transform = theStack[stackIndex].transform;
                transform.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
                transform.localPosition = new Vector3(middle - (lastPosition.x / 2), scoreCount, middle - (lastPosition.z / 2));
            }
        }
        if (isMovingOnX)
            alignPostiion = theStack[stackIndex].transform.localPosition.x;
        else
            alignPostiion = theStack[stackIndex].transform.localPosition.z;
        isMovingOnX = !isMovingOnX;
        return true;
    }

    private void EndGame()
    {
        gameOver = true;
        theStack[stackIndex].AddComponent<Rigidbody>();
    }
}

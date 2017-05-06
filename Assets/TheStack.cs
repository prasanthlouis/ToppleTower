using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour {

    private GameObject[] theStack;
    private int stackIndex;
    private int scoreCount = 0;
    private float tileTransition = 0.0f;
    private float tileSpeed = 3.5f;
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

	}


    private void MoveTile()
    {
        tileTransition += Time.deltaTime * tileSpeed;
        theStack[stackIndex].transform.localPosition = new Vector3(Mathf.Sin(tileTransition * 3), scoreCount, 0);
    }

    private void SpawnTile()
    {
        stackIndex--;
        if (stackIndex < 0)
            stackIndex = transform.childCount - 1;

        theStack[stackIndex].transform.localPosition = new Vector3(0, scoreCount, 0);
    }
    private bool PlaceTile()
    {
        return true;
    }

    private void EndGame()
    {

    }
}

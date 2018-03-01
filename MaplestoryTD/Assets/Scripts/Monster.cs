using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Stack<Node> path;

    public Point GridPosition { get; set; }

    private Vector3 destination;

    public bool isActive { get; set; }

    private void Update()
    {
        Move();
    }

    public void Spawn()
    {
        transform.position = LevelManager.Instance.StartPortal.transform.position;

        //scale monsters as they come out of portal
        StartCoroutine(Scale(new Vector3(0.1f,0.1f), new Vector3(1,1)));

        SetPath(LevelManager.Instance.Path);
        
    }

    //scale monster
    public IEnumerator Scale(Vector3 from, Vector3 to)
    {
        isActive = false;

        float progress = 0;
        
        while(progress <= 1)
        {
            //linear interpolation of scale
            transform.localScale = Vector3.Lerp(from, to, progress);
            progress += Time.deltaTime;

            yield return null;
        }
        transform.localScale = to;

        isActive = true;
    }

    //move the monster
    private void Move()
    {
        if(isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().worldPosition;
                }
            }
        }
    }

    private void SetPath(Stack<Node> newPath)
    {
        if(newPath != null)
        {
            this.path = newPath;

            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().worldPosition;
        }
    }
}

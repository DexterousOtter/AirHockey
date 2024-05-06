using TMPro;
using UnityEngine;
using System.Collections;
public class PortalSpawner : MonoBehaviour
{
    public GameObject portalPrefab;
    public float minY;
    public float maxY;
    public float minX;
    public float maxX;

    private GameObject portal1;
    private GameObject portal2;
    void Start()
    {
        InvokeRepeating("SpawnPortals", 10f, 20f);
    }
    enum BoardSide
    {
        left,
        right
    }
    void SpawnPortals()
    {
        // Spawn two portals
        portal1 = Instantiate(portalPrefab, RandomPosition(BoardSide.left), Quaternion.identity);
        portal2 = Instantiate(portalPrefab, RandomPosition(BoardSide.right), Quaternion.identity);

        // Set each portal to point to the other
        portal1.GetComponent<Portal>().linkedPortal = portal2;
        portal2.GetComponent<Portal>().linkedPortal = portal1;
        StartCoroutine(DestroyPortalsAfterSeconds(15));
    }

    Vector2 RandomPosition(BoardSide boardSide)
    {
        // Randomly choose a side of the board (0 = top, 1 = right, 2 = bottom, 3 = left)
        int side = Random.Range(0, 4);

        Vector2 position = Vector2.zero;

        float maxXForBoardSide, minXForBoardSide;

        if (boardSide == BoardSide.left)
        {
            maxXForBoardSide = -0.2f;
            minXForBoardSide = minX;
        }
        else
        {
            maxXForBoardSide = maxX;
            minXForBoardSide = 0.2f;
        }

        switch (side)
        {
            case 0: // Top
                position = new Vector2(Random.Range(minXForBoardSide, maxXForBoardSide), maxY);
                break;
            case 1: // Right
                position = new Vector2(maxXForBoardSide, Random.Range(minY, maxY));
                break;
            case 2: // Bottom
                position = new Vector2(Random.Range(minXForBoardSide, maxXForBoardSide), minY);
                break;
            case 3: // Left
                position = new Vector2(minXForBoardSide, Random.Range(minY, maxY));
                break;
        }

        return position;
    }
    IEnumerator DestroyPortalsAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (portal1 != null)
        {
            Destroy(portal1);
        }
        if (portal2 != null)
        {
            Destroy(portal2);
        }
    }
}
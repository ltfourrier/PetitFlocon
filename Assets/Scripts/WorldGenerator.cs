using UnityEngine;
using System.Collections.Generic;

public class Vector2i {
    public int x { get; set; }
    public int y { get; set; }

    public static Vector2i FromVector2(Vector2 v) {
        Vector2i rtn = new Vector2i();
        rtn.x = Mathf.FloorToInt(v.x);
        rtn.y = Mathf.FloorToInt(v.y);
        return rtn;
    }

    public override bool Equals(object obj) {
        if (obj == null) {
            return false;
        }

        Vector2i v = obj as Vector2i;
        if ((object)v == null) {
            return false;
        }

        return (v.x == this.x && v.y == this.y);
    }

    public bool Equals(Vector2i v) {
        return (v.x == this.x && v.y == this.y);
    }

    public override int GetHashCode() {
        return x ^ y;
    }

    public override string ToString() {
        return x.ToString() + ';' + y.ToString();
    }
}

public class WorldGenerator : MonoBehaviour {

    public GameObject terrainRenderer;
    public GameObject pineTree;

    public Camera mainCamera;

    public int chunkWidth;
    public int chunkHeight;

    private Dictionary<Vector2i, GameObject> chunks;

	// Use this for initialization
	void Start () {
        chunks = new Dictionary<Vector2i, GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        // Compute the scale needed to go from pixels to chunk position.
        Vector3 pixelToChunkScale = new Vector3(1.0f / 8.0f / chunkWidth, 1.0f / 8.0f /chunkHeight);

        // Check for the four corners of the Camera
        for (float x = 0.0f; x <= 1.0f; x += 1.0f) {
            for (float y = 0.0f; y <= 1.0f; y += 1.0f) {
                Vector3 position = mainCamera.ViewportToWorldPoint(new Vector3(x, y));
                position -= this.transform.position;
                position.Scale(pixelToChunkScale);
                if (!chunks.ContainsKey(Vector2i.FromVector2(position))) {
                    this.GenerateChunk(Vector2i.FromVector2(position));
                }
            }
        }
    }

    void GenerateChunk (Vector2i chunkPosition) {
        Debug.Log("Instantiated chunk at " + chunkPosition.ToString());

        // Like the update method, we need a chunk to pixels scaling vector.
        Vector3 chunkToPixelScale = new Vector3(chunkWidth * 8.0f, chunkHeight * 8.0f);

        // Before we scale, calculate the maximum resource number
        Vector3 position = new Vector3(chunkPosition.x, chunkPosition.y);
        float maxResources = 20.0f / position.magnitude;

        // Scale to pixels and add the chunk
        position.Scale(chunkToPixelScale);
        GameObject chunk = Instantiate(terrainRenderer);
        chunk.transform.position = position + this.transform.position;
        chunks.Add(chunkPosition, chunk);

        // Now that the chunk is added, we adjust the max ressources and generate them
        if (maxResources >= 10.0f) {
            maxResources = 10.0f;
        } else if (maxResources <= 1.0f) {
            maxResources = 1.0f;
        }

        // Convert the pixel position to a tile position
        position.Scale(new Vector3(1.0f / 8.0f, 1.0f / 8.0f));

        int treeCount = Random.Range(0, (int) maxResources);
        for (int it = 0; it < treeCount; it++) {
            Vector3 treePosition = new Vector3(
                Mathf.Floor(Random.Range(position.x, position.x + chunkWidth)),
                Mathf.Floor(Random.Range(position.y, position.y + chunkHeight)));
            Debug.Log("Instantiated tree at " + treePosition.ToString());
            treePosition.Scale(new Vector3(8.0f, 8.0f));
            GameObject tree = Instantiate(pineTree);
            tree.transform.position = treePosition;
        }
    }

}

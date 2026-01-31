using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public Vector2 scrollSpeed = new Vector2(0.5f, 0f);
    private Material material;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector2 offset = material.mainTextureOffset;
        offset += scrollSpeed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }
}

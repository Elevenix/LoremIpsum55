using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineWall : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float sizeOutline = 0.1f;
    [SerializeField] private SpriteRenderer spriteOutline;

    // Start is called before the first frame update
    void Awake()
    {
        spriteOutline.color = color;
        float scaleX = 1 + (1 / transform.localScale.x * sizeOutline);
        float scaleY = 1 + (1 / transform.localScale.y * sizeOutline);
        spriteOutline.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }
}

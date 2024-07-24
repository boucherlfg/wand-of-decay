using UnityEngine;

public class Decayable : MonoBehaviour 
{
    private float currentLife;
    [SerializeField]
    private float maxLife = 10;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private GameObject toTurnIntoWhenDead;

    private SpriteRenderer rend;
    private void Start()
    {
        currentLife = maxLife;
        rend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        var index = Mathf.Clamp((int)((currentLife / maxLife) * sprites.Length), 0, sprites.Length - 1);
        Debug.Log(currentLife);
        rend.sprite = sprites[index];
        if (currentLife <= 0)
        {
            if(toTurnIntoWhenDead) Instantiate(toTurnIntoWhenDead, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void ReceiveDamage(float multiplier)
    {
        currentLife -= multiplier * Time.deltaTime;
    }
}
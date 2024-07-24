using UnityEngine;

public class Decayable : MonoBehaviour 
{
    public DecayableType type;
    private float currentLife;
    [SerializeField]
    private float maxLife = 10;
    [SerializeField]
    private float regenRate = 0.15f;
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
        currentLife += Time.deltaTime * regenRate;
        var index = Mathf.Clamp((int)((currentLife / maxLife) * sprites.Length), 0, sprites.Length - 1);
        rend.sprite = sprites[index];
        if (currentLife <= 0)
        {
            if(toTurnIntoWhenDead) Instantiate(toTurnIntoWhenDead, transform.position, Quaternion.identity);
            ServiceManager.Instance.Get<Stats>()[type]++;
            Destroy(gameObject);
        }
    }

    public void ReceiveDamage(float multiplier)
    {
        currentLife -= multiplier * Time.deltaTime;
    }
}
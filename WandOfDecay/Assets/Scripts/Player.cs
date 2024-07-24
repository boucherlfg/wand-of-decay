using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform arm;
    [SerializeField]
    private float speed;

    [Header("Ray")]
    [SerializeField]
    private LineRenderer rayLine;
    [SerializeField]
    private ParticleSystem rayImpact;
    [SerializeField]
    private Transform rayStart;
    [SerializeField]
    private float rayDamage = 5f;

    [Header("AoE")]
    [SerializeField]
    private GameObject aoeCirclePrefab;
    [SerializeField]
    private GameObject aoeHitEffect;
    [SerializeField]
    private float aoeRange = 3f;
    [SerializeField]
    private float aoeCooldown = 3f;
    [SerializeField]
    private float aoeDamage = 0.5f;

    private Vector2 mouseDelta;
    private Vector2 mousePosition;
    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
        HandleMouseDirection();
        HandleLeftClick();
        HandleRightClick();
    }

    void HandleMove()
    {
        var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void HandleMouseDirection()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseDelta = (mousePosition - (Vector2)transform.position).normalized;
        var mouseDirection = Mathf.Atan2(mouseDelta.y, mouseDelta.x);
        arm.transform.rotation = Quaternion.Euler(0, 0, mouseDirection * Mathf.Rad2Deg);
    }

    void HandleLeftClick()
    {


        if (!Input.GetMouseButton(0))
        {
            rayImpact.gameObject.SetActive(false);
            rayLine.gameObject.SetActive(false);
            return;
        }

        var endPos = (Vector2)rayStart.position + mouseDelta * (mousePosition - (Vector2)rayStart.position).magnitude;

        var hit = Physics2D.RaycastAll(rayStart.position, mouseDelta, 10)
                           .Where(x => x.collider.gameObject != gameObject && x.collider.GetComponent<Decayable>())
                           .OrderBy(x => Vector2.Distance(x.transform.position, rayStart.position))
                           .FirstOrDefault();

        if (hit)
        {
            endPos = hit.point;
            hit.collider.GetComponent<Decayable>().ReceiveDamage(rayDamage);
        }

        rayLine.gameObject.SetActive(true);
        rayLine.SetPositions(new Vector3[] { Vector3.zero, Vector3.right * (endPos - (Vector2)rayStart.position).magnitude });
        rayImpact.gameObject.SetActive(true);
        rayImpact.transform.position = endPos;
    }

    void HandleRightClick()
    {
        if (Input.GetMouseButtonUp(1))
        {

            var hits = Physics2D.OverlapCircleAll(transform.position, aoeRange)
                               .Where(x => x.gameObject != gameObject && x.GetComponent<Decayable>());

            var aoe = Instantiate(aoeCirclePrefab, transform.position, Quaternion.identity).GetComponent<AoeCirlcle>();
            aoe.radius = aoeRange;

            foreach (var hit in hits)
            {
                StartCoroutine(ReceiveDamage(hit.GetComponent<Decayable>()));
                Instantiate(aoeHitEffect, hit.transform.position, Quaternion.identity);
            }

            IEnumerator ReceiveDamage(Decayable decayable)
            {
                for (float i = 0; i < 1; i += Time.deltaTime)
                {
                    decayable.ReceiveDamage(aoeDamage);
                    yield return null;
                }
            }
        }
    }
}

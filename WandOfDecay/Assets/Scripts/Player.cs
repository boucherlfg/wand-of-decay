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
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject endMenu;

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
    private float aoeCounter = 0;
    [SerializeField]
    private GameObject aoeIndicator;

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
        if (startMenu.activeSelf || endMenu.activeSelf) return;

        HandleMove();
        HandleMouseDirection();
        HandleLeftClick();
        HandleRightClick();
        HandlePause();
    }

    void HandlePause()
    {
        if (!Input.GetKeyUp(KeyCode.Escape)) return;

        pauseMenu.SetActive(!pauseMenu.activeSelf);
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
        var rayActive = false;
        var impactActive = false;
        if (!Input.GetMouseButton(0))
        {
            rayImpact.gameObject.SetActive(impactActive);
            rayLine.gameObject.SetActive(rayActive);
            return;
        }

        rayActive = true;
        var distance = (mousePosition - (Vector2)rayStart.position).magnitude;
        var endPos = (Vector2)rayStart.position + mouseDelta * distance;

        var hit = Physics2D.RaycastAll(rayStart.position, mouseDelta, distance)
                           .Where(x => x.collider.gameObject != gameObject && x.collider.GetComponent<Decayable>())
                           .OrderBy(x => Vector2.Distance(x.transform.position, rayStart.position))
                           .FirstOrDefault();

        if (hit)
        {
            endPos = hit.point;
            hit.collider.GetComponent<Decayable>().ReceiveDamage(rayDamage);
            impactActive = true;
        }

        rayImpact.gameObject.SetActive(impactActive);
        rayImpact.transform.position = endPos;
        rayLine.gameObject.SetActive(rayActive);
        rayLine.SetPositions(new Vector3[] { Vector3.zero, Vector3.right * (endPos - (Vector2)rayStart.position).magnitude });
    }

    void HandleRightClick()
    {
        aoeIndicator.SetActive(aoeCounter > 0);
        aoeCounter -= Time.deltaTime;
        if (aoeCounter > 0) return;
        if (!Input.GetMouseButtonUp(1)) return;
        aoeCounter = aoeCooldown;

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

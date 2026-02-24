using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float gravity = -25f;

    [Header("Look")]
    [SerializeField] float lookSensitivity = 300f;
    [SerializeField] Transform cameraTransform;

    [Header("Spawn & Safety")]
    [SerializeField] bool spawnNearClerkAtStart = true;
    [SerializeField] float clerkSpawnDistance = 4.2f;
    [SerializeField] float relocateDistanceThreshold = 0.35f;
    [Tooltip("0 = same Y level as Clerk. Negative is lower, positive is higher.")]
    [SerializeField] float clerkHeightOffset = 0f;

    [SerializeField] bool autoCreateFallbackGround = true;
    [SerializeField] float initialSnapProbeDistance = 200f;
    [SerializeField] float minimumAllowedY = -30f;
    [SerializeField] float fallbackGroundY = 0f;

    [SerializeField] float maxReasonableCameraOffset = 3f;
    [SerializeField] Vector3 defaultCameraLocalPosition = new Vector3(0f, 1.62f, 0f);

    CharacterController cc;
    float xRot, vVel;
    bool controlEnabled = true;
    Vector3 safePos;
    bool safeInit;

    public bool ControlEnabled => controlEnabled;
    public Transform CameraTransform => cameraTransform;

    void Awake()
    {
        cc = GetComponent<CharacterController>() ?? gameObject.AddComponent<CharacterController>();
        cc.height = 1.8f; cc.center = new Vector3(0, 0.9f, 0); cc.radius = 0.3f;

        if (!cameraTransform)
        {
            var c = GetComponentInChildren<Camera>(true);
            if (c) cameraTransform = c.transform;
            else if (Camera.main) cameraTransform = Camera.main.transform;
        }

        FixCamLocal();
        (GetComponent<PlayerInteractor>() ?? gameObject.AddComponent<PlayerInteractor>()).SetCameraTransform(cameraTransform);
    }

    void Start()
    {
        EnsureGround();
        SpawnNearClerk();
        SnapToGround();
        safePos = transform.position; safeInit = true;
        LockCursor(true);
    }

    void Update()
    {
        if (!cameraTransform) return;

        if (controlEnabled) { Look(); Move(); }
        else GravityOnly();

        if (cc.isGrounded) { safePos = transform.position; safeInit = true; }
        if (transform.position.y < minimumAllowedY) RestoreSafe();
    }

    public void SetControlEnabled(bool enabled)
    {
        if (controlEnabled == enabled) return;
        controlEnabled = enabled;
        LockCursor(enabled);
    }

    void Look()
    {
        float mx = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
        xRot = Mathf.Clamp(xRot - my, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRot, 0, 0);
        transform.Rotate(Vector3.up * mx);
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical");
        Vector3 m = (transform.right * h + transform.forward * v) * moveSpeed;
        ApplyGravity(); m.y = vVel;
        cc.Move(m * Time.deltaTime);
    }

    void GravityOnly() { ApplyGravity(); cc.Move(Vector3.up * vVel * Time.deltaTime); }

    void ApplyGravity()
    {
        if (cc.isGrounded && vVel < 0) vVel = -2f;
        vVel += gravity * Time.deltaTime;
    }

    void SpawnNearClerk()
    {
        if (!spawnNearClerkAtStart) return;
        var clerk = FindObjectOfType<Clerk>(true);
        if (!clerk) return;

        Vector3 cp = clerk.transform.position;
        Vector3 a = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 b = new Vector3(cp.x, 0, cp.z);
        if (Vector3.Distance(a, b) <= Mathf.Clamp(relocateDistanceThreshold, 0.1f, 1f)) return;

        Vector3 dir = clerk.transform.forward.sqrMagnitude < 0.0001f ? Vector3.back : clerk.transform.forward;
        dir.y = 0; dir.Normalize();

        Vector3 tp = cp - dir * clerkSpawnDistance; tp.y = cp.y + clerkHeightOffset;

        cc.enabled = false;
        transform.position = tp;
        transform.LookAt(new Vector3(cp.x, tp.y, cp.z));
        cc.enabled = true;
    }

    void EnsureGround()
    {
        if (GroundHit(initialSnapProbeDistance, out _)) return;
        if (autoCreateFallbackGround) CreateFallbackGround();
    }

    void SnapToGround()
    {
        if (!GroundHit(initialSnapProbeDistance, out RaycastHit hit)) return;
        var p = transform.position; p.y = hit.point.y + 0.05f; transform.position = p;
        vVel = -2f;
    }

    bool GroundHit(float dist, out RaycastHit best)
    {
        best = default; float bestD = float.MaxValue;
        var origin = transform.position + Vector3.up * 0.5f;
        var hits = Physics.RaycastAll(origin, Vector3.down, dist, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < hits.Length; i++)
        {
            var h = hits[i];
            if (!h.collider) continue;
            var t = h.collider.transform;
            if (t == transform || t.IsChildOf(transform)) continue;
            if (h.normal.y < 0.45f) continue;
            if (h.distance < bestD) { bestD = h.distance; best = h; }
        }
        return bestD < float.MaxValue;
    }

    void CreateFallbackGround()
    {
        const string name = "RuntimeFallbackGround";
        if (GameObject.Find(name)) return;

        float y = fallbackGroundY;
        if (GroundHit(1000f, out RaycastHit hit)) y = hit.point.y;

        var g = GameObject.CreatePrimitive(PrimitiveType.Cube);
        g.name = name;
        g.transform.position = new Vector3(transform.position.x, y - 0.5f, transform.position.z);
        g.transform.localScale = new Vector3(200f, 1f, 200f);
        g.layer = 0;

        var r = g.GetComponent<Renderer>();
        if (r) r.enabled = false;
    }

    void RestoreSafe()
    {
        if (!safeInit) return;
        cc.enabled = false; transform.position = safePos; cc.enabled = true;
        vVel = -2f;
    }

    void FixCamLocal()
    {
        if (!cameraTransform || !cameraTransform.IsChildOf(transform)) return;
        var lp = cameraTransform.localPosition;
        if (lp.magnitude > maxReasonableCameraOffset || lp.z < -1f || lp.y < 0.2f)
            cameraTransform.localPosition = defaultCameraLocalPosition;
    }

    static void LockCursor(bool on)
    {
        Cursor.lockState = on ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !on;
    }
}
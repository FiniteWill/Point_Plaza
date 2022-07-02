using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform startingPosition = null;
    [SerializeField] private float horizontalOffset = 0.0f;
    [SerializeField] private float verticalOffset = 0.0f;
    [SerializeField] private int zDepth = -10;
    [SerializeField] [Min(0.01f)] private float smoothingFactor = 1.0f;

    private void Awake()
    {
        Assert.IsNotNull(cam, $"{this.name} does not have a {nameof(cam)} but requires one.");
        SceneManager.sceneLoaded += SetPlayerDe;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
        if (isDebugging)
        { Debug.Log($"Is Moving, Target Pos: {player.transform.position} and Current Pos: {cam.transform.position}"); }
    }

    /// <summary>
    /// Function that moves the given GameObject to follow its target.
    /// </summary>
    private void Move()
    {
        if(player == null) { return; }
        Vector3 targetPos = Vector3.zero;
        targetPos = new Vector3(player.transform.position.x, player.transform.position.y, zDepth);
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPos, smoothingFactor * Time.fixedDeltaTime);
    }
    public void SnapToObject()
    {
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, zDepth);
    }

    public void SetPlayerDe(Scene scene, LoadSceneMode loadMode)
    {
        player = FindObjectOfType<PlatformerPlayer_Movement>().gameObject;
        Debug.Log($"{name} called SetPlayer on scene load. Found {player.name}.");
        Assert.IsNotNull(player);
        SnapToObject();
    }
}

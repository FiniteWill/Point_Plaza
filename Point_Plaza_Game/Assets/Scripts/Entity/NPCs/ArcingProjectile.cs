using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(DamageZone))]
[RequireComponent(typeof(Rigidbody2D))]
public class ArcingProjectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 3f;
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private float verticalForce;
    [SerializeField] private float horizontalForce;
    public Vector2 Direction { get { return direction; } set { direction = value; } }
    public int Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }

    private DamageZone projectileDamageZone = null;
    private Rigidbody2D rgbd2D = null;
    // Start is called before the first frame update
    void Start()
    {
        projectileDamageZone = GetComponent<DamageZone>();
        Assert.IsNotNull(projectileDamageZone);
        rgbd2D = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rgbd2D);
    }

    // Update is called once per frame
    void Update()
    {
        rgbd2D.position += speed * direction + new Vector2(horizontalForce, verticalForce);
    }
}
using UnityEngine;
public interface IProjectile
{
    public Vector2 Direction { get; set; }
    public int Damage { get; set; }
    public float Speed { get; set; }
}

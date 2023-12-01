using UnityEngine;

public class DropChange : MonoBehaviour
{

    [SerializeField] private ParticleSystem _destroyDrop;
    [SerializeField] private AudioClip _destroySound;


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.gameObject.CompareTag("Drop") && !other.gameObject.CompareTag("SpawnArea"))
        {
            if (!other.gameObject.CompareTag("DeadZone"))
            {
                Vector2 spawnEfectPosition = new Vector2(transform.position.x, transform.position.y - 0.31f);
                ParticleSystem newEfect = Instantiate(_destroyDrop, spawnEfectPosition, Quaternion.identity);
                newEfect.Play();
                SoundManager.instance.PlaySound(_destroySound, 0.2f);
            }
            Destroy(gameObject);
        }
    }
}

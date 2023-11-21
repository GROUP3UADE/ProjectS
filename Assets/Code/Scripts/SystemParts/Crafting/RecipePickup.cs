using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class RecipePickup : MonoBehaviour
{
    [SerializeField] private CraftRecipeSO recipeToGive;
    [SerializeField] private float lifetime = 60f;

    private void Awake()
    {
        Invoke(nameof(Destruction), lifetime);
    }

    private void Start()
    {
        var spriteR = GetComponent<SpriteRenderer>();
        var coll = GetComponent<BoxCollider2D>();
        coll.isTrigger = true;
        coll.offset = new Vector2(0, 0);
        var bounds = spriteR.bounds;
        var lossyScale = transform.lossyScale;
        coll.size = new Vector3(bounds.size.x / lossyScale.x,
            bounds.size.y / lossyScale.y,
            bounds.size.z / lossyScale.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerModel = other.GetComponent<PlayerModel>();
        // Confirmar que fue un jugador y que ese jugador no tenga el inventario lleno
        if (!playerModel || !playerModel.CompareTag("PlayerDetection")) return;
        GameManager.Instance.CraftDatabase.UnlockRecipe(recipeToGive);
        AudioManager.Instance.PlayPickUpSound();
        // quizas cambiar esto por pool management
        Destruction();
    }

    private void Destruction()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
using UnityEngine;

// Clase para recoger las pociones.
public class Potion : Element
{
    // Tipo y duración.
    public PotionType type;
    public float duration;

    // ¿Se usó?
    bool used;

    // Empieza sin ser usada.
    private void Start() => used = false;

    private void Update() { /* Nada */ }

    void OnTriggerEnter(Collider other)
    {
        if (!used)
        {
            // Se "destruye", da el efecto, destruye y usa.
            gameObject.GetComponent<Animator>().SetTrigger("destroy");
            other.gameObject.GetComponent<Character>().StartPotionEffect(type, duration);
            Delete(1);
            used = true;
        }
    }

    // Por ser Element.
    public override void InteractionBad() { /* Nada. */ }
    public override void InteractionGood() { /* Nada. */ }
}

// Tipos.
public enum PotionType
{
    speed,
    moral
};

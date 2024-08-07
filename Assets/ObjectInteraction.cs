using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public AudioClip interactionSound;
    public Animation interactionAnimation;

    void OnMouseDown()
    {
        // Animation und Sound abspielen
        PlayInteractionFeedback();
        // Objekt dem Inventar hinzufügen
        UIManager.Instance.UpdateInventoryUI(gameObject);
        //Das Objekt deaktivieren oder aus der Szene entfernen
        gameObject.SetActive(false);
    }

    void PlayInteractionFeedback()
    {
        if (interactionAnimation != null)
        {
            interactionAnimation.Play();
        }
        if (interactionSound != null)
        {
            AudioSource.PlayClipAtPoint(interactionSound, transform.position);
        }
    }
}

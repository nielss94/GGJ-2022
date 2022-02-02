using UnityEngine;

public interface IPickUp
{
    public Sprite InteractSprite { get; set; }
    public string GetInteractString();
    public string GetInteractImageString();
    public bool CanInteract();
}